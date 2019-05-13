using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models;
using UKFast.API.Client.ECloud.Models.Request;
using UKFast.API.Client.ECloud.Operations;
using UKFast.API.Client.Exception;
using UKFast.API.Client.Models;

namespace UKFast.API.Client.ECloud.Operations.Tests
{
    [TestClass]
    public class PodOperationsTests
    {
        [TestMethod]
        public async Task GetPodsAsync_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetAllAsync(Arg.Any<UKFastClient.GetPaginatedAsyncFunc<Pod>>(), null).Returns(Task.Run<IList<Pod>>(() =>
            {
                return new List<Pod>()
                {
                    new Pod(),
                    new Pod()
                };
            }));

            var ops = new PodOperations<Pod>(client);
            var pods = await ops.GetPodsAsync();

            Assert.AreEqual(2, pods.Count);
        }

        [TestMethod]
        public async Task GetPodsPaginatedAsync_ExpectedClientCall()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetPaginatedAsync<Pod>("/ecloud/v1/pods").Returns(Task.Run(() =>
            {
                return new Paginated<Pod>(client, "/ecloud/v1/pods", null, new Response.ClientResponse<System.Collections.Generic.IList<Pod>>()
                {
                    Body = new Response.ClientResponseBody<System.Collections.Generic.IList<Pod>>()
                    {
                        Data = new List<Pod>()
                        {
                            new Pod(),
                            new Pod()
                        }
                    }
                });
            }));

            var ops = new PodOperations<Pod>(client);
            var paginated = await ops.GetPodsPaginatedAsync();

            Assert.AreEqual(2, paginated.Items.Count);
        }

        [TestMethod]
        public async Task GetPodAsync_ValidParameters_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetAsync<Pod>("/ecloud/v1/pods/123").Returns(new Pod()
            {
                ID = 123
            });

            var ops = new PodOperations<Pod>(client);
            var pod = await ops.GetPodAsync(123);

            Assert.AreEqual(123, pod.ID);
        }

        [TestMethod]
        public async Task GetPodAsync_InvalidPodID_ThrowsUKFastClientValidationException()
        {
            var ops = new PodOperations<Pod>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.GetPodAsync(0));
        }
    }
}
