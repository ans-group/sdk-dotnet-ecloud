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
    public class ImageMetadataOperationsTests
    {
        [TestMethod]
        public async Task GetImageMetadataAsync_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAllAsync(Arg.Any<ANSClient.GetPaginatedAsyncFunc<ImageMetadata>>(), null).Returns(Task.Run<IList<ImageMetadata>>(() =>
            {
                return new List<ImageMetadata>()
                 {
                        new ImageMetadata(),
                        new ImageMetadata()
                 };
            }));

            var ops = new ImageMetadataOperations<ImageMetadata>(client);
            var metadata = await ops.GetImageMetadataAsync("img-abcdef12");

            Assert.AreEqual(2, metadata.Count);
        }

        [TestMethod]
        public async Task GetImageMetadataPaginatedAsync_ExpectedClientCall()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetPaginatedAsync<ImageMetadata>("/ecloud/v2/images/img-abcdef12/metadata").Returns(Task.Run(() =>
            {
                return new Paginated<ImageMetadata>(client, "/ecloud/v2/images/img-abcdef12/metadata", null, new ClientResponse<IList<ImageMetadata>>()
                {
                    Body = new ClientResponseBody<IList<ImageMetadata>>()
                    {
                        Data = new List<ImageMetadata>()
                        {
                            new ImageMetadata(),
                            new ImageMetadata()
                        }
                    }
                });
            }));

            var ops = new ImageMetadataOperations<ImageMetadata>(client);
            var paginated = await ops.GetImageMetadataPaginatedAsync("img-abcdef12");

            Assert.AreEqual(2, paginated.Items.Count);
        }

        [TestMethod]
        public async Task GetImageMetadataPaginatedAsync_InvalidImageID_ThrowsANSClientValidationException()
        {
            var ops = new ImageMetadataOperations<ImageMetadata>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetImageMetadataPaginatedAsync(""));
        }
    }
}