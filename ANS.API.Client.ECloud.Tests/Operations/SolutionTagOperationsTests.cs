using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V1;
using ANS.API.Client.ECloud.Models.V1.Request;
using ANS.API.Client.ECloud.Operations;
using ANS.API.Client.Exception;
using ANS.API.Client.Models;

namespace ANS.API.Client.ECloud.Tests.Operations
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

            IANSECloudClient client = Substitute.For<IANSECloudClient>();
            client.PostAsync<Tag>("/ecloud/v1/solutions/123/tags", req).Returns(new Tag()
            {
                Key = "testkey"
            });

            var ops = new SolutionTagOperations<Tag>(client);
            var tagKey = await ops.CreateSolutionTagAsync(123, req);

            Assert.AreEqual("testkey", tagKey);
        }

        [TestMethod]
        public async Task CreateSolutionTagAsync_InvalidSolutionID_ThrowsANSClientValidationException()
        {
            var ops = new SolutionTagOperations<Tag>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.CreateSolutionTagAsync(0, null));
        }

        [TestMethod]
        public async Task GetSolutionTagsAsync_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAllAsync(Arg.Any<ANSClient.GetPaginatedAsyncFunc<Tag>>(), null).Returns(Task.Run<IList<Tag>>(() =>
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
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

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
        public async Task GetSolutionTagsPaginatedAsync_InvalidSolutionID_ThrowsANSClientValidationException()
        {
            var ops = new SolutionTagOperations<Tag>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetSolutionTagsPaginatedAsync(0));
        }

        [TestMethod]
        public async Task GetSolutionTagAsync_ValidParameters_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAsync<Tag>("/ecloud/v1/solutions/123/tags/testkey").Returns(new Tag()
            {
                Value = "testvalue"
            });

            var ops = new SolutionTagOperations<Tag>(client);
            var tag = await ops.GetSolutionTagAsync(123, "testkey");

            Assert.AreEqual("testvalue", tag.Value);
        }

        [TestMethod]
        public async Task GetSolutionTagAsync_InvalidSolutionID_ThrowsANSClientValidationException()
        {
            var ops = new SolutionTagOperations<Tag>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetSolutionTagAsync(0, "testkey"));
        }

        [TestMethod]
        public async Task GetSolutionTagAsync_InvalidTagKey_ThrowsANSClientValidationException()
        {
            var ops = new SolutionTagOperations<Tag>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetSolutionTagAsync(123, ""));
        }

        [TestMethod]
        public async Task UpdateSolutionTagAsync_ValidParameters_ExpectedResult()
        {
            UpdateTagRequest req = new UpdateTagRequest()
            {
                Value = "testvalue"
            };

            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.PatchAsync<Tag>("/ecloud/v1/solutions/123/tags/testkey").Returns(new Tag()
            {
                Value = "testvalue"
            });

            var ops = new SolutionTagOperations<Tag>(client);
            await ops.UpdateSolutionTagAsync(123, "testkey", req);

            await client.Received().PatchAsync("/ecloud/v1/solutions/123/tags/testkey", req);
        }

        [TestMethod]
        public async Task UpdateSolutionTagAsync_InvalidSolutionID_ThrowsANSClientValidationException()
        {
            var ops = new SolutionTagOperations<Tag>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.UpdateSolutionTagAsync(0, "testkey", null));
        }

        [TestMethod]
        public async Task UpdateSolutionTagAsync_InvalidTagKey_ThrowsANSClientValidationException()
        {
            var ops = new SolutionTagOperations<Tag>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.UpdateSolutionTagAsync(123, "", null));
        }

        [TestMethod]
        public async Task DeleteSolutionTagAsync_ValidParameters_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.DeleteAsync<Tag>("/ecloud/v1/solutions/123/tags/testkey").Returns(new Tag()
            {
                Value = "testvalue"
            });

            var ops = new SolutionTagOperations<Tag>(client);
            await ops.DeleteSolutionTagAsync(123, "testkey");

            await client.Received().DeleteAsync("/ecloud/v1/solutions/123/tags/testkey");
        }

        [TestMethod]
        public async Task DeleteSolutionTagAsync_InvalidSolutionID_ThrowsANSClientValidationException()
        {
            var ops = new SolutionTagOperations<Tag>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.DeleteSolutionTagAsync(0, "testkey"));
        }

        [TestMethod]
        public async Task DeleteSolutionTagAsync_InvalidTagKey_ThrowsANSClientValidationException()
        {
            var ops = new SolutionTagOperations<Tag>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.DeleteSolutionTagAsync(123, ""));
        }
    }
}