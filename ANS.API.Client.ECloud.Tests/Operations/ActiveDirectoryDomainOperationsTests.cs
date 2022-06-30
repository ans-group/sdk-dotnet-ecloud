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
    public class ActiveDirectoryDomainOperationsTests
    {
        [TestMethod]
        public async Task GetActiveDirectoryDomainsAsync_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAllAsync(Arg.Any<ANSClient.GetPaginatedAsyncFunc<ActiveDirectoryDomain>>(), null).Returns(Task.Run<IList<ActiveDirectoryDomain>>(() =>
            {
                return new List<ActiveDirectoryDomain>()
                 {
                        new ActiveDirectoryDomain(),
                        new ActiveDirectoryDomain()
                 };
            }));

            var ops = new ActiveDirectoryDomainOperations<ActiveDirectoryDomain>(client);
            var activeDirectoryDomains = await ops.GetActiveDirectoryDomainsAsync();

            Assert.AreEqual(2, activeDirectoryDomains.Count);
        }

        [TestMethod]
        public async Task GetActiveDirectoryDomainsPaginatedAsync_ExpectedClientCall()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetPaginatedAsync<ActiveDirectoryDomain>("/ecloud/v1/active-directory/domains").Returns(Task.Run(() =>
            {
                return new Paginated<ActiveDirectoryDomain>(client, "/ecloud/v1/active-directory/domains", null, new Response.ClientResponse<System.Collections.Generic.IList<ActiveDirectoryDomain>>()
                {
                    Body = new Response.ClientResponseBody<System.Collections.Generic.IList<ActiveDirectoryDomain>>()
                    {
                        Data = new List<ActiveDirectoryDomain>()
                        {
                            new ActiveDirectoryDomain(),
                            new ActiveDirectoryDomain()
                        }
                    }
                });
            }));

            var ops = new ActiveDirectoryDomainOperations<ActiveDirectoryDomain>(client);
            var paginated = await ops.GetActiveDirectoryDomainsPaginatedAsync();

            Assert.AreEqual(2, paginated.Items.Count);
        }

        [TestMethod]
        public async Task GetActiveDirectoryDomainAsync_ValidParameters_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAsync<ActiveDirectoryDomain>("/ecloud/v1/active-directory/domains/123").Returns(new ActiveDirectoryDomain()
            {
                ID = 123
            });

            var ops = new ActiveDirectoryDomainOperations<ActiveDirectoryDomain>(client);
            var domain = await ops.GetActiveDirectoryDomainAsync(123);

            Assert.AreEqual(123, domain.ID);
        }

        [TestMethod]
        public async Task GetActiveDirectoryDomainAsync_InvalidDomainID_ThrowsANSClientValidationException()
        {
            var ops = new ActiveDirectoryDomainOperations<ActiveDirectoryDomain>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetActiveDirectoryDomainAsync(0));
        }
    }
}