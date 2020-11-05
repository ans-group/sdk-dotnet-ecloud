using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V2;
using UKFast.API.Client.ECloud.Operations;
using UKFast.API.Client.Exception;
using UKFast.API.Client.Models;
using UKFast.API.Client.Response;

namespace UKFast.API.Client.ECloud.Tests.Operations
{
    [TestClass]
    public class DHCPOperationsTests
    {
        [TestMethod]
        public async Task GetDHCPsAsync_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetAllAsync(Arg.Any<UKFastClient.GetPaginatedAsyncFunc<DHCP>>(), null).Returns(Task.Run<IList<DHCP>>(() =>
            {
                return new List<DHCP>()
                 {
                        new DHCP(),
                        new DHCP()
                 };
            }));

            var ops = new DHCPOperations<DHCP>(client);
            var dhcps = await ops.GetDHCPsAsync();

            Assert.AreEqual(2, dhcps.Count);
        }

        [TestMethod]
        public async Task GetDHCPsPaginatedAsync_ExpectedClientCall()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetPaginatedAsync<DHCP>("/ecloud/v2/dhcps").Returns(Task.Run(() =>
            {
                return new Paginated<DHCP>(client, "/ecloud/v2/dhcps", null, new ClientResponse<IList<DHCP>>()
                {
                    Body = new ClientResponseBody<IList<DHCP>>()
                    {
                        Data = new List<DHCP>()
                        {
                            new DHCP(),
                            new DHCP()
                        }
                    }
                });
            }));

            var ops = new DHCPOperations<DHCP>(client);
            var paginated = await ops.GetDHCPsPaginatedAsync();

            Assert.AreEqual(2, paginated.Items.Count);
        }

        [TestMethod]
        public async Task GetDHCPAsync_ValidParameters_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            string dhcpID = "00000000-0000-0000-0000-000000000000";

            client.GetAsync<DHCP>($"/ecloud/v2/dhcps/{dhcpID}").Returns(new DHCP()
            {
                ID = dhcpID
            });

            var ops = new DHCPOperations<DHCP>(client);
            var appliance = await ops.GetDHCPAsync(dhcpID);

            Assert.AreEqual(dhcpID, appliance.ID);
        }

        [TestMethod]
        public async Task GetDHCPAsync_InvalidDHCPID_ThrowsUKFastClientValidationException()
        {
            var ops = new DHCPOperations<DHCP>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.GetDHCPAsync(""));
        }
    }
}