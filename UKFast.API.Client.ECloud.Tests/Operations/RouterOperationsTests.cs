using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V2;
using UKFast.API.Client.ECloud.Models.V2.Request;
using UKFast.API.Client.ECloud.Operations;
using UKFast.API.Client.Exception;
using UKFast.API.Client.Models;
using UKFast.API.Client.Response;

namespace UKFast.API.Client.ECloud.Tests.Operations
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

            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();
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
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetAllAsync(Arg.Any<UKFastClient.GetPaginatedAsyncFunc<Router>>(), null).Returns(Task.Run<IList<Router>>(() =>
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
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

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
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

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
        public async Task GetRouterAsync_InvalidRouterName_ThrowsUKFastClientValidationException()
        {
            var ops = new RouterOperations<Router>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.GetRouterAsync(""));
        }

        [TestMethod]
        public async Task GetRouterAsync_NotFound_ThrowsUKFastClientNotFoundRequestException()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetAsync<Router>("/ecloud/v2/routers/rtr-abcd1234").Throws(
                new UKFastClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new RouterOperations<Router>(client);

            await Assert.ThrowsExceptionAsync<UKFastClientNotFoundRequestException>(() => ops.GetRouterAsync("rtr-abcd1234"));
        }

        [TestMethod]
        public async Task UpdateRouterAsync_ExpectedResult()
        {
            UpdateRouterRequest req = new UpdateRouterRequest()
            {
                Name = "test-router"
            };

            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            var ops = new RouterOperations<Router>(client);
            await ops.UpdateRouterAsync("rtr-abcd1234", req);

            await client.Received().PatchAsync("/ecloud/v2/routers/rtr-abcd1234", req);
        }

        [TestMethod]
        public async Task UpdateRouterAsync_InvalidRouterID_ThrowsUKFastClientValidationException()
        {
            var ops = new RouterOperations<Router>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.UpdateRouterAsync("", null));
        }

        [TestMethod]
        public async Task UpdateRouterAsync_NotFound_ThrowsUKFastClientNotFoundRequestException()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.PatchAsync("/ecloud/v2/routers/rtr-abcd1234").Throws(
                new UKFastClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new RouterOperations<Router>(client);

            await Assert.ThrowsExceptionAsync<UKFastClientNotFoundRequestException>(() => ops.UpdateRouterAsync("rtr-abcd1234", null));
        }

        [TestMethod]
        public async Task DeleteRouterAsync_ValidParameters()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            var ops = new RouterOperations<Router>(client);
            await ops.DeleteRouterAsync("rtr-abcd1234");

            await client.Received().DeleteAsync("/ecloud/v2/routers/rtr-abcd1234");
        }

        [TestMethod]
        public async Task DeleteRouterAsync_InvalidRouterID_ThrowsUKFastClientValidationException()
        {
            var ops = new RouterOperations<Router>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.DeleteRouterAsync(""));
        }

        [TestMethod]
        public async Task DeleteRouterAsync_NotFound_ThrowsUKFastClientNotFoundRequestException()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.DeleteAsync("/ecloud/v2/routers/rtr-abcd1234").Throws(
                new UKFastClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new RouterOperations<Router>(client);

            await Assert.ThrowsExceptionAsync<UKFastClientNotFoundRequestException>(() => ops.DeleteRouterAsync("rtr-abcd1234"));
        }
    }
}