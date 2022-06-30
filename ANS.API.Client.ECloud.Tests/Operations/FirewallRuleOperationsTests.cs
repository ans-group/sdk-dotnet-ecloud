using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using ANS.API.Client.ECloud.Models.V2;
using ANS.API.Client.ECloud.Operations;
using ANS.API.Client.Exception;
using ANS.API.Client.Models;
using ANS.API.Client.Response;

namespace ANS.API.Client.ECloud.Tests.Operations
{
    [TestClass]
    public class FirewallRuleOperationsTests
    {
        [TestMethod]
        public async Task GetFirewallRulesAsync_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAllAsync(Arg.Any<ANSClient.GetPaginatedAsyncFunc<FirewallRule>>(), null).Returns(Task.Run<IList<FirewallRule>>(() =>
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
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

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
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

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
        public async Task GetFirewallRuleAsync_InvalidFirewallRuleName_ThrowsANSClientValidationException()
        {
            var ops = new FirewallRuleOperations<FirewallRule>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetFirewallRuleAsync(""));
        }

        [TestMethod]
        public async Task GetFirewallRuleAsync_NotFound_ThrowsANSClientNotFoundRequestException()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAsync<FirewallRule>("/ecloud/v2/firewall-rules/fwr-abcd1234").Throws(
                new ANSClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new FirewallRuleOperations<FirewallRule>(client);

            await Assert.ThrowsExceptionAsync<ANSClientNotFoundRequestException>(() => ops.GetFirewallRuleAsync("fwr-abcd1234"));
        }
    }
}