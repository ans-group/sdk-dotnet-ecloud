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
    public class HostOperationsTests
    {
        [TestMethod]
        public async Task GetHostsAsync_ExpectedResult()
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

            var ops = new HostOperations<Host>(client);
            var hosts = await ops.GetHostsAsync();

            Assert.AreEqual(2, hosts.Count);
        }

        [TestMethod]
        public async Task GetHostsPaginatedAsync_ExpectedClientCall()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetPaginatedAsync<Host>("/ecloud/v1/hosts").Returns(Task.Run(() =>
            {
                return new Paginated<Host>(client, "/ecloud/v1/hosts", null, new Response.ClientResponse<System.Collections.Generic.IList<Host>>()
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

            var ops = new HostOperations<Host>(client);
            var paginated = await ops.GetHostsPaginatedAsync();

            Assert.AreEqual(2, paginated.Items.Count);
        }

        [TestMethod]
        public async Task GetHostAsync_ValidParameters_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAsync<Host>("/ecloud/v1/hosts/123").Returns(new Host()
            {
                ID = 123
            });

            var ops = new HostOperations<Host>(client);
            var hostitem = await ops.GetHostAsync(123);

            Assert.AreEqual(123, hostitem.ID);
        }

        [TestMethod]
        public async Task GetHostAsync_InvalidHostID_ThrowsANSClientValidationException()
        {
            var ops = new HostOperations<Host>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetHostAsync(0));
        }
    }
}