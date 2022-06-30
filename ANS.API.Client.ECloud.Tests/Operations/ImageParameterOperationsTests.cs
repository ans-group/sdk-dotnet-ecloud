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
    public class ImageParameterOperationsTests
    {
        [TestMethod]
        public async Task GetImageParametersAsync_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAllAsync(Arg.Any<ANSClient.GetPaginatedAsyncFunc<ImageParameter>>(), null).Returns(Task.Run<IList<ImageParameter>>(() =>
            {
                return new List<ImageParameter>()
                 {
                        new ImageParameter(),
                        new ImageParameter()
                 };
            }));

            var ops = new ImageParameterOperations<ImageParameter>(client);
            var parameters = await ops.GetImageParametersAsync("img-abcdef12");

            Assert.AreEqual(2, parameters.Count);
        }

        [TestMethod]
        public async Task GetImageParametersPaginatedAsync_ExpectedClientCall()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetPaginatedAsync<ImageParameter>("/ecloud/v2/images/img-abcdef12/parameters").Returns(Task.Run(() =>
            {
                return new Paginated<ImageParameter>(client, "/ecloud/v2/images/img-abcdef12/parameters", null, new ClientResponse<IList<ImageParameter>>()
                {
                    Body = new ClientResponseBody<IList<ImageParameter>>()
                    {
                        Data = new List<ImageParameter>()
                        {
                            new ImageParameter(),
                            new ImageParameter()
                        }
                    }
                });
            }));

            var ops = new ImageParameterOperations<ImageParameter>(client);
            var paginated = await ops.GetImageParametersPaginatedAsync("img-abcdef12");

            Assert.AreEqual(2, paginated.Items.Count);
        }

        [TestMethod]
        public async Task GetImageParametersPaginatedAsync_InvalidImageID_ThrowsANSClientValidationException()
        {
            var ops = new ImageParameterOperations<ImageParameter>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetImageParametersPaginatedAsync(""));
        }
    }
}