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
    public class ApplianceOperationsTests
    {
        [TestMethod]
        public async Task GetAppliancesAsync_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAllAsync(Arg.Any<ANSClient.GetPaginatedAsyncFunc<Appliance>>(), null).Returns(Task.Run<IList<Appliance>>(() =>
            {
                return new List<Appliance>()
                 {
                        new Appliance(),
                        new Appliance()
                 };
            }));

            var ops = new ApplianceOperations<Appliance>(client);
            var appliances = await ops.GetAppliancesAsync();

            Assert.AreEqual(2, appliances.Count);
        }

        [TestMethod]
        public async Task GetAppliancesPaginatedAsync_ExpectedClientCall()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetPaginatedAsync<Appliance>("/ecloud/v1/appliances").Returns(Task.Run(() =>
            {
                return new Paginated<Appliance>(client, "/ecloud/v1/appliances", null, new Response.ClientResponse<System.Collections.Generic.IList<Appliance>>()
                {
                    Body = new Response.ClientResponseBody<System.Collections.Generic.IList<Appliance>>()
                    {
                        Data = new List<Appliance>()
                        {
                            new Appliance(),
                            new Appliance()
                        }
                    }
                });
            }));

            var ops = new ApplianceOperations<Appliance>(client);
            var paginated = await ops.GetAppliancesPaginatedAsync();

            Assert.AreEqual(2, paginated.Items.Count);
        }

        [TestMethod]
        public async Task GetApplianceAsync_ValidParameters_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAsync<Appliance>("/ecloud/v1/appliances/00000000-0000-0000-0000-000000000000").Returns(new Appliance()
            {
                ID = "00000000-0000-0000-0000-000000000000"
            });

            var ops = new ApplianceOperations<Appliance>(client);
            var appliance = await ops.GetApplianceAsync("00000000-0000-0000-0000-000000000000");

            Assert.AreEqual("00000000-0000-0000-0000-000000000000", appliance.ID);
        }

        [TestMethod]
        public async Task GetApplianceAsync_InvalidApplianceID_ThrowsANSClientValidationException()
        {
            var ops = new ApplianceOperations<Appliance>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetApplianceAsync(""));
        }
    }
}