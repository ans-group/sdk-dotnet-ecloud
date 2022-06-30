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
    public class SolutionFirewallOperationsTests
    {
        [TestMethod]
        public async Task GetSolutionFirewallsAsync_ExpectedResult()
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

            var ops = new SolutionFirewallOperations<Firewall>(client);
            var solutions = await ops.GetSolutionFirewallsAsync(123);

            Assert.AreEqual(2, solutions.Count);
        }

        [TestMethod]
        public async Task GetSolutionFirewallsPaginatedAsync_ExpectedClientCall()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

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
        public async Task GetSolutionFirewallsPaginatedAsync_InvalidSolutionID_ThrowsANSClientValidationException()
        {
            var ops = new SolutionFirewallOperations<Firewall>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetSolutionFirewallsPaginatedAsync(0));
        }
    }
}