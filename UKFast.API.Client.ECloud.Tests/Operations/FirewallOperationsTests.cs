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
    public class FirewallOperationsTests
    {
        [TestMethod]
        public async Task GetFirewallsAsync_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetAllAsync(Arg.Any<UKFastClient.GetPaginatedAsyncFunc<Firewall>>(), null).Returns(Task.Run<IList<Firewall>>(() =>
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
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

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
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetAsync<Firewall>("/ecloud/v1/firewalls/123").Returns(new Firewall()
            {
                ID = 123
            });

            var ops = new FirewallOperations<Firewall>(client);
            var firewall = await ops.GetFirewallAsync(123);

            Assert.AreEqual(123, firewall.ID);
        }

        [TestMethod]
        public async Task GetFirewallAsync_InvalidFirewallID_ThrowsUKFastClientValidationException()
        {
            var ops = new FirewallOperations<Firewall>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.GetFirewallAsync(0));
        }

        [TestMethod]
        public async Task GetFirewallConfigAsync_ValidParameters_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetAsync<FirewallConfig>("/ecloud/v1/firewalls/123/config").Returns(new FirewallConfig()
            {
                Config = "testconfig"
            });

            var ops = new FirewallOperations<Firewall>(client);
            var config = await ops.GetFirewallConfigAsync(123);

            Assert.AreEqual("testconfig", config.Config);
        }

        [TestMethod]
        public async Task GetFirewallConfigAsync_InvalidFirewallID_ThrowsUKFastClientValidationException()
        {
            var ops = new FirewallOperations<Firewall>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.GetFirewallConfigAsync(0));
        }
    }
}
