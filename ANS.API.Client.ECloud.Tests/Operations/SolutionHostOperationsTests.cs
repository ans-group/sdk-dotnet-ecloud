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
    public class SolutionHostOperationsTests
    {
        [TestMethod]
        public async Task GetSolutionHostsAsync_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAllAsync(Arg.Any<ANSClient.GetPaginatedAsyncFunc<Host>>(), null).Returns(Task.Run<IList<Host>>(() =>
            {
                return new List<Host>()
                {
                    new Host(),
                    new Host()
                };
            }));

            var ops = new SolutionHostOperations<Host>(client);
            var solutions = await ops.GetSolutionHostsAsync(123);

            Assert.AreEqual(2, solutions.Count);
        }

        [TestMethod]
        public async Task GetSolutionHostsPaginatedAsync_ExpectedClientCall()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetPaginatedAsync<Host>("/ecloud/v1/solutions/123/hosts").Returns(Task.Run(() =>
            {
                return new Paginated<Host>(client, "/ecloud/v1/solutions/123/hosts", null, new Response.ClientResponse<System.Collections.Generic.IList<Host>>()
                {
                    Body = new Response.ClientResponseBody<System.Collections.Generic.IList<Host>>()
                    {
                        Data = new List<Host>()
                        {
                            new Host(),
                            new Host()
                        }
                    }
                });
            }));

            var ops = new SolutionHostOperations<Host>(client);
            var paginated = await ops.GetSolutionHostsPaginatedAsync(123);

            Assert.AreEqual(2, paginated.Items.Count);
        }

        [TestMethod]
        public async Task GetSolutionHostsPaginatedAsync_InvalidSolutionID_ThrowsANSClientValidationException()
        {
            var ops = new SolutionHostOperations<Host>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetSolutionHostsPaginatedAsync(0));
        }
    }
}