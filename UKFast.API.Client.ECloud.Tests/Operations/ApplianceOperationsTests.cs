using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V1;
using UKFast.API.Client.Exception;
using UKFast.API.Client.Models;

namespace UKFast.API.Client.ECloud.Operations.Tests
{
    [TestClass]
    public class ApplianceOperationsTests
    {
        [TestMethod]
        public async Task GetAppliancesAsync_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetAllAsync(Arg.Any<UKFastClient.GetPaginatedAsyncFunc<Appliance>>(), null).Returns(Task.Run<IList<Appliance>>(() =>
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
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

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
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetAsync<Appliance>("/ecloud/v1/appliances/00000000-0000-0000-0000-000000000000").Returns(new Appliance()
            {
                ID = "00000000-0000-0000-0000-000000000000"
            });

            var ops = new ApplianceOperations<Appliance>(client);
            var appliance = await ops.GetApplianceAsync("00000000-0000-0000-0000-000000000000");

            Assert.AreEqual("00000000-0000-0000-0000-000000000000", appliance.ID);
        }

        [TestMethod]
        public async Task GetApplianceAsync_InvalidApplianceID_ThrowsUKFastClientValidationException()
        {
            var ops = new ApplianceOperations<Appliance>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.GetApplianceAsync(""));
        }
    }
}