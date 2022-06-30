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
    public class ImageOperationsTests
    {
        [TestMethod]
        public async Task GetImagesAsync_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAllAsync(Arg.Any<ANSClient.GetPaginatedAsyncFunc<Image>>(), null).Returns(Task.Run<IList<Image>>(() =>
            {
                return new List<Image>()
                 {
                        new Image(),
                        new Image()
                 };
            }));

            var ops = new ImageOperations<Image>(client);
            var images = await ops.GetImagesAsync();

            Assert.AreEqual(2, images.Count);
        }

        [TestMethod]
        public async Task GetImagesPaginatedAsync_ExpectedClientCall()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetPaginatedAsync<Image>("/ecloud/v2/images").Returns(Task.Run(() =>
            {
                return new Paginated<Image>(client, "/ecloud/v2/images", null, new ClientResponse<IList<Image>>()
                {
                    Body = new ClientResponseBody<IList<Image>>()
                    {
                        Data = new List<Image>()
                        {
                            new Image(),
                            new Image()
                        }
                    }
                });
            }));

            var ops = new ImageOperations<Image>(client);
            var paginated = await ops.GetImagesPaginatedAsync();

            Assert.AreEqual(2, paginated.Items.Count);
        }

        [TestMethod]
        public async Task GetImageAsync_ValidParameters_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            string imageID = "img-abcd1234";

            client.GetAsync<Image>($"/ecloud/v2/images/{imageID}").Returns(new Image()
            {
                ID = imageID
            });

            var ops = new ImageOperations<Image>(client);
            var image = await ops.GetImageAsync(imageID);

            Assert.AreEqual("img-abcd1234", image.ID);
        }

        [TestMethod]
        public async Task GetImageAsync_InvalidImageID_ThrowsANSClientValidationException()
        {
            var ops = new ImageOperations<Image>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetImageAsync(""));
        }

        [TestMethod]
        public async Task GetImageAsync_NotFound_ThrowsANSClientNotFoundRequestException()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAsync<Image>("/ecloud/v2/images/img-abcd1234").Throws(
                new ANSClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new ImageOperations<Image>(client);

            await Assert.ThrowsExceptionAsync<ANSClientNotFoundRequestException>(() => ops.GetImageAsync("img-abcd1234"));
        }
    }
}