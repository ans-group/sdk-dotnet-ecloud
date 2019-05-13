using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models;
using UKFast.API.Client.ECloud.Models.Request;
using UKFast.API.Client.ECloud.Operations;
using UKFast.API.Client.Exception;
using UKFast.API.Client.Models;

namespace UKFast.API.Client.ECloud.Operations.Tests
{
    [TestClass]
    public class SolutionOperationsTests
    {
        [TestMethod]
        public async Task GetSolutionsAsync_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetAllAsync(Arg.Any<UKFastClient.GetPaginatedAsyncFunc<Solution>>(), null).Returns(Task.Run<IList<Solution>>(() =>
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
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

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
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetAsync<Solution>("/ecloud/v1/solutions/123").Returns(new Solution()
            {
                ID = 123
            });

            var ops = new SolutionOperations<Solution>(client);
            var solution = await ops.GetSolutionAsync(123);

            Assert.AreEqual(123, solution.ID);
        }

        [TestMethod]
        public async Task GetSolutionAsync_InvalidSolutionID_ThrowsUKFastClientValidationException()
        {
            var ops = new SolutionOperations<Solution>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.GetSolutionAsync(0));
        }

        [TestMethod]
        public async Task UpdateSolutionAsync_ExpectedResult()
        {
            UpdateSolutionRequest req = new UpdateSolutionRequest()
            {
                Name = "testsolution"
            };

            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            var ops = new SolutionOperations<Solution>(client);
            await ops.UpdateSolutionAsync(123, req);

            await client.Received().PatchAsync("/ecloud/v1/solutions/123", req);
        }

        [TestMethod]
        public async Task UpdateSolutionAsync_InvalidSolutionID_ThrowsUKFastClientValidationException()
        {
            var ops = new SolutionOperations<Solution>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.UpdateSolutionAsync(0, null));
        }
    }
}
