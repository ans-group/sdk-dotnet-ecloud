using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V1;
using UKFast.API.Client.ECloud.Operations;
using UKFast.API.Client.Exception;
using UKFast.API.Client.Models;

namespace UKFast.API.Client.ECloud.Tests.Operations
{
    [TestClass]
    public class PodApplianceOperationsTests
    {
        [TestMethod]
        public async Task GetPodAppliancesAsync_ExpectedResult()
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

            var ops = new PodApplianceOperations<Appliance>(client);
            var appliances = await ops.GetPodAppliancesAsync(123);

            Assert.AreEqual(2, appliances.Count);
        }

        [TestMethod]
        public async Task GetAppliancesPaginatedAsync_ExpectedClientCall()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetPaginatedAsync<Appliance>("/ecloud/v1/pods/123/appliances").Returns(Task.Run(() =>
            {
                return new Paginated<Appliance>(client, "/ecloud/v1/pods/123/appliances", null, new Response.ClientResponse<System.Collections.Generic.IList<Appliance>>()
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

            var ops = new PodApplianceOperations<Appliance>(client);
            var paginated = await ops.GetPodAppliancesPaginatedAsync(123);

            Assert.AreEqual(2, paginated.Items.Count);
        }

        [TestMethod]
        public async Task GetAppliancesPaginatedAsync_InvalidPodID_ThrowsUKFastClientValidationException()
        {
            var ops = new PodApplianceOperations<Appliance>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.GetPodAppliancesPaginatedAsync(0));
        }
    }
}