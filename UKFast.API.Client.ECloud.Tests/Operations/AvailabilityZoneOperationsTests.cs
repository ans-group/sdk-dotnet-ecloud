using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NSubstitute.ExceptionExtensions;
using NSubstitute.Routing.Handlers;
using UKFast.API.Client.ECloud.Models.V2;
using UKFast.API.Client.ECloud.Operations;
using UKFast.API.Client.Exception;
using UKFast.API.Client.Models;
using UKFast.API.Client.Response;

namespace UKFast.API.Client.ECloud.Tests.Operations
{
    [TestClass]
    public class AvailabilityZoneOperationsTests
    {
        [TestMethod]
        public async Task GetAvailabilityZonesAsync_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetAllAsync(Arg.Any<UKFastClient.GetPaginatedAsyncFunc<AvailabilityZone>>(), null).Returns(Task.Run<IList<AvailabilityZone>>(() =>
            {
                return new List<AvailabilityZone>()
                {
                    new AvailabilityZone(),
                    new AvailabilityZone()
                };
            }));

            var ops = new AvailabilityZoneOperations<AvailabilityZone>(client);
            var azs = await ops.GetAvailabilityZonesAsync();

            Assert.AreEqual(2, azs.Count);
        }

        [TestMethod]
        public async Task GetAvailabilityZonesPaginatedAsync_ExpectedClientCall()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetPaginatedAsync<AvailabilityZone>("/ecloud/v2/availability-zones").Returns(Task.Run(() =>
            {
                return new Paginated<AvailabilityZone>(client, "/ecloud/v2/availability-zones", null,
                    new ClientResponse<IList<AvailabilityZone>>()
                    {
                        Body = new ClientResponseBody<IList<AvailabilityZone>>()
                        {
                            Data = new List<AvailabilityZone>()
                            {
                                new AvailabilityZone(),
                                new AvailabilityZone()
                            }
                        }
                    });
            }));

            var ops = new AvailabilityZoneOperations<AvailabilityZone>(client);
            var paginated = await ops.GetAvailabilityZonesPaginatedAsync();

            Assert.AreEqual(2, paginated.Items.Count);
        }

        [TestMethod]
        public async Task GetAvailabilityZoneAsync_ValidParameters_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            string azId = "az-abcd1234";

            client.GetAsync<AvailabilityZone>($"/ecloud/v2/availability-zones/{azId}").Returns(new AvailabilityZone()
            {
                ID = azId
            });

            var ops = new AvailabilityZoneOperations<AvailabilityZone>(client);
            var az = await ops.GetAvailabilityZoneAsync(azId);

            Assert.AreEqual("az-abcd1234", az.ID);
        }

        [TestMethod]
        public async Task GetAvailabilityZoneAsync_InvalidAvailabilityZoneName_ThrowsUKFastClientValidationException()
        {
            var ops = new AvailabilityZoneOperations<AvailabilityZone>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.GetAvailabilityZoneAsync(""));
        }

        [TestMethod]
        public async Task GetAvailabilityZoneAsync_NotFound_ThrowsUKFastClientNotFoundRequestException()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetAsync<AvailabilityZone>("/ecloud/v2/availability-zones/az-abcd1234").Throws(
                new UKFastClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new AvailabilityZoneOperations<AvailabilityZone>(client);

            await Assert.ThrowsExceptionAsync<UKFastClientNotFoundRequestException>(() => ops.GetAvailabilityZoneAsync("az-abcd1234"));
        }
    }
}