using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V1;
using UKFast.API.Client.ECloud.Models.V1.Request;
using UKFast.API.Client.ECloud.Operations;
using UKFast.API.Client.Exception;
using UKFast.API.Client.Models;

namespace UKFast.API.Client.ECloud.Tests.Operations
{
    [TestClass]
    public class SolutionTagOperationsTests
    {
        [TestMethod]
        public async Task CreateSolutionTagAsync_ExpectedResult()
        {
            CreateTagRequest req = new CreateTagRequest()
            {
                Key = "testkey",
                Value = "testvalue"
            };

            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();
            client.PostAsync<Tag>("/ecloud/v1/solutions/123/tags", req).Returns(new Tag()
            {
                Key = "testkey"
            });

            var ops = new SolutionTagOperations<Tag>(client);
            var tagKey = await ops.CreateSolutionTagAsync(123, req);

            Assert.AreEqual("testkey", tagKey);
        }

        [TestMethod]
        public async Task CreateSolutionTagAsync_InvalidSolutionID_ThrowsUKFastClientValidationException()
        {
            var ops = new SolutionTagOperations<Tag>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.CreateSolutionTagAsync(0, null));
        }

        [TestMethod]
        public async Task GetSolutionTagsAsync_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetAllAsync(Arg.Any<UKFastClient.GetPaginatedAsyncFunc<Tag>>(), null).Returns(Task.Run<IList<Tag>>(() =>
            {
                return new List<Tag>()
                 {
                        new Tag(),
                        new Tag()
                 };
            }));

            var ops = new SolutionTagOperations<Tag>(client);
            var tags = await ops.GetSolutionTagsAsync(123);

            Assert.AreEqual(2, tags.Count);
        }

        [TestMethod]
        public async Task GetSolutionTagsPaginatedAsync_ExpectedClientCall()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetPaginatedAsync<Tag>("/ecloud/v1/solutions/123/tags").Returns(Task.Run(() =>
            {
                return new Paginated<Tag>(client, "/ecloud/v1/solutions/123/tags", null, new Response.ClientResponse<System.Collections.Generic.IList<Tag>>()
                {
                    Body = new Response.ClientResponseBody<System.Collections.Generic.IList<Tag>>()
                    {
                        Data = new List<Tag>()
                        {
                            new Tag(),
                            new Tag()
                        }
                    }
                });
            }));

            var ops = new SolutionTagOperations<Tag>(client);
            var paginated = await ops.GetSolutionTagsPaginatedAsync(123);

            Assert.AreEqual(2, paginated.Items.Count);
        }

        [TestMethod]
        public async Task GetSolutionTagsPaginatedAsync_InvalidSolutionID_ThrowsUKFastClientValidationException()
        {
            var ops = new SolutionTagOperations<Tag>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.GetSolutionTagsPaginatedAsync(0));
        }

        [TestMethod]
        public async Task GetSolutionTagAsync_ValidParameters_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetAsync<Tag>("/ecloud/v1/solutions/123/tags/testkey").Returns(new Tag()
            {
                Value = "testvalue"
            });

            var ops = new SolutionTagOperations<Tag>(client);
            var tag = await ops.GetSolutionTagAsync(123, "testkey");

            Assert.AreEqual("testvalue", tag.Value);
        }

        [TestMethod]
        public async Task GetSolutionTagAsync_InvalidSolutionID_ThrowsUKFastClientValidationException()
        {
            var ops = new SolutionTagOperations<Tag>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.GetSolutionTagAsync(0, "testkey"));
        }

        [TestMethod]
        public async Task GetSolutionTagAsync_InvalidTagKey_ThrowsUKFastClientValidationException()
        {
            var ops = new SolutionTagOperations<Tag>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.GetSolutionTagAsync(123, ""));
        }

        [TestMethod]
        public async Task UpdateSolutionTagAsync_ValidParameters_ExpectedResult()
        {
            UpdateTagRequest req = new UpdateTagRequest()
            {
                Value = "testvalue"
            };

            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.PatchAsync<Tag>("/ecloud/v1/solutions/123/tags/testkey").Returns(new Tag()
            {
                Value = "testvalue"
            });

            var ops = new SolutionTagOperations<Tag>(client);
            await ops.UpdateSolutionTagAsync(123, "testkey", req);

            await client.Received().PatchAsync("/ecloud/v1/solutions/123/tags/testkey", req);
        }

        [TestMethod]
        public async Task UpdateSolutionTagAsync_InvalidSolutionID_ThrowsUKFastClientValidationException()
        {
            var ops = new SolutionTagOperations<Tag>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.UpdateSolutionTagAsync(0, "testkey", null));
        }

        [TestMethod]
        public async Task UpdateSolutionTagAsync_InvalidTagKey_ThrowsUKFastClientValidationException()
        {
            var ops = new SolutionTagOperations<Tag>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.UpdateSolutionTagAsync(123, "", null));
        }

        [TestMethod]
        public async Task DeleteSolutionTagAsync_ValidParameters_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.DeleteAsync<Tag>("/ecloud/v1/solutions/123/tags/testkey").Returns(new Tag()
            {
                Value = "testvalue"
            });

            var ops = new SolutionTagOperations<Tag>(client);
            await ops.DeleteSolutionTagAsync(123, "testkey");

            await client.Received().DeleteAsync("/ecloud/v1/solutions/123/tags/testkey");
        }

        [TestMethod]
        public async Task DeleteSolutionTagAsync_InvalidSolutionID_ThrowsUKFastClientValidationException()
        {
            var ops = new SolutionTagOperations<Tag>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.DeleteSolutionTagAsync(0, "testkey"));
        }

        [TestMethod]
        public async Task DeleteSolutionTagAsync_InvalidTagKey_ThrowsUKFastClientValidationException()
        {
            var ops = new SolutionTagOperations<Tag>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.DeleteSolutionTagAsync(123, ""));
        }
    }
}