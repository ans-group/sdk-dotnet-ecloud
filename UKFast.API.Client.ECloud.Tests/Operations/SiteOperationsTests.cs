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
    public class SiteOperationsTests
    {
        [TestMethod]
        public async Task GetSitesAsync_ExpectedResult()
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

            var ops = new SiteOperations<Site>(client);
            var sites = await ops.GetSitesAsync();

            Assert.AreEqual(2, sites.Count);
        }

        [TestMethod]
        public async Task GetSitesPaginatedAsync_ExpectedClientCall()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetPaginatedAsync<Site>("/ecloud/v1/sites").Returns(Task.Run(() =>
            {
                return new Paginated<Site>(client, "/ecloud/v1/sites", null, new Response.ClientResponse<System.Collections.Generic.IList<Site>>()
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

            var ops = new SiteOperations<Site>(client);
            var paginated = await ops.GetSitesPaginatedAsync();

            Assert.AreEqual(2, paginated.Items.Count);
        }

        [TestMethod]
        public async Task GetSiteAsync_ValidParameters_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetAsync<Site>("/ecloud/v1/sites/123").Returns(new Site()
            {
                ID = 123
            });

            var ops = new SiteOperations<Site>(client);
            var site = await ops.GetSiteAsync(123);

            Assert.AreEqual(123, site.ID);
        }

        [TestMethod]
        public async Task GetSiteAsync_InvalidSiteID_ThrowsUKFastClientValidationException()
        {
            var ops = new SiteOperations<Site>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.GetSiteAsync(0));
        }
    }
}
