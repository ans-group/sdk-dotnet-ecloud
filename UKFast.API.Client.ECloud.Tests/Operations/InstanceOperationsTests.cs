using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
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
    public class InstanceOperationsTests
    {
        [TestMethod]
        public async Task CreateInstanceAsync_ExpectedResult()
        {
            CreateInstanceRequest req = new CreateInstanceRequest()
            {
                ApplianceID = "00000000-0000-0000-0000-000000000000",
                Name = "test instance",
                RAMCapacity = 2,
                VCPUCores = 2
            };

            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();
            client.PostAsync<Instance>("/ecloud/v2/instances", req).Returns(new Instance()
            {
                ID = "i-abcd1234"
            });

            var ops = new InstanceOperations<Instance>(client);
            var instanceID = await ops.CreateInstanceAsync(req);

            Assert.AreEqual("i-abcd1234", instanceID);
        }

        [TestMethod]
        public async Task GetInstancesAsync_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetAllAsync(Arg.Any<UKFastClient.GetPaginatedAsyncFunc<Instance>>(), null).Returns(Task.Run<IList<Instance>>(() =>
            {
                return new List<Instance>()
                {
                    new Instance(),
                    new Instance()
                };
            }));

            var ops = new InstanceOperations<Instance>(client);
            var instances = await ops.GetInstancesAsync();

            Assert.AreEqual(2, instances.Count);
        }

        [TestMethod]
        public async Task GetInstancesPaginatedAsync_ExpectedClientCall()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetPaginatedAsync<Instance>("/ecloud/v2/instances").Returns(Task.Run(() =>
            {
                return new Paginated<Instance>(client, "/ecloud/v2/instances", null,
                    new ClientResponse<IList<Instance>>()
                    {
                        Body = new ClientResponseBody<IList<Instance>>()
                        {
                            Data = new List<Instance>()
                            {
                                new Instance(),
                                new Instance()
                            }
                        }
                    });
            }));

            var ops = new InstanceOperations<Instance>(client);
            var paginated = await ops.GetInstancesPaginatedAsync();

            Assert.AreEqual(2, paginated.Items.Count);
        }

        [TestMethod]
        public async Task GetInstanceAsync_ValidParameters_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            string instanceID = "00000000-0000-0000-0000-000000000000";

            client.GetAsync<Instance>($"/ecloud/v2/instances/{instanceID}").Returns(new Instance()
            {
                ID = instanceID
            });

            var ops = new InstanceOperations<Instance>(client);
            var instance = await ops.GetInstanceAsync(instanceID);

            Assert.AreEqual(instanceID, instance.ID);
        }

        [TestMethod]
        public async Task GetInstanceAsync_InvalidInstanceName_ThrowsUKFastClientValidationException()
        {
            var ops = new InstanceOperations<Instance>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.GetInstanceAsync(""));
        }

        [TestMethod]
        public async Task UpdateInstanceAsync_ExpectedResult()
        {
            UpdateInstanceRequest req = new UpdateInstanceRequest()
            {
                Name = "test-instance"
            };

            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            var ops = new InstanceOperations<Instance>(client);
            await ops.UpdateInstanceAsync("00000000-0000-0000-0000-00000000000", req);

            await client.Received().PatchAsync("/ecloud/v2/instances/00000000-0000-0000-0000-00000000000", req);
        }

        [TestMethod]
        public async Task UpdateInstanceAsync_InvalidInstanceID_ThrowsUKFastClientValidationException()
        {
            var ops = new InstanceOperations<Instance>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.UpdateInstanceAsync("", null));
        }

        [TestMethod]
        public async Task DeleteInstanceAsync_ValidParameters()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            var ops = new InstanceOperations<Instance>(client);
            await ops.DeleteInstanceAsync("00000000-0000-0000-0000-00000000000");

            await client.Received().DeleteAsync("/ecloud/v2/instances/00000000-0000-0000-0000-00000000000");
        }

        [TestMethod]
        public async Task DeleteInstanceAsync_InvalidInstanceID_ThrowsUKFastClientValidationException()
        {
            var ops = new InstanceOperations<Instance>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.DeleteInstanceAsync(""));
        }

        [TestMethod]
        public async Task PowerOnInstanceAsync_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            var ops = new InstanceOperations<Instance>(client);
            await ops.PowerOnInstanceAsync("00000000-0000-0000-0000-000000000000");

            await client.Received().PutAsync("/ecloud/v2/instances/00000000-0000-0000-0000-000000000000/power-on");
        }

        [TestMethod]
        public async Task PowerOnInstanceAsync_InvalidInstanceID_ThrowsUKFastClientValidationException()
        {
            var ops = new InstanceOperations<Instance>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.PowerOnInstanceAsync(""));
        }

        [TestMethod]
        public async Task PowerOffInstanceAsync_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            var ops = new InstanceOperations<Instance>(client);
            await ops.PowerOffInstanceAsync("00000000-0000-0000-0000-000000000000");

            await client.Received().PutAsync("/ecloud/v2/instances/00000000-0000-0000-0000-000000000000/power-off");
        }

        [TestMethod]
        public async Task PowerOffInstanceAsync_InvalidInstanceID_ThrowsUKFastClientValidationException()
        {
            var ops = new InstanceOperations<Instance>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.PowerOffInstanceAsync(""));
        }

        [TestMethod]
        public async Task PowerResetInstanceAsync_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            var ops = new InstanceOperations<Instance>(client);
            await ops.PowerResetInstanceAsync("00000000-0000-0000-0000-000000000000");

            await client.Received().PutAsync("/ecloud/v2/instances/00000000-0000-0000-0000-000000000000/power-reset");
        }

        [TestMethod]
        public async Task PowerResetInstanceAsync_InvalidInstanceID_ThrowsUKFastClientValidationException()
        {
            var ops = new InstanceOperations<Instance>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.PowerResetInstanceAsync(""));
        }

        [TestMethod]
        public async Task PowerShutdownInstanceAsync_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            var ops = new InstanceOperations<Instance>(client);
            await ops.PowerShutdownInstanceAsync("00000000-0000-0000-0000-000000000000");

            await client.Received().PutAsync("/ecloud/v2/instances/00000000-0000-0000-0000-000000000000/power-shutdown");
        }

        [TestMethod]
        public async Task PowerShutdownInstanceAsync_InvalidInstanceID_ThrowsUKFastClientValidationException()
        {
            var ops = new InstanceOperations<Instance>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.PowerShutdownInstanceAsync(""));
        }

        [TestMethod]
        public async Task PowerRestartInstanceAsync_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            var ops = new InstanceOperations<Instance>(client);
            await ops.PowerRestartInstanceAsync("00000000-0000-0000-0000-000000000000");

            await client.Received().PutAsync("/ecloud/v2/instances/00000000-0000-0000-0000-000000000000/power-restart");
        }

        [TestMethod]
        public async Task PowerRestartInstanceAsync_InvalidInstanceID_ThrowsUKFastClientValidationException()
        {
            var ops = new InstanceOperations<Instance>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.PowerRestartInstanceAsync(""));
        }
    }
}