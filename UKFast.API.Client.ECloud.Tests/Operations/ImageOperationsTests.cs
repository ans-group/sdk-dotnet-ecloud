using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NSubstitute.ExceptionExtensions;
using UKFast.API.Client.ECloud.Models.V2;
using UKFast.API.Client.ECloud.Operations;
using UKFast.API.Client.Exception;
using UKFast.API.Client.Models;
using UKFast.API.Client.Response;

namespace UKFast.API.Client.ECloud.Tests.Operations
{
    [TestClass]
    public class ImageOperationsTests
    {
        [TestMethod]
        public async Task GetImagesAsync_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetAllAsync(Arg.Any<UKFastClient.GetPaginatedAsyncFunc<Image>>(), null).Returns(Task.Run<IList<Image>>(() =>
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
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

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
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

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
        public async Task GetImageAsync_InvalidImageID_ThrowsUKFastClientValidationException()
        {
            var ops = new ImageOperations<Image>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.GetImageAsync(""));
        }

        [TestMethod]
        public async Task GetImageAsync_NotFound_ThrowsUKFastClientNotFoundRequestException()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetAsync<Image>("/ecloud/v2/images/img-abcd1234").Throws(
                new UKFastClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new ImageOperations<Image>(client);

            await Assert.ThrowsExceptionAsync<UKFastClientNotFoundRequestException>(() => ops.GetImageAsync("img-abcd1234"));
        }
    }
}