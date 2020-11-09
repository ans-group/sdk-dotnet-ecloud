using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute.Routing.Handlers;
using UKFast.API.Client.ECloud.Models.V2;
using UKFast.API.Client.ECloud.Operations;
using UKFast.API.Client.Exception;
using UKFast.API.Client.Models;
using UKFast.API.Client.Response;

namespace UKFast.API.Client.ECloud.Tests.Operations
{
    [TestClass]
    public class FloatingIPOperationsTests
    {
        [TestMethod]
        public async Task GetFloatingIPsAsync_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetAllAsync(Arg.Any<UKFastClient.GetPaginatedAsyncFunc<FloatingIP>>(), null).Returns(Task.Run<IList<FloatingIP>>(() =>
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
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

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
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

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
        public async Task GetFloatingIPAsync_InvalidFloatingIPName_ThrowsUKFastClientValidationException()
        {
            var ops = new FloatingIPOperations<FloatingIP>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.GetFloatingIPAsync(""));
        }
    }
}