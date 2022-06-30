using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ANS.API.Client.ECloud.Models.V1;
using ANS.API.Client.ECloud.Models.V1.Request;
using ANS.API.Client.ECloud.Operations;
using ANS.API.Client.Exception;
using ANS.API.Client.Models;

namespace ANS.API.Client.ECloud.Tests.Operations
{
    [TestClass]
    public class SolutionOperationsTests
    {
        [TestMethod]
        public async Task GetSolutionsAsync_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAllAsync(Arg.Any<ANSClient.GetPaginatedAsyncFunc<Solution>>(), null).Returns(Task.Run<IList<Solution>>(() =>
            {
                return new List<Solution>()
                {
                    new Solution(),
                    new Solution()
                };
            }));

            var ops = new SolutionOperations<Solution>(client);
            var solutions = await ops.GetSolutionsAsync();

            Assert.AreEqual(2, solutions.Count);
        }

        [TestMethod]
        public async Task GetSolutionsPaginatedAsync_ExpectedClientCall()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetPaginatedAsync<Solution>("/ecloud/v1/solutions").Returns(Task.Run(() =>
            {
                return new Paginated<Solution>(client, "/ecloud/v1/solutions", null, new Response.ClientResponse<System.Collections.Generic.IList<Solution>>()
                {
                    Body = new Response.ClientResponseBody<System.Collections.Generic.IList<Solution>>()
                    {
                        Data = new List<Solution>()
                        {
                            new Solution(),
                            new Solution()
                        }
                    }
                });
            }));

            var ops = new SolutionOperations<Solution>(client);
            var paginated = await ops.GetSolutionsPaginatedAsync();

            Assert.AreEqual(2, paginated.Items.Count);
        }

        [TestMethod]
        public async Task GetSolutionAsync_ValidParameters_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAsync<Solution>("/ecloud/v1/solutions/123").Returns(new Solution()
            {
                ID = 123
            });

            var ops = new SolutionOperations<Solution>(client);
            var solution = await ops.GetSolutionAsync(123);

            Assert.AreEqual(123, solution.ID);
        }

        [TestMethod]
        public async Task GetSolutionAsync_InvalidSolutionID_ThrowsANSClientValidationException()
        {
            var ops = new SolutionOperations<Solution>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetSolutionAsync(0));
        }

        [TestMethod]
        public async Task UpdateSolutionAsync_ExpectedResult()
        {
            UpdateSolutionRequest req = new UpdateSolutionRequest()
            {
                Name = "testsolution"
            };

            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            var ops = new SolutionOperations<Solution>(client);
            await ops.UpdateSolutionAsync(123, req);

            await client.Received().PatchAsync("/ecloud/v1/solutions/123", req);
        }

        [TestMethod]
        public async Task UpdateSolutionAsync_InvalidSolutionID_ThrowsANSClientValidationException()
        {
            var ops = new SolutionOperations<Solution>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.UpdateSolutionAsync(0, null));
        }
    }
}