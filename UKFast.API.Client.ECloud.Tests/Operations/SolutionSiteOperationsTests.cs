using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V1;
using UKFast.API.Client.Exception;
using UKFast.API.Client.Models;

namespace UKFast.API.Client.ECloud.Operations.Tests
{
    [TestClass]
    public class SolutionSiteOperationsTests
    {
        [TestMethod]
        public async Task GetSolutionSitesAsync_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetAllAsync(Arg.Any<UKFastClient.GetPaginatedAsyncFunc<Site>>(), null).Returns(Task.Run<IList<Site>>(() =>
            {
                return new List<Site>()
                {
                    new Site(),
                    new Site()
                };
            }));

            var ops = new SolutionSiteOperations<Site>(client);
            var solutions = await ops.GetSolutionSitesAsync(123);

            Assert.AreEqual(2, solutions.Count);
        }

        [TestMethod]
        public async Task GetSolutionSitesPaginatedAsync_ExpectedClientCall()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetPaginatedAsync<Site>("/ecloud/v1/solutions/123/sites").Returns(Task.Run(() =>
            {
                return new Paginated<Site>(client, "/ecloud/v1/solutions/123/sites", null, new Response.ClientResponse<System.Collections.Generic.IList<Site>>()
                {
                    Body = new Response.ClientResponseBody<System.Collections.Generic.IList<Site>>()
                    {
                        Data = new List<Site>()
                        {
                            new Site(),
                            new Site()
                        }
                    }
                });
            }));

            var ops = new SolutionSiteOperations<Site>(client);
            var paginated = await ops.GetSolutionSitesPaginatedAsync(123);

            Assert.AreEqual(2, paginated.Items.Count);
        }

        [TestMethod]
        public async Task GetSolutionSitesPaginatedAsync_InvalidSolutionID_ThrowsUKFastClientValidationException()
        {
            var ops = new SolutionSiteOperations<Site>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.GetSolutionSitesPaginatedAsync(0));
        }
    }
}