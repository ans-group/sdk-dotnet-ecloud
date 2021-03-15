using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NSubstitute.ExceptionExtensions;
using UKFast.API.Client.ECloud.Models.V2;
using UKFast.API.Client.ECloud.Operations;
using UKFast.API.Client.Exception;
using UKFast.API.Client.Models;
using UKFast.API.Client.Response;

namespace UKFast.API.Client.ECloud.Tests.Operations
{
    [TestClass]
    public class RegionOperationsTests
    {
        [TestMethod]
        public async Task GetRegionsAsync_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetAllAsync(Arg.Any<UKFastClient.GetPaginatedAsyncFunc<Region>>(), null).Returns(Task.Run<IList<Region>>(() =>
            {
                return new List<Region>()
                 {
                        new Region(),
                        new Region()
                 };
            }));

            var ops = new RegionOperations<Region>(client);
            var regions = await ops.GetRegionsAsync();

            Assert.AreEqual(2, regions.Count);
        }

        [TestMethod]
        public async Task GetRegionsPaginatedAsync_ExpectedClientCall()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetPaginatedAsync<Region>("/ecloud/v2/regions").Returns(Task.Run(() =>
            {
                return new Paginated<Region>(client, "/ecloud/v2/regions", null, new ClientResponse<IList<Region>>()
                {
                    Body = new ClientResponseBody<IList<Region>>()
                    {
                        Data = new List<Region>()
                        {
                            new Region(),
                            new Region()
                        }
                    }
                });
            }));

            var ops = new RegionOperations<Region>(client);
            var paginated = await ops.GetRegionsPaginatedAsync();

            Assert.AreEqual(2, paginated.Items.Count);
        }

        [TestMethod]
        public async Task GetRegionAsync_ValidParameters_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            string regionID = "reg-abcd1234";

            client.GetAsync<Region>($"/ecloud/v2/regions/{regionID}").Returns(new Region()
            {
                ID = regionID
            });

            var ops = new RegionOperations<Region>(client);
            var region = await ops.GetRegionAsync(regionID);

            Assert.AreEqual("reg-abcd1234", region.ID);
        }

        [TestMethod]
        public async Task GetRegionAsync_InvalidRegionID_ThrowsUKFastClientValidationException()
        {
            var ops = new RegionOperations<Region>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.GetRegionAsync(""));
        }

        [TestMethod]
        public async Task GetRegionAsync_NotFound_ThrowsUKFastClientNotFoundRequestException()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetAsync<Region>("/ecloud/v2/regions/reg-abcd1234").Throws(
                new UKFastClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new RegionOperations<Region>(client);

            await Assert.ThrowsExceptionAsync<UKFastClientNotFoundRequestException>(() => ops.GetRegionAsync("reg-abcd1234"));
        }
    }
}