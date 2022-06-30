using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ANS.API.Client.ECloud.Models.V1;
using ANS.API.Client.ECloud.Operations;
using ANS.API.Client.Exception;
using ANS.API.Client.Models;

namespace ANS.API.Client.ECloud.Tests.Operations
{
    [TestClass]
    public class SolutionNetworkOperationsTests
    {
        [TestMethod]
        public async Task GetSolutionNetworksAsync_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAllAsync(Arg.Any<ANSClient.GetPaginatedAsyncFunc<NetworkV1>>(), null).Returns(Task.Run<IList<NetworkV1>>(() =>
            {
                return new List<NetworkV1>()
                {
                    new NetworkV1(),
                    new NetworkV1()
                };
            }));

            var ops = new SolutionNetworkOperations<NetworkV1>(client);
            var solutions = await ops.GetSolutionNetworksAsync(123);

            Assert.AreEqual(2, solutions.Count);
        }

        [TestMethod]
        public async Task GetSolutionNetworksPaginatedAsync_ExpectedClientCall()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetPaginatedAsync<NetworkV1>("/ecloud/v1/solutions/123/networks").Returns(Task.Run(() =>
            {
                return new Paginated<NetworkV1>(client, "/ecloud/v1/solutions/123/networks", null, new Response.ClientResponse<System.Collections.Generic.IList<NetworkV1>>()
                {
                    Body = new Response.ClientResponseBody<System.Collections.Generic.IList<NetworkV1>>()
                    {
                        Data = new List<NetworkV1>()
                        {
                            new NetworkV1(),
                            new NetworkV1()
                        }
                    }
                });
            }));

            var ops = new SolutionNetworkOperations<NetworkV1>(client);
            var paginated = await ops.GetSolutionNetworksPaginatedAsync(123);

            Assert.AreEqual(2, paginated.Items.Count);
        }

        [TestMethod]
        public async Task GetSolutionNetworksPaginatedAsync_InvalidSolutionID_ThrowsANSClientValidationException()
        {
            var ops = new SolutionNetworkOperations<NetworkV1>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetSolutionNetworksPaginatedAsync(0));
        }
    }
}