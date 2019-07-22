using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models;
using UKFast.API.Client.ECloud.Operations;
using UKFast.API.Client.Exception;
using UKFast.API.Client.Models;

namespace UKFast.API.Client.ECloud.Tests.Operations
{
    [TestClass]
    public class PodGPUProfileOperationsTests
    {
        public async Task GetPodGPUProfilesAsync_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetAllAsync(Arg.Any<UKFastClient.GetPaginatedAsyncFunc<GPUProfile>>(), null).Returns(Task.Run<IList<GPUProfile>>(() =>
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
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetPaginatedAsync<GPUProfile>("/ecloud/v1/pods/123/gpu_profiles").Returns(Task.Run(() =>
            {
                return new Paginated<GPUProfile>(client, "/ecloud/v1/pods/123/gpu_profiles", null, new Response.ClientResponse<System.Collections.Generic.IList<GPUProfile>>()
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
        public async Task GetGPUProfilesPaginatedAsync_InvalidPodID_ThrowsUKFastClientValidationException()
        {
            var ops = new PodGPUProfileOperations<GPUProfile>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.GetPodGPUProfilesPaginatedAsync(0));
        }
    }
}
