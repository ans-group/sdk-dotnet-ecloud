using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models;
using UKFast.API.Client.ECloud.Models.Request;
using UKFast.API.Client.ECloud.Operations;
using UKFast.API.Client.Exception;
using UKFast.API.Client.Models;

namespace UKFast.API.Client.ECloud.Tests.Operations
{
    [TestClass]
    public class VirtualMachineTagOperationsTests
    {
        [TestMethod]
        public async Task CreateVirtualMachineTagAsync_ExpectedResult()
        {
            CreateTagRequest req = new CreateTagRequest()
            {
                Key = "testkey",
                Value = "testvalue"
            };

            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();
            client.PostAsync<Tag>("/ecloud/v1/vms/123/tags", req).Returns(new Tag()
            {
                Key = "testkey"
            });

            var ops = new VirtualMachineTagOperations<Tag>(client);
            var tagKey = await ops.CreateVirtualMachineTagAsync(123, req);

            Assert.AreEqual("testkey", tagKey);
        }

        [TestMethod]
        public async Task CreateVirtualMachineTagAsync_InvalidVirtualMachineID_ThrowsUKFastClientValidationException()
        {
            var ops = new VirtualMachineTagOperations<Tag>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.CreateVirtualMachineTagAsync(0, null));
        }

        [TestMethod]
        public async Task GetVirtualMachineTagsAsync_ExpectedResult()
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

            var ops = new VirtualMachineTagOperations<Tag>(client);
            var tags = await ops.GetVirtualMachineTagsAsync(123);

            Assert.AreEqual(2, tags.Count);
        }

        [TestMethod]
        public async Task GetVirtualMachineTagsPaginatedAsync_ExpectedClientCall()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetPaginatedAsync<Tag>("/ecloud/v1/vms/123/tags").Returns(Task.Run(() =>
            {
                return new Paginated<Tag>(client, "/ecloud/v1/vms/123/tags", null, new Response.ClientResponse<System.Collections.Generic.IList<Tag>>()
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

            var ops = new VirtualMachineTagOperations<Tag>(client);
            var paginated = await ops.GetVirtualMachineTagsPaginatedAsync(123);

            Assert.AreEqual(2, paginated.Items.Count);
        }

        [TestMethod]
        public async Task GetVirtualMachineTagsPaginatedAsync_InvalidVirtualMachineID_ThrowsUKFastClientValidationException()
        {
            var ops = new VirtualMachineTagOperations<Tag>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.GetVirtualMachineTagsPaginatedAsync(0));
        }

        [TestMethod]
        public async Task GetVirtualMachineTagAsync_ValidParameters_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetAsync<Tag>("/ecloud/v1/vms/123/tags/testkey").Returns(new Tag()
            {
                Value = "testvalue"
            });

            var ops = new VirtualMachineTagOperations<Tag>(client);
            var tag = await ops.GetVirtualMachineTagAsync(123, "testkey");

            Assert.AreEqual("testvalue", tag.Value);
        }

        [TestMethod]
        public async Task GetVirtualMachineTagAsync_InvalidVirtualMachineID_ThrowsUKFastClientValidationException()
        {
            var ops = new VirtualMachineTagOperations<Tag>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.GetVirtualMachineTagAsync(0, "testkey"));
        }

        [TestMethod]
        public async Task GetVirtualMachineTagAsync_InvalidTagKey_ThrowsUKFastClientValidationException()
        {
            var ops = new VirtualMachineTagOperations<Tag>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.GetVirtualMachineTagAsync(123, ""));
        }

        [TestMethod]
        public async Task UpdateVirtualMachineTagAsync_ValidParameters_ExpectedResult()
        {
            UpdateTagRequest req = new UpdateTagRequest()
            {
                Value = "testvalue"
            };

            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.PatchAsync<Tag>("/ecloud/v1/vms/123/tags/testkey").Returns(new Tag()
            {
                Value = "testvalue"
            });

            var ops = new VirtualMachineTagOperations<Tag>(client);
            await ops.UpdateVirtualMachineTagAsync(123, "testkey", req);

            await client.Received().PatchAsync("/ecloud/v1/vms/123/tags/testkey", req);
        }

        [TestMethod]
        public async Task UpdateVirtualMachineTagAsync_InvalidVirtualMachineID_ThrowsUKFastClientValidationException()
        {
            var ops = new VirtualMachineTagOperations<Tag>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.UpdateVirtualMachineTagAsync(0, "testkey", null));
        }

        [TestMethod]
        public async Task UpdateVirtualMachineTagAsync_InvalidTagKey_ThrowsUKFastClientValidationException()
        {
            var ops = new VirtualMachineTagOperations<Tag>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.UpdateVirtualMachineTagAsync(123, "", null));
        }

        [TestMethod]
        public async Task DeleteVirtualMachineTagAsync_ValidParameters_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.DeleteAsync<Tag>("/ecloud/v1/vms/123/tags/testkey").Returns(new Tag()
            {
                Value = "testvalue"
            });

            var ops = new VirtualMachineTagOperations<Tag>(client);
            await ops.DeleteVirtualMachineTagAsync(123, "testkey");

            await client.Received().DeleteAsync("/ecloud/v1/vms/123/tags/testkey");
        }

        [TestMethod]
        public async Task DeleteVirtualMachineTagAsync_InvalidVirtualMachineID_ThrowsUKFastClientValidationException()
        {
            var ops = new VirtualMachineTagOperations<Tag>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.DeleteVirtualMachineTagAsync(0, "testkey"));
        }

        [TestMethod]
        public async Task DeleteVirtualMachineTagAsync_InvalidTagKey_ThrowsUKFastClientValidationException()
        {
            var ops = new VirtualMachineTagOperations<Tag>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.DeleteVirtualMachineTagAsync(123, ""));
        }
    }
}
