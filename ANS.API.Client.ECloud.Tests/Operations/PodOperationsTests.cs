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
    public class PodOperationsTests
    {
        [TestMethod]
        public async Task GetPodsAsync_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAllAsync(Arg.Any<ANSClient.GetPaginatedAsyncFunc<Pod>>(), null).Returns(Task.Run<IList<Pod>>(() =>
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
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

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
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAsync<Pod>("/ecloud/v1/pods/123").Returns(new Pod()
            {
                ID = 123
            });

            var ops = new PodOperations<Pod>(client);
            var pod = await ops.GetPodAsync(123);

            Assert.AreEqual(123, pod.ID);
        }

        [TestMethod]
        public async Task GetPodAsync_InvalidPodID_ThrowsANSClientValidationException()
        {
            var ops = new PodOperations<Pod>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetPodAsync(0));
        }
    }
}