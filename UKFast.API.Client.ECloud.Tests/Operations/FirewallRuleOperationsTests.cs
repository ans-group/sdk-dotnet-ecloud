using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using NSubstitute;
using UKFast.API.Client.ECloud.Models.V2;
using UKFast.API.Client.ECloud.Operations;
using UKFast.API.Client.Exception;
using UKFast.API.Client.Models;
using UKFast.API.Client.Response;

namespace UKFast.API.Client.ECloud.Tests.Operations
{
    [TestClass]
    public class FirewallRuleOperationsTests
    {
        [TestMethod]
        public async Task GetFirewallRulesAsync_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetAllAsync(Arg.Any<UKFastClient.GetPaginatedAsyncFunc<FirewallRule>>(), null).Returns(Task.Run<IList<FirewallRule>>(() =>
            {
                return new List<FirewallRule>()
                {
                    new FirewallRule(),
                    new FirewallRule()
                };
            }));

            var ops = new FirewallRuleOperations<FirewallRule>(client);
            var fwRules = await ops.GetFirewallRulesAsync();

            Assert.AreEqual(2, fwRules.Count);
        }

        [TestMethod]
        public async Task GetFirewallRulesPaginatedAsync_ExpectedClientCall()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetPaginatedAsync<FirewallRule>("/ecloud/v2/firewall-rules").Returns(Task.Run(() =>
            {
                return new Paginated<FirewallRule>(client, "/ecloud/v2/firewall-rules", null,
                    new ClientResponse<IList<FirewallRule>>()
                    {
                        Body = new ClientResponseBody<IList<FirewallRule>>()
                        {
                            Data = new List<FirewallRule>()
                            {
                                new FirewallRule(),
                                new FirewallRule()
                            }
                        }
                    });
            }));

            var ops = new FirewallRuleOperations<FirewallRule>(client);
            var paginated = await ops.GetFirewallRulesPaginatedAsync();

            Assert.AreEqual(2, paginated.Items.Count);
        }

        [TestMethod]
        public async Task GetFirewallRuleAsync_ValidParameters_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            string fwRuleID = "fwr-abcd1234";

            client.GetAsync<FirewallRule>($"/ecloud/v2/firewall-rules/{fwRuleID}").Returns(new FirewallRule()
            {
                ID = fwRuleID
            });

            var ops = new FirewallRuleOperations<FirewallRule>(client);
            var fwRule = await ops.GetFirewallRuleAsync(fwRuleID);

            Assert.AreEqual("fwr-abcd1234", fwRule.ID);
        }

        [TestMethod]
        public async Task GetFirewallRuleAsync_InvalidFirewallRuleName_ThrowsUKFastClientValidationException()
        {
            var ops = new FirewallRuleOperations<FirewallRule>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.GetFirewallRuleAsync(""));
        }
    }
}