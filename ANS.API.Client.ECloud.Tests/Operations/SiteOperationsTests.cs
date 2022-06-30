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
    public class SiteOperationsTests
    {
        [TestMethod]
        public async Task GetSitesAsync_ExpectedResult()
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

            var ops = new SiteOperations<Site>(client);
            var sites = await ops.GetSitesAsync();

            Assert.AreEqual(2, sites.Count);
        }

        [TestMethod]
        public async Task GetSitesPaginatedAsync_ExpectedClientCall()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

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
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAsync<Site>("/ecloud/v1/sites/123").Returns(new Site()
            {
                ID = 123
            });

            var ops = new SiteOperations<Site>(client);
            var site = await ops.GetSiteAsync(123);

            Assert.AreEqual(123, site.ID);
        }

        [TestMethod]
        public async Task GetSiteAsync_InvalidSiteID_ThrowsANSClientValidationException()
        {
            var ops = new SiteOperations<Site>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetSiteAsync(0));
        }
    }
}