using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NSubstitute.ExceptionExtensions;
using ANS.API.Client.ECloud.Models.V2;
using ANS.API.Client.ECloud.Operations;
using ANS.API.Client.Exception;
using ANS.API.Client.Models;
using ANS.API.Client.Response;

namespace ANS.API.Client.ECloud.Tests.Operations
{
    [TestClass]
    public class DHCPOperationsTests
    {
        [TestMethod]
        public async Task GetDHCPsAsync_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAllAsync(Arg.Any<ANSClient.GetPaginatedAsyncFunc<DHCP>>(), null).Returns(Task.Run<IList<DHCP>>(() =>
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
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

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
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            string dhcpID = "dhcp-abcd1234";

            client.GetAsync<DHCP>($"/ecloud/v2/dhcps/{dhcpID}").Returns(new DHCP()
            {
                ID = dhcpID
            });

            var ops = new DHCPOperations<DHCP>(client);
            var dhcp = await ops.GetDHCPAsync(dhcpID);

            Assert.AreEqual("dhcp-abcd1234", dhcp.ID);
        }

        [TestMethod]
        public async Task GetDHCPAsync_InvalidDHCPID_ThrowsANSClientValidationException()
        {
            var ops = new DHCPOperations<DHCP>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetDHCPAsync(""));
        }

        [TestMethod]
        public async Task GetDHCPAsync_NotFound_ThrowsANSClientNotFoundRequestException()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAsync<DHCP>("/ecloud/v2/dhcps/dhcp-abcd1234").Throws(
                new ANSClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new DHCPOperations<DHCP>(client);

            await Assert.ThrowsExceptionAsync<ANSClientNotFoundRequestException>(() => ops.GetDHCPAsync("dhcp-abcd1234"));
        }
    }
}