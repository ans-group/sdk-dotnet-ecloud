using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using UKFast.API.Client.ECloud.Models.V1;
using UKFast.API.Client.ECloud.Operations;
using UKFast.API.Client.Exception;
using UKFast.API.Client.Models;

namespace UKFast.API.Client.ECloud.Tests.Operations
{
    [TestClass]
    public class SolutionHostOperationsTests
    {
        [TestMethod]
        public async Task GetSolutionHostsAsync_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetAllAsync(Arg.Any<UKFastClient.GetPaginatedAsyncFunc<Host>>(), null).Returns(Task.Run<IList<Host>>(() =>
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
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

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
        public async Task GetSolutionHostsPaginatedAsync_InvalidSolutionID_ThrowsUKFastClientValidationException()
        {
            var ops = new SolutionHostOperations<Host>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.GetSolutionHostsPaginatedAsync(0));
        }
    }
}