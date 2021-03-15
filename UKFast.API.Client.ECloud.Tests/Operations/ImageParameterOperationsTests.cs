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
    public class ImageParameterOperationsTests
    {
        [TestMethod]
        public async Task GetImageParametersAsync_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetAllAsync(Arg.Any<UKFastClient.GetPaginatedAsyncFunc<ImageParameter>>(), null).Returns(Task.Run<IList<ImageParameter>>(() =>
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
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

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
        public async Task GetImageParametersPaginatedAsync_InvalidImageID_ThrowsUKFastClientValidationException()
        {
            var ops = new ImageParameterOperations<ImageParameter>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.GetImageParametersPaginatedAsync(""));
        }
    }
}