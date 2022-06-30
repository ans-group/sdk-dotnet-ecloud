using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V1;
using ANS.API.Client.ECloud.Operations;
using ANS.API.Client.Exception;
using ANS.API.Client.Models;

namespace ANS.API.Client.ECloud.Tests.Operations
{
    [TestClass]
    public class PodGPUProfileOperationsTests
    {
        [TestMethod]
        public async Task GetPodGPUProfilesAsync_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAllAsync(Arg.Any<ANSClient.GetPaginatedAsyncFunc<GPUProfile>>(), null).Returns(Task.Run<IList<GPUProfile>>(() =>
            {
                return new List<GPUProfile>()
                 {
                        new GPUProfile(),
                        new GPUProfile()
                 };
            }));

            var ops = new PodGPUProfileOperations<GPUProfile>(client);
            var gpuProfiles = await ops.GetPodGPUProfilesAsync(123);

            Assert.AreEqual(2, gpuProfiles.Count);
        }

        [TestMethod]
        public async Task GetPodGPUProfilesPaginatedAsync_ExpectedClientCall()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetPaginatedAsync<GPUProfile>("/ecloud/v1/pods/123/gpu-profiles").Returns(Task.Run(() =>
            {
                return new Paginated<GPUProfile>(client, "/ecloud/v1/pods/123/gpu-profiles", null, new Response.ClientResponse<System.Collections.Generic.IList<GPUProfile>>()
                {
                    Body = new Response.ClientResponseBody<System.Collections.Generic.IList<GPUProfile>>()
                    {
                        Data = new List<GPUProfile>()
                        {
                            new GPUProfile(),
                            new GPUProfile()
                        }
                    }
                });
            }));

            var ops = new PodGPUProfileOperations<GPUProfile>(client);
            var paginated = await ops.GetPodGPUProfilesPaginatedAsync(123);

            Assert.AreEqual(2, paginated.Items.Count);
        }

        [TestMethod]
        public async Task GetGPUProfilesPaginatedAsync_InvalidPodID_ThrowsANSClientValidationException()
        {
            var ops = new PodGPUProfileOperations<GPUProfile>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetPodGPUProfilesPaginatedAsync(0));
        }
    }
}