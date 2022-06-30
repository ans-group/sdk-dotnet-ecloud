using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NSubstitute.ExceptionExtensions;
using ANS.API.Client.ECloud.Models.V2;
using ANS.API.Client.ECloud.Models.V2.Request;
using ANS.API.Client.ECloud.Operations;
using ANS.API.Client.Exception;
using ANS.API.Client.Models;
using ANS.API.Client.Response;

namespace ANS.API.Client.ECloud.Tests.Operations
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

            IANSECloudClient client = Substitute.For<IANSECloudClient>();
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
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAllAsync(Arg.Any<ANSClient.GetPaginatedAsyncFunc<Network>>(), null).Returns(Task.Run<IList<Network>>(() =>
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
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

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
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

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
        public async Task GetNetworkAsync_InvalidNetworkName_ThrowsANSClientValidationException()
        {
            var ops = new NetworkOperations<Network>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetNetworkAsync(""));
        }

        [TestMethod]
        public async Task GetNetworkAsync_NotFound_ThrowsANSClientNotFoundRequestException()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAsync<Network>("/ecloud/v2/networks/net-abcd1234").Throws(
                new ANSClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new NetworkOperations<Network>(client);

            await Assert.ThrowsExceptionAsync<ANSClientNotFoundRequestException>(() => ops.GetNetworkAsync("net-abcd1234"));
        }

        [TestMethod]
        public async Task UpdateNetworkAsync_ExpectedResult()
        {
            UpdateNetworkRequest req = new UpdateNetworkRequest()
            {
                Name = "test-network"
            };

            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            var ops = new NetworkOperations<Network>(client);
            await ops.UpdateNetworkAsync("net-abcd1234", req);

            await client.Received().PatchAsync("/ecloud/v2/networks/net-abcd1234", req);
        }

        [TestMethod]
        public async Task UpdateNetworkAsync_InvalidNetworkID_ThrowsANSClientValidationException()
        {
            var ops = new NetworkOperations<Network>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.UpdateNetworkAsync("", null));
        }

        [TestMethod]
        public async Task UpdateNetworkAsync_NotFound_ThrowsANSClientNotFoundRequestException()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.PatchAsync("/ecloud/v2/networks/net-abcd1234").Throws(
                new ANSClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new NetworkOperations<Network>(client);

            await Assert.ThrowsExceptionAsync<ANSClientNotFoundRequestException>(() => ops.UpdateNetworkAsync("net-abcd1234", null));
        }

        [TestMethod]
        public async Task DeleteNetworkAsync_ValidParameters()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            var ops = new NetworkOperations<Network>(client);
            await ops.DeleteNetworkAsync("net-abcd1234");

            await client.Received().DeleteAsync("/ecloud/v2/networks/net-abcd1234");
        }

        [TestMethod]
        public async Task DeleteNetworkAsync_InvalidNetworkID_ThrowsANSClientValidationException()
        {
            var ops = new NetworkOperations<Network>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.DeleteNetworkAsync(""));
        }

        [TestMethod]
        public async Task DeleteNetworkAsync_NotFound_ThrowsANSClientNotFoundRequestException()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.DeleteAsync("/ecloud/v2/networks/net-abcd1234").Throws(
                new ANSClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new NetworkOperations<Network>(client);

            await Assert.ThrowsExceptionAsync<ANSClientNotFoundRequestException>(() => ops.DeleteNetworkAsync("net-abcd1234"));
        }
    }
}