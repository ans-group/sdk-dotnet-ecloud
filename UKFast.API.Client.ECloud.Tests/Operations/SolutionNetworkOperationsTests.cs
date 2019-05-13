using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models;
using UKFast.API.Client.ECloud.Models.Request;
using UKFast.API.Client.ECloud.Operations;
using UKFast.API.Client.Exception;
using UKFast.API.Client.Models;

namespace UKFast.API.Client.ECloud.Operations.Tests
{
    [TestClass]
    public class SolutionNetworkOperationsTests
    {
        [TestMethod]
        public async Task GetSolutionNetworksAsync_ExpectedResult()
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

            var ops = new SolutionNetworkOperations<Network>(client);
            var solutions = await ops.GetSolutionNetworksAsync(123);

            Assert.AreEqual(2, solutions.Count);
        }

        [TestMethod]
        public async Task GetSolutionNetworksPaginatedAsync_ExpectedClientCall()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetPaginatedAsync<Network>("/ecloud/v1/solutions/123/networks").Returns(Task.Run(() =>
            {
                return new Paginated<Network>(client, "/ecloud/v1/solutions/123/networks", null, new Response.ClientResponse<System.Collections.Generic.IList<Network>>()
                {
                    Body = new Response.ClientResponseBody<System.Collections.Generic.IList<Network>>()
                    {
                        Data = new List<Network>()
                        {
                            new Network(),
                            new Network()
                        }
                    }
                });
            }));

            var ops = new SolutionNetworkOperations<Network>(client);
            var paginated = await ops.GetSolutionNetworksPaginatedAsync(123);

            Assert.AreEqual(2, paginated.Items.Count);
        }

        [TestMethod]
        public async Task GetSolutionNetworksPaginatedAsync_InvalidSolutionID_ThrowsUKFastClientValidationException()
        {
            var ops = new SolutionNetworkOperations<Network>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.GetSolutionNetworksPaginatedAsync(0));
        }
    }
}
