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
    public class FirewallOperationsTests
    {
        [TestMethod]
        public async Task GetFirewallsAsync_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAllAsync(Arg.Any<ANSClient.GetPaginatedAsyncFunc<Firewall>>(), null).Returns(Task.Run<IList<Firewall>>(() =>
            {
                return new List<Firewall>()
                {
                    new Firewall(),
                    new Firewall()
                };
            }));

            var ops = new FirewallOperations<Firewall>(client);
            var firewalls = await ops.GetFirewallsAsync();

            Assert.AreEqual(2, firewalls.Count);
        }

        [TestMethod]
        public async Task GetFirewallsPaginatedAsync_ExpectedClientCall()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetPaginatedAsync<Firewall>("/ecloud/v1/firewalls").Returns(Task.Run(() =>
            {
                return new Paginated<Firewall>(client, "/ecloud/v1/firewalls", null, new Response.ClientResponse<System.Collections.Generic.IList<Firewall>>()
                {
                    Body = new Response.ClientResponseBody<System.Collections.Generic.IList<Firewall>>()
                    {
                        Data = new List<Firewall>()
                        {
                            new Firewall(),
                            new Firewall()
                        }
                    }
                });
            }));

            var ops = new FirewallOperations<Firewall>(client);
            var paginated = await ops.GetFirewallsPaginatedAsync();

            Assert.AreEqual(2, paginated.Items.Count);
        }

        [TestMethod]
        public async Task GetFirewallAsync_ValidParameters_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAsync<Firewall>("/ecloud/v1/firewalls/123").Returns(new Firewall()
            {
                ID = 123
            });

            var ops = new FirewallOperations<Firewall>(client);
            var firewall = await ops.GetFirewallAsync(123);

            Assert.AreEqual(123, firewall.ID);
        }

        [TestMethod]
        public async Task GetFirewallAsync_InvalidFirewallID_ThrowsANSClientValidationException()
        {
            var ops = new FirewallOperations<Firewall>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetFirewallAsync(0));
        }

        [TestMethod]
        public async Task GetFirewallConfigAsync_ValidParameters_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAsync<FirewallConfig>("/ecloud/v1/firewalls/123/config").Returns(new FirewallConfig()
            {
                Config = "testconfig"
            });

            var ops = new FirewallOperations<Firewall>(client);
            var config = await ops.GetFirewallConfigAsync(123);

            Assert.AreEqual("testconfig", config.Config);
        }

        [TestMethod]
        public async Task GetFirewallConfigAsync_InvalidFirewallID_ThrowsANSClientValidationException()
        {
            var ops = new FirewallOperations<Firewall>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetFirewallConfigAsync(0));
        }
    }
}