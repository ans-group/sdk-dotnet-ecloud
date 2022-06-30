using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NSubstitute.ExceptionExtensions;
using ANS.API.Client.ECloud.Models.V2;
using ANS.API.Client.ECloud.Models.V2.Request;
using ANS.API.Client.ECloud.Operations;
using ANS.API.Client.Exception;
using ANS.API.Client.Models;
using ANS.API.Client.Response;

namespace ANS.API.Client.ECloud.Tests.Operations
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

            IANSECloudClient client = Substitute.For<IANSECloudClient>();
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
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAllAsync(Arg.Any<ANSClient.GetPaginatedAsyncFunc<FirewallPolicy>>(), null).Returns(Task.Run<IList<FirewallPolicy>>(() =>
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
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

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
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

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
        public async Task GetFirewallPolicyAsync_InvalidFirewallPolicyName_ThrowsANSClientValidationException()
        {
            var ops = new FirewallPolicyOperations<FirewallPolicy>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetFirewallPolicyAsync(""));
        }

        [TestMethod]
        public async Task GetFirewallPolicyAsync_NotFound_ThrowsANSClientNotFoundRequestException()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAsync<FirewallPolicy>("/ecloud/v2/firewall-policies/fwp-abcd1234").Throws(
                new ANSClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new FirewallPolicyOperations<FirewallPolicy>(client);

            await Assert.ThrowsExceptionAsync<ANSClientNotFoundRequestException>(() => ops.GetFirewallPolicyAsync("fwp-abcd1234"));
        }

        [TestMethod]
        public async Task UpdateFirewallPolicyAsync_ExpectedResult()
        {
            UpdateFirewallPolicyRequest req = new UpdateFirewallPolicyRequest()
            {
                Name = "test"
            };

            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            var ops = new FirewallPolicyOperations<FirewallPolicy>(client);
            await ops.UpdateFirewallPolicyAsync("fwp-abcd1234", req);

            await client.Received().PatchAsync("/ecloud/v2/firewall-policies/fwp-abcd1234", req);
        }

        [TestMethod]
        public async Task UpdateFirewallPolicyAsync_InvalidFirewallPolicyID_ThrowsANSClientValidationException()
        {
            var ops = new FirewallPolicyOperations<FirewallPolicy>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.UpdateFirewallPolicyAsync("", null));
        }

        [TestMethod]
        public async Task UpdateFirewallPolicyAsync_NotFound_ThrowsANSClientNotFoundRequestException()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.PatchAsync("/ecloud/v2/firewall-policies/fwp-abcd1234").Throws(
                new ANSClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new FirewallPolicyOperations<FirewallPolicy>(client);

            await Assert.ThrowsExceptionAsync<ANSClientNotFoundRequestException>(() => ops.UpdateFirewallPolicyAsync("fwp-abcd1234", null));
        }

        [TestMethod]
        public async Task DeleteFirewallPolicyAsync_ValidParameters()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            var ops = new FirewallPolicyOperations<FirewallPolicy>(client);
            await ops.DeleteFirewallPolicyAsync("fwp-abcd1234");

            await client.Received().DeleteAsync("/ecloud/v2/firewall-policies/fwp-abcd1234");
        }

        [TestMethod]
        public async Task DeleteFirewallPolicyAsync_InvalidFirewallPolicyID_ThrowsANSClientValidationException()
        {
            var ops = new FirewallPolicyOperations<FirewallPolicy>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.DeleteFirewallPolicyAsync(""));
        }

        [TestMethod]
        public async Task DeleteFirewallPolicyAsync_NotFound_ThrowsANSClientNotFoundRequestException()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.DeleteAsync("/ecloud/v2/firewall-policies/fwp-abcd1234").Throws(
                new ANSClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new FirewallPolicyOperations<FirewallPolicy>(client);

            await Assert.ThrowsExceptionAsync<ANSClientNotFoundRequestException>(() => ops.DeleteFirewallPolicyAsync("fwp-abcd1234"));
        }
    }
}