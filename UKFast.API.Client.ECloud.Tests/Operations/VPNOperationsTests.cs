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
    public class VPNOperationsTests
    {
        [TestMethod]
        public async Task CreateVPNAsync_ExpectedResult()
        {
            CreateVPNRequest req = new CreateVPNRequest()
            {
                RouterID = "rtr-abcdef12"
            };

            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();
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
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetAllAsync(Arg.Any<UKFastClient.GetPaginatedAsyncFunc<VPN>>(), null).Returns(Task.Run<IList<VPN>>(() =>
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
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

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
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

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
        public async Task GetVPNAsync_InvalidVPNName_ThrowsUKFastClientValidationException()
        {
            var ops = new VPNOperations<VPN>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.GetVPNAsync(""));
        }

        [TestMethod]
        public async Task GetVPNAsync_NotFound_ThrowsUKFastClientNotFoundRequestException()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetAsync<VPN>("/ecloud/v2/vpns/vpn-abcd1234").Throws(
                new UKFastClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new VPNOperations<VPN>(client);

            await Assert.ThrowsExceptionAsync<UKFastClientNotFoundRequestException>(() => ops.GetVPNAsync("vpn-abcd1234"));
        }


        [TestMethod]
        public async Task UpdateVPNAsync_ExpectedResult()
        {
            UpdateVPNRequest req = new UpdateVPNRequest()
            {
                RouterID = "rtr-abcdef12"
            };

            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            var ops = new VPNOperations<VPN>(client);
            await ops.UpdateVPNAsync("vpn-abcd1234", req);

            await client.Received().PatchAsync("/ecloud/v2/vpns/vpn-abcd1234", req);
        }

        [TestMethod]
        public async Task UpdateVPNAsync_InvalidVPNID_ThrowsUKFastClientValidationException()
        {
            var ops = new VPNOperations<VPN>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.UpdateVPNAsync("", null));
        }

        [TestMethod]
        public async Task UpdateVPNAsync_NotFound_ThrowsUKFastClientNotFoundRequestException()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.PatchAsync("/ecloud/v2/vpns/vpn-abcd1234").Throws(
                new UKFastClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new VPNOperations<VPN>(client);

            await Assert.ThrowsExceptionAsync<UKFastClientNotFoundRequestException>(() => ops.UpdateVPNAsync("vpn-abcd1234", null));
        }

        [TestMethod]
        public async Task DeleteVPNAsync_ValidParameters()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            var ops = new VPNOperations<VPN>(client);
            await ops.DeleteVPNAsync("vpn-abcd1234");

            await client.Received().DeleteAsync("/ecloud/v2/vpns/vpn-abcd1234");
        }

        [TestMethod]
        public async Task DeleteVPNAsync_InvalidVPNID_ThrowsUKFastClientValidationException()
        {
            var ops = new VPNOperations<VPN>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.DeleteVPNAsync(""));
        }

        [TestMethod]
        public async Task DeleteVPNAsync_NotFound_ThrowsUKFastClientNotFoundRequestException()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.DeleteAsync("/ecloud/v2/vpns/vpn-abcd1234").Throws(
                new UKFastClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new VPNOperations<VPN>(client);

            await Assert.ThrowsExceptionAsync<UKFastClientNotFoundRequestException>(() => ops.DeleteVPNAsync("vpn-abcd1234"));
        }
    }
}