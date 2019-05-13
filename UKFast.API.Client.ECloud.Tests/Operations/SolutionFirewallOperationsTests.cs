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
    public class SolutionFirewallOperationsTests
    {
        [TestMethod]
        public async Task GetSolutionFirewallsAsync_ExpectedResult()
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

            var ops = new SolutionFirewallOperations<Firewall>(client);
            var solutions = await ops.GetSolutionFirewallsAsync(123);

            Assert.AreEqual(2, solutions.Count);
        }

        [TestMethod]
        public async Task GetSolutionFirewallsPaginatedAsync_ExpectedClientCall()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetPaginatedAsync<Firewall>("/ecloud/v1/solutions/123/firewalls").Returns(Task.Run(() =>
            {
                return new Paginated<Firewall>(client, "/ecloud/v1/solutions/123/firewalls", null, new Response.ClientResponse<System.Collections.Generic.IList<Firewall>>()
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

            var ops = new SolutionFirewallOperations<Firewall>(client);
            var paginated = await ops.GetSolutionFirewallsPaginatedAsync(123);

            Assert.AreEqual(2, paginated.Items.Count);
        }

        [TestMethod]
        public async Task GetSolutionFirewallsPaginatedAsync_InvalidSolutionID_ThrowsUKFastClientValidationException()
        {
            var ops = new SolutionFirewallOperations<Firewall>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.GetSolutionFirewallsPaginatedAsync(0));
        }
    }
}
