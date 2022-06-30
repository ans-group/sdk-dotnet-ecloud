using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NSubstitute.ExceptionExtensions;
using ANS.API.Client.ECloud.Models.V2;
using ANS.API.Client.ECloud.Operations;
using ANS.API.Client.Exception;
using ANS.API.Client.Models;
using ANS.API.Client.Response;

namespace ANS.API.Client.ECloud.Tests.Operations
{
    [TestClass]
    public class RegionOperationsTests
    {
        [TestMethod]
        public async Task GetRegionsAsync_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAllAsync(Arg.Any<ANSClient.GetPaginatedAsyncFunc<Region>>(), null).Returns(Task.Run<IList<Region>>(() =>
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
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

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
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

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
        public async Task GetRegionAsync_InvalidRegionID_ThrowsANSClientValidationException()
        {
            var ops = new RegionOperations<Region>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetRegionAsync(""));
        }

        [TestMethod]
        public async Task GetRegionAsync_NotFound_ThrowsANSClientNotFoundRequestException()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAsync<Region>("/ecloud/v2/regions/reg-abcd1234").Throws(
                new ANSClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new RegionOperations<Region>(client);

            await Assert.ThrowsExceptionAsync<ANSClientNotFoundRequestException>(() => ops.GetRegionAsync("reg-abcd1234"));
        }
    }
}