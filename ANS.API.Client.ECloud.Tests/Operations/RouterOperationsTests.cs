using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V2;
using ANS.API.Client.ECloud.Models.V2.Request;
using ANS.API.Client.ECloud.Operations;
using ANS.API.Client.Exception;
using ANS.API.Client.Models;
using ANS.API.Client.Response;

namespace ANS.API.Client.ECloud.Tests.Operations
{
    [TestClass]
    public class RouterOperationsTests
    {
        [TestMethod]
        public async Task CreateRouterAsync_ExpectedResult()
        {
            CreateRouterRequest req = new CreateRouterRequest()
            {
                Name = "test router"
            };

            IANSECloudClient client = Substitute.For<IANSECloudClient>();
            client.PostAsync<Router>("/ecloud/v2/routers", req).Returns(new Router()
            {
                ID = "rtr-abcd1234"
            });

            var ops = new RouterOperations<Router>(client);
            var routerID = await ops.CreateRouterAsync(req);

            Assert.AreEqual("rtr-abcd1234", routerID);
        }

        [TestMethod]
        public async Task GetRoutersAsync_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAllAsync(Arg.Any<ANSClient.GetPaginatedAsyncFunc<Router>>(), null).Returns(Task.Run<IList<Router>>(() =>
            {
                return new List<Router>()
                {
                    new Router(),
                    new Router()
                };
            }));

            var ops = new RouterOperations<Router>(client);
            var routers = await ops.GetRoutersAsync();

            Assert.AreEqual(2, routers.Count);
        }

        [TestMethod]
        public async Task GetRoutersPaginatedAsync_ExpectedClientCall()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetPaginatedAsync<Router>("/ecloud/v2/routers").Returns(Task.Run(() =>
            {
                return new Paginated<Router>(client, "/ecloud/v2/routers", null,
                    new ClientResponse<IList<Router>>()
                    {
                        Body = new ClientResponseBody<IList<Router>>()
                        {
                            Data = new List<Router>()
                            {
                                new Router(),
                                new Router()
                            }
                        }
                    });
            }));

            var ops = new RouterOperations<Router>(client);
            var paginated = await ops.GetRoutersPaginatedAsync();

            Assert.AreEqual(2, paginated.Items.Count);
        }

        [TestMethod]
        public async Task GetRouterAsync_ValidParameters_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            string routerID = "rtr-abcd1234";

            client.GetAsync<Router>($"/ecloud/v2/routers/{routerID}").Returns(new Router()
            {
                ID = routerID
            });

            var ops = new RouterOperations<Router>(client);
            var router = await ops.GetRouterAsync(routerID);

            Assert.AreEqual("rtr-abcd1234", router.ID);
        }

        [TestMethod]
        public async Task GetRouterAsync_InvalidRouterName_ThrowsANSClientValidationException()
        {
            var ops = new RouterOperations<Router>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetRouterAsync(""));
        }

        [TestMethod]
        public async Task GetRouterAsync_NotFound_ThrowsANSClientNotFoundRequestException()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAsync<Router>("/ecloud/v2/routers/rtr-abcd1234").Throws(
                new ANSClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new RouterOperations<Router>(client);

            await Assert.ThrowsExceptionAsync<ANSClientNotFoundRequestException>(() => ops.GetRouterAsync("rtr-abcd1234"));
        }

        [TestMethod]
        public async Task UpdateRouterAsync_ExpectedResult()
        {
            UpdateRouterRequest req = new UpdateRouterRequest()
            {
                Name = "test-router"
            };

            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            var ops = new RouterOperations<Router>(client);
            await ops.UpdateRouterAsync("rtr-abcd1234", req);

            await client.Received().PatchAsync("/ecloud/v2/routers/rtr-abcd1234", req);
        }

        [TestMethod]
        public async Task UpdateRouterAsync_InvalidRouterID_ThrowsANSClientValidationException()
        {
            var ops = new RouterOperations<Router>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.UpdateRouterAsync("", null));
        }

        [TestMethod]
        public async Task UpdateRouterAsync_NotFound_ThrowsANSClientNotFoundRequestException()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.PatchAsync("/ecloud/v2/routers/rtr-abcd1234").Throws(
                new ANSClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new RouterOperations<Router>(client);

            await Assert.ThrowsExceptionAsync<ANSClientNotFoundRequestException>(() => ops.UpdateRouterAsync("rtr-abcd1234", null));
        }

        [TestMethod]
        public async Task DeleteRouterAsync_ValidParameters()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            var ops = new RouterOperations<Router>(client);
            await ops.DeleteRouterAsync("rtr-abcd1234");

            await client.Received().DeleteAsync("/ecloud/v2/routers/rtr-abcd1234");
        }

        [TestMethod]
        public async Task DeleteRouterAsync_InvalidRouterID_ThrowsANSClientValidationException()
        {
            var ops = new RouterOperations<Router>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.DeleteRouterAsync(""));
        }

        [TestMethod]
        public async Task DeleteRouterAsync_NotFound_ThrowsANSClientNotFoundRequestException()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.DeleteAsync("/ecloud/v2/routers/rtr-abcd1234").Throws(
                new ANSClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new RouterOperations<Router>(client);

            await Assert.ThrowsExceptionAsync<ANSClientNotFoundRequestException>(() => ops.DeleteRouterAsync("rtr-abcd1234"));
        }
    }
}