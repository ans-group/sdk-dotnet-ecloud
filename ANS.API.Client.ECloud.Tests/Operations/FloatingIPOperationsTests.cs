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
    public class FloatingIPOperationsTests
    {
        [TestMethod]
        public async Task GetFloatingIPsAsync_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAllAsync(Arg.Any<ANSClient.GetPaginatedAsyncFunc<FloatingIP>>(), null).Returns(Task.Run<IList<FloatingIP>>(() =>
            {
                return new List<FloatingIP>()
                {
                    new FloatingIP(),
                    new FloatingIP()
                };
            }));

            var ops = new FloatingIPOperations<FloatingIP>(client);
            var floatingIPs = await ops.GetFloatingIPsAsync();

            Assert.AreEqual(2, floatingIPs.Count);
        }

        [TestMethod]
        public async Task GetFloatingIPsPaginatedAsync_ExpectedClientCall()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetPaginatedAsync<FloatingIP>("/ecloud/v2/floating-ips").Returns(Task.Run(() =>
            {
                return new Paginated<FloatingIP>(client, "/ecloud/v2/floating-ips", null,
                    new ClientResponse<IList<FloatingIP>>()
                    {
                        Body = new ClientResponseBody<IList<FloatingIP>>()
                        {
                            Data = new List<FloatingIP>()
                            {
                                new FloatingIP(),
                                new FloatingIP()
                            }
                        }
                    });
            }));

            var ops = new FloatingIPOperations<FloatingIP>(client);
            var paginated = await ops.GetFloatingIPsPaginatedAsync();

            Assert.AreEqual(2, paginated.Items.Count);
        }

        [TestMethod]
        public async Task GetFloatingIPAsync_ValidParameters_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            string floatingIPID = "fip-abcd1234";

            client.GetAsync<FloatingIP>($"/ecloud/v2/floating-ips/{floatingIPID}").Returns(new FloatingIP()
            {
                ID = floatingIPID
            });

            var ops = new FloatingIPOperations<FloatingIP>(client);
            var floatingIP = await ops.GetFloatingIPAsync(floatingIPID);

            Assert.AreEqual("fip-abcd1234", floatingIP.ID);
        }

        [TestMethod]
        public async Task GetFloatingIPAsync_InvalidFloatingIPName_ThrowsANSClientValidationException()
        {
            var ops = new FloatingIPOperations<FloatingIP>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetFloatingIPAsync(""));
        }

        [TestMethod]
        public async Task GetFloatingIPAsync_NotFound_ThrowsANSClientNotFoundRequestException()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAsync<FloatingIP>("/ecloud/v2/floating-ips/fip-abcd1234").Throws(
                new ANSClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new FloatingIPOperations<FloatingIP>(client);

            await Assert.ThrowsExceptionAsync<ANSClientNotFoundRequestException>(() => ops.GetFloatingIPAsync("fip-abcd1234"));
        }
    }
}