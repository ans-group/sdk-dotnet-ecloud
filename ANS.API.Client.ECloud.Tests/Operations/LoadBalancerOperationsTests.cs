using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NSubstitute.ExceptionExtensions;
using ANS.API.Client.ECloud.Models.V2;
using ANS.API.Client.ECloud.Models.V2.Request;
using ANS.API.Client.ECloud.Operations;
using ANS.API.Client.Exception;
using ANS.API.Client.Models;
using ANS.API.Client.Response;

namespace ANS.API.Client.ECloud.Tests.Operations
{
    [TestClass]
    public class LoadBalancerOperationsTests
    {
        [TestMethod]
        public async Task CreateLoadBalancerClusterAsync_ExpectedResult()
        {
            CreateLoadBalancerClusterRequest req = new CreateLoadBalancerClusterRequest()
            {
                Name = "test lbc"
            };

            IANSECloudClient client = Substitute.For<IANSECloudClient>();
            client.PostAsync<LoadBalancerCluster>("/ecloud/v2/lbcs", req).Returns(new LoadBalancerCluster()
            {
                ID = "lbc-abcd1234"
            });

            var ops = new LoadBalancerOperations<LoadBalancerCluster>(client);
            var routerID = await ops.CreateLoadBalancerClusterAsync(req);

            Assert.AreEqual("lbc-abcd1234", routerID);
        }

        [TestMethod]
        public async Task GetLoadBalancerClustersAsync_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAllAsync(Arg.Any<ANSClient.GetPaginatedAsyncFunc<LoadBalancerCluster>>(), null).Returns(Task.Run<IList<LoadBalancerCluster>>(() =>
            {
                return new List<LoadBalancerCluster>()
                {
                    new LoadBalancerCluster(),
                    new LoadBalancerCluster()
                };
            }));

            var ops = new LoadBalancerOperations<LoadBalancerCluster>(client);
            var lbcs = await ops.GetLoadBalancerClustersAsync();

            Assert.AreEqual(2, lbcs.Count);
        }

        [TestMethod]
        public async Task GetLoadBalancerClustersPaginatedAsync_ExpectedClientCall()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetPaginatedAsync<LoadBalancerCluster>("/ecloud/v2/lbcs").Returns(Task.Run(() =>
            {
                return new Paginated<LoadBalancerCluster>(client, "/ecloud/v2/lbcs", null,
                    new ClientResponse<IList<LoadBalancerCluster>>()
                    {
                        Body = new ClientResponseBody<IList<LoadBalancerCluster>>()
                        {
                            Data = new List<LoadBalancerCluster>()
                            {
                                new LoadBalancerCluster(),
                                new LoadBalancerCluster()
                            }
                        }
                    });
            }));

            var ops = new LoadBalancerOperations<LoadBalancerCluster>(client);
            var paginated = await ops.GetLoadBalancerClustersPaginatedAsync();

            Assert.AreEqual(2, paginated.Items.Count);
        }

        [TestMethod]
        public async Task GetLoadBalancerClusterAsync_ValidParameters_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            string lbcID = "lbc-abcd1234";

            client.GetAsync<LoadBalancerCluster>($"/ecloud/v2/lbcs/{lbcID}").Returns(new LoadBalancerCluster()
            {
                ID = lbcID
            });

            var ops = new LoadBalancerOperations<LoadBalancerCluster>(client);
            var lbc = await ops.GetLoadBalancerClusterAsync(lbcID);

            Assert.AreEqual("lbc-abcd1234", lbc.ID);
        }

        [TestMethod]
        public async Task GetLoadBalancerClusterAsync_InvalidLoadBalancerClusterName_ThrowsANSClientValidationException()
        {
            var ops = new LoadBalancerOperations<LoadBalancerCluster>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetLoadBalancerClusterAsync(""));
        }

        [TestMethod]
        public async Task GetLoadBalancerClusterAsync_NotFound_ThrowsANSClientNotFoundRequestException()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAsync<LoadBalancerCluster>("/ecloud/v2/lbcs/lbc-abcd1234").Throws(
                new ANSClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new LoadBalancerOperations<LoadBalancerCluster>(client);

            await Assert.ThrowsExceptionAsync<ANSClientNotFoundRequestException>(() => ops.GetLoadBalancerClusterAsync("lbc-abcd1234"));
        }

        [TestMethod]
        public async Task UpdateLoadBalancerClusterAsync_ExpectedResult()
        {
            UpdateLoadBalancerClusterRequest req = new UpdateLoadBalancerClusterRequest()
            {
                Name = "test lbc"
            };

            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            var ops = new LoadBalancerOperations<LoadBalancerCluster>(client);
            await ops.UpdateLoadBalancerClusterAsync("lbc-abcd1234", req);

            await client.Received().PatchAsync("/ecloud/v2/lbcs/lbc-abcd1234", req);
        }

        [TestMethod]
        public async Task UpdateLoadBalancerClusterAsync_InvalidLoadBalancerClusterID_ThrowsANSClientValidationException()
        {
            var ops = new LoadBalancerOperations<LoadBalancerCluster>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.UpdateLoadBalancerClusterAsync("", null));
        }

        [TestMethod]
        public async Task UpdateLoadBalancerClusterAsync_NotFound_ThrowsANSClientNotFoundRequestException()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.PatchAsync("/ecloud/v2/lbcs/lbc-abcd1234").Throws(
                new ANSClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new LoadBalancerOperations<LoadBalancerCluster>(client);

            await Assert.ThrowsExceptionAsync<ANSClientNotFoundRequestException>(() => ops.UpdateLoadBalancerClusterAsync("lbc-abcd1234", null));
        }

        [TestMethod]
        public async Task DeleteLoadBalancerClusterAsync_ValidParameters()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            var ops = new LoadBalancerOperations<LoadBalancerCluster>(client);
            await ops.DeleteLoadBalancerClusterAsync("lbc-abcd1234");

            await client.Received().DeleteAsync("/ecloud/v2/lbcs/lbc-abcd1234");
        }

        [TestMethod]
        public async Task DeleteLoadBalancerClusterAsync_InvalidLoadBalancerClusterID_ThrowsANSClientValidationException()
        {
            var ops = new LoadBalancerOperations<LoadBalancerCluster>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.DeleteLoadBalancerClusterAsync(""));
        }

        [TestMethod]
        public async Task DeleteLoadBalancerClusterAsync_NotFound_ThrowsANSClientNotFoundRequestException()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.DeleteAsync("/ecloud/v2/lbcs/lbc-abcd1234").Throws(
                new ANSClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new LoadBalancerOperations<LoadBalancerCluster>(client);

            await Assert.ThrowsExceptionAsync<ANSClientNotFoundRequestException>(() => ops.DeleteLoadBalancerClusterAsync("lbc-abcd1234"));
        }
    }
}