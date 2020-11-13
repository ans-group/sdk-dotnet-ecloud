using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V2;
using UKFast.API.Client.ECloud.Models.V2.Request;
using UKFast.API.Client.ECloud.Operations;
using UKFast.API.Client.Exception;
using UKFast.API.Client.Models;
using UKFast.API.Client.Response;

namespace UKFast.API.Client.ECloud.Tests.Operations
{
    [TestClass]
    public class NetworkOperationsTests
    {
        [TestMethod]
        public async Task CreateNetworkAsync_ExpectedResult()
        {
            CreateNetworkRequest req = new CreateNetworkRequest()
            {
                Name = "test-network"
            };

            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();
            client.PostAsync<Network>("/ecloud/v2/networks", req).Returns(new Network()
            {
                ID = "net-abcd1234"
            });

            var ops = new NetworkOperations<Network>(client);
            var networkID = await ops.CreateNetworkAsync(req);

            Assert.AreEqual("net-abcd1234", networkID);
        }

        [TestMethod]
        public async Task GetNetworksAsync_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetAllAsync(Arg.Any<UKFastClient.GetPaginatedAsyncFunc<Network>>(), null).Returns(Task.Run<IList<Network>>(() =>
            {
                return new List<Network>()
                {
                    new Network(),
                    new Network()
                };
            }));

            var ops = new NetworkOperations<Network>(client);
            var networks = await ops.GetNetworksAsync();

            Assert.AreEqual(2, networks.Count);
        }

        [TestMethod]
        public async Task GetNetworksPaginatedAsync_ExpectedClientCall()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetPaginatedAsync<Network>("/ecloud/v2/networks").Returns(Task.Run(() =>
            {
                return new Paginated<Network>(client, "/ecloud/v2/networks", null,
                    new ClientResponse<IList<Network>>()
                    {
                        Body = new ClientResponseBody<IList<Network>>()
                        {
                            Data = new List<Network>()
                            {
                                new Network(),
                                new Network()
                            }
                        }
                    });
            }));

            var ops = new NetworkOperations<Network>(client);
            var paginated = await ops.GetNetworksPaginatedAsync();

            Assert.AreEqual(2, paginated.Items.Count);
        }

        [TestMethod]
        public async Task GetNetworkAsync_ValidParameters_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            string networkID = "net-abcd1234";

            client.GetAsync<Network>($"/ecloud/v2/networks/{networkID}").Returns(new Network()
            {
                ID = networkID
            });

            var ops = new NetworkOperations<Network>(client);
            var network = await ops.GetNetworkAsync(networkID);

            Assert.AreEqual("net-abcd1234", network.ID);
        }

        [TestMethod]
        public async Task GetNetworkAsync_InvalidNetworkName_ThrowsUKFastClientValidationException()
        {
            var ops = new NetworkOperations<Network>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.GetNetworkAsync(""));
        }

        [TestMethod]
        public async Task UpdateNetworkAsync_ExpectedResult()
        {
            UpdateNetworkRequest req = new UpdateNetworkRequest()
            {
                Name = "test-network"
            };

            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            var ops = new NetworkOperations<Network>(client);
            await ops.UpdateNetworkAsync("net-abcd1234", req);

            await client.Received().PatchAsync("/ecloud/v2/networks/net-abcd1234", req);
        }

        [TestMethod]
        public async Task UpdateNetworkAsync_InvalidNetworkID_ThrowsUKFastClientValidationException()
        {
            var ops = new NetworkOperations<Network>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.UpdateNetworkAsync("", null));
        }

        [TestMethod]
        public async Task DeleteNetworkAsync_ValidParameters()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            var ops = new NetworkOperations<Network>(client);
            await ops.DeleteNetworkAsync("net-abcd1234");

            await client.Received().DeleteAsync("/ecloud/v2/networks/net-abcd1234");
        }

        [TestMethod]
        public async Task DeleteNetworkAsync_InvalidNetworkID_ThrowsUKFastClientValidationException()
        {
            var ops = new NetworkOperations<Network>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.DeleteNetworkAsync(""));
        }
    }
}