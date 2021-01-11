using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NSubstitute.ExceptionExtensions;
using UKFast.API.Client.ECloud.Models.V2;
using UKFast.API.Client.ECloud.Models.V2.Request;
using UKFast.API.Client.ECloud.Operations;
using UKFast.API.Client.Exception;
using UKFast.API.Client.Models;
using UKFast.API.Client.Response;

namespace UKFast.API.Client.ECloud.Tests.Operations
{
    [TestClass]
    public class FirewallPolicyOperationsTests
    {
        [TestMethod]
        public async Task CreateFirewallPolicyAsync_ExpectedResult()
        {
            CreateFirewallPolicyRequest req = new CreateFirewallPolicyRequest()
            {
                Name = "test policy"
            };

            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();
            client.PostAsync<FirewallPolicy>("/ecloud/v2/firewall-policies", req).Returns(new FirewallPolicy()
            {
                ID = "fwp-abcd1234"
            });

            var ops = new FirewallPolicyOperations<FirewallPolicy>(client);
            var policy = await ops.CreateFirewallPolicyAsync(req);

            Assert.AreEqual("fwp-abcd1234", policy);
        }

        [TestMethod]
        public async Task GetFirewallPoliciesAsync_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetAllAsync(Arg.Any<UKFastClient.GetPaginatedAsyncFunc<FirewallPolicy>>(), null).Returns(Task.Run<IList<FirewallPolicy>>(() =>
            {
                return new List<FirewallPolicy>()
                {
                    new FirewallPolicy(),
                    new FirewallPolicy()
                };
            }));

            var ops = new FirewallPolicyOperations<FirewallPolicy>(client);
            var policies = await ops.GetFirewallPoliciesAsync();

            Assert.AreEqual(2, policies.Count);
        }

        [TestMethod]
        public async Task GetFirewallPoliciesPaginatedAsync_ExpectedClientCall()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetPaginatedAsync<FirewallPolicy>("/ecloud/v2/firewall-policies").Returns(Task.Run(() =>
            {
                return new Paginated<FirewallPolicy>(client, "/ecloud/v2/firewall-policies", null,
                    new ClientResponse<IList<FirewallPolicy>>()
                    {
                        Body = new ClientResponseBody<IList<FirewallPolicy>>()
                        {
                            Data = new List<FirewallPolicy>()
                            {
                                new FirewallPolicy(),
                                new FirewallPolicy()
                            }
                        }
                    });
            }));

            var ops = new FirewallPolicyOperations<FirewallPolicy>(client);
            var paginated = await ops.GetFirewallPoliciesPaginatedAsync();

            Assert.AreEqual(2, paginated.Items.Count);
        }

        [TestMethod]
        public async Task GetFirewallPolicyAsync_ValidParameters_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            string fwp = "fwp-abcd1234";

            client.GetAsync<FirewallPolicy>($"/ecloud/v2/firewall-policies/{fwp}").Returns(new FirewallPolicy()
            {
                ID = fwp
            });

            var ops = new FirewallPolicyOperations<FirewallPolicy>(client);
            var policy = await ops.GetFirewallPolicyAsync(fwp);

            Assert.AreEqual("fwp-abcd1234", policy.ID);
        }

        [TestMethod]
        public async Task GetFirewallPolicyAsync_InvalidFirewallPolicyName_ThrowsUKFastClientValidationException()
        {
            var ops = new FirewallPolicyOperations<FirewallPolicy>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.GetFirewallPolicyAsync(""));
        }

        [TestMethod]
        public async Task GetFirewallPolicyAsync_NotFound_ThrowsUKFastClientNotFoundRequestException()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetAsync<FirewallPolicy>("/ecloud/v2/firewall-policies/fwp-abcd1234").Throws(
                new UKFastClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new FirewallPolicyOperations<FirewallPolicy>(client);

            await Assert.ThrowsExceptionAsync<UKFastClientNotFoundRequestException>(() => ops.GetFirewallPolicyAsync("fwp-abcd1234"));
        }

        [TestMethod]
        public async Task UpdateFirewallPolicyAsync_ExpectedResult()
        {
            UpdateFirewallPolicyRequest req = new UpdateFirewallPolicyRequest()
            {
                Name = "test"
            };

            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            var ops = new FirewallPolicyOperations<FirewallPolicy>(client);
            await ops.UpdateFirewallPolicyAsync("fwp-abcd1234", req);

            await client.Received().PatchAsync("/ecloud/v2/firewall-policies/fwp-abcd1234", req);
        }

        [TestMethod]
        public async Task UpdateFirewallPolicyAsync_InvalidFirewallPolicyID_ThrowsUKFastClientValidationException()
        {
            var ops = new FirewallPolicyOperations<FirewallPolicy>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.UpdateFirewallPolicyAsync("", null));
        }

        [TestMethod]
        public async Task UpdateFirewallPolicyAsync_NotFound_ThrowsUKFastClientNotFoundRequestException()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.PatchAsync("/ecloud/v2/firewall-policies/fwp-abcd1234").Throws(
                new UKFastClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new FirewallPolicyOperations<FirewallPolicy>(client);

            await Assert.ThrowsExceptionAsync<UKFastClientNotFoundRequestException>(() => ops.UpdateFirewallPolicyAsync("fwp-abcd1234", null));
        }

        [TestMethod]
        public async Task DeleteFirewallPolicyAsync_ValidParameters()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            var ops = new FirewallPolicyOperations<FirewallPolicy>(client);
            await ops.DeleteFirewallPolicyAsync("fwp-abcd1234");

            await client.Received().DeleteAsync("/ecloud/v2/firewall-policies/fwp-abcd1234");
        }

        [TestMethod]
        public async Task DeleteFirewallPolicyAsync_InvalidFirewallPolicyID_ThrowsUKFastClientValidationException()
        {
            var ops = new FirewallPolicyOperations<FirewallPolicy>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.DeleteFirewallPolicyAsync(""));
        }

        [TestMethod]
        public async Task DeleteFirewallPolicyAsync_NotFound_ThrowsUKFastClientNotFoundRequestException()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.DeleteAsync("/ecloud/v2/firewall-policies/fwp-abcd1234").Throws(
                new UKFastClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new FirewallPolicyOperations<FirewallPolicy>(client);

            await Assert.ThrowsExceptionAsync<UKFastClientNotFoundRequestException>(() => ops.DeleteFirewallPolicyAsync("fwp-abcd1234"));
        }
    }
}