using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NSubstitute.ExceptionExtensions;
using NSubstitute.Routing.Handlers;
using ANS.API.Client.ECloud.Models.V2;
using ANS.API.Client.ECloud.Operations;
using ANS.API.Client.Exception;
using ANS.API.Client.Models;
using ANS.API.Client.Response;

namespace ANS.API.Client.ECloud.Tests.Operations
{
    [TestClass]
    public class AvailabilityZoneOperationsTests
    {
        [TestMethod]
        public async Task GetAvailabilityZonesAsync_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAllAsync(Arg.Any<ANSClient.GetPaginatedAsyncFunc<AvailabilityZone>>(), null).Returns(Task.Run<IList<AvailabilityZone>>(() =>
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
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

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
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

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
        public async Task GetAvailabilityZoneAsync_InvalidAvailabilityZoneName_ThrowsANSClientValidationException()
        {
            var ops = new AvailabilityZoneOperations<AvailabilityZone>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetAvailabilityZoneAsync(""));
        }

        [TestMethod]
        public async Task GetAvailabilityZoneAsync_NotFound_ThrowsANSClientNotFoundRequestException()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAsync<AvailabilityZone>("/ecloud/v2/availability-zones/az-abcd1234").Throws(
                new ANSClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new AvailabilityZoneOperations<AvailabilityZone>(client);

            await Assert.ThrowsExceptionAsync<ANSClientNotFoundRequestException>(() => ops.GetAvailabilityZoneAsync("az-abcd1234"));
        }
    }
}