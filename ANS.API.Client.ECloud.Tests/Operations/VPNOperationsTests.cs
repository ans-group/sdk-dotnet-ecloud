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
    public class VPNOperationsTests
    {
        [TestMethod]
        public async Task CreateVPNAsync_ExpectedResult()
        {
            CreateVPNRequest req = new CreateVPNRequest()
            {
                RouterID = "rtr-abcdef12"
            };

            IANSECloudClient client = Substitute.For<IANSECloudClient>();
            client.PostAsync<VPN>("/ecloud/v2/vpns", req).Returns(new VPN()
            {
                ID = "vpn-abcd1234"
            });

            var ops = new VPNOperations<VPN>(client);
            var vpnID = await ops.CreateVPNAsync(req);

            Assert.AreEqual("vpn-abcd1234", vpnID);
        }

        [TestMethod]
        public async Task GetVPNsAsync_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAllAsync(Arg.Any<ANSClient.GetPaginatedAsyncFunc<VPN>>(), null).Returns(Task.Run<IList<VPN>>(() =>
            {
                return new List<VPN>()
                {
                    new VPN(),
                    new VPN()
                };
            }));

            var ops = new VPNOperations<VPN>(client);
            var vpns = await ops.GetVPNsAsync();

            Assert.AreEqual(2, vpns.Count);
        }

        [TestMethod]
        public async Task GetVPNsPaginatedAsync_ExpectedClientCall()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetPaginatedAsync<VPN>("/ecloud/v2/vpns").Returns(Task.Run(() =>
            {
                return new Paginated<VPN>(client, "/ecloud/v2/vpns", null,
                    new ClientResponse<IList<VPN>>()
                    {
                        Body = new ClientResponseBody<IList<VPN>>()
                        {
                            Data = new List<VPN>()
                            {
                                new VPN(),
                                new VPN()
                            }
                        }
                    });
            }));

            var ops = new VPNOperations<VPN>(client);
            var paginated = await ops.GetVPNsPaginatedAsync();

            Assert.AreEqual(2, paginated.Items.Count);
        }

        [TestMethod]
        public async Task GetVPNAsync_ValidParameters_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            string vpnID = "vpn-abcd1234";

            client.GetAsync<VPN>($"/ecloud/v2/vpns/{vpnID}").Returns(new VPN()
            {
                ID = vpnID
            });

            var ops = new VPNOperations<VPN>(client);
            var router = await ops.GetVPNAsync(vpnID);

            Assert.AreEqual("vpn-abcd1234", router.ID);
        }

        [TestMethod]
        public async Task GetVPNAsync_InvalidVPNName_ThrowsANSClientValidationException()
        {
            var ops = new VPNOperations<VPN>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetVPNAsync(""));
        }

        [TestMethod]
        public async Task GetVPNAsync_NotFound_ThrowsANSClientNotFoundRequestException()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAsync<VPN>("/ecloud/v2/vpns/vpn-abcd1234").Throws(
                new ANSClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new VPNOperations<VPN>(client);

            await Assert.ThrowsExceptionAsync<ANSClientNotFoundRequestException>(() => ops.GetVPNAsync("vpn-abcd1234"));
        }


        [TestMethod]
        public async Task UpdateVPNAsync_ExpectedResult()
        {
            UpdateVPNRequest req = new UpdateVPNRequest()
            {
                RouterID = "rtr-abcdef12"
            };

            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            var ops = new VPNOperations<VPN>(client);
            await ops.UpdateVPNAsync("vpn-abcd1234", req);

            await client.Received().PatchAsync("/ecloud/v2/vpns/vpn-abcd1234", req);
        }

        [TestMethod]
        public async Task UpdateVPNAsync_InvalidVPNID_ThrowsANSClientValidationException()
        {
            var ops = new VPNOperations<VPN>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.UpdateVPNAsync("", null));
        }

        [TestMethod]
        public async Task UpdateVPNAsync_NotFound_ThrowsANSClientNotFoundRequestException()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.PatchAsync("/ecloud/v2/vpns/vpn-abcd1234").Throws(
                new ANSClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new VPNOperations<VPN>(client);

            await Assert.ThrowsExceptionAsync<ANSClientNotFoundRequestException>(() => ops.UpdateVPNAsync("vpn-abcd1234", null));
        }

        [TestMethod]
        public async Task DeleteVPNAsync_ValidParameters()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            var ops = new VPNOperations<VPN>(client);
            await ops.DeleteVPNAsync("vpn-abcd1234");

            await client.Received().DeleteAsync("/ecloud/v2/vpns/vpn-abcd1234");
        }

        [TestMethod]
        public async Task DeleteVPNAsync_InvalidVPNID_ThrowsANSClientValidationException()
        {
            var ops = new VPNOperations<VPN>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.DeleteVPNAsync(""));
        }

        [TestMethod]
        public async Task DeleteVPNAsync_NotFound_ThrowsANSClientNotFoundRequestException()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.DeleteAsync("/ecloud/v2/vpns/vpn-abcd1234").Throws(
                new ANSClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new VPNOperations<VPN>(client);

            await Assert.ThrowsExceptionAsync<ANSClientNotFoundRequestException>(() => ops.DeleteVPNAsync("vpn-abcd1234"));
        }
    }
}