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
    public class SolutionSiteOperationsTests
    {
        [TestMethod]
        public async Task GetSolutionSitesAsync_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAllAsync(Arg.Any<ANSClient.GetPaginatedAsyncFunc<Site>>(), null).Returns(Task.Run<IList<Site>>(() =>
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
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

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
        public async Task GetSolutionSitesPaginatedAsync_InvalidSolutionID_ThrowsANSClientValidationException()
        {
            var ops = new SolutionSiteOperations<Site>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetSolutionSitesPaginatedAsync(0));
        }
    }
}