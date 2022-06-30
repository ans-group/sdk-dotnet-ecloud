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
    public class InstanceOperationsTests
    {
        [TestMethod]
        public async Task CreateInstanceAsync_ExpectedResult()
        {
            CreateInstanceRequest req = new CreateInstanceRequest()
            {
                ImageID = "img-abcdef",
                Name = "test instance",
                RAMCapacity = 2,
                VCPUCores = 2
            };

            IANSECloudClient client = Substitute.For<IANSECloudClient>();
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
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAllAsync(Arg.Any<ANSClient.GetPaginatedAsyncFunc<Instance>>(), null).Returns(Task.Run<IList<Instance>>(() =>
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
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

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
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            string instanceID = "i-abcd1234";

            client.GetAsync<Instance>($"/ecloud/v2/instances/{instanceID}").Returns(new Instance()
            {
                ID = instanceID
            });

            var ops = new InstanceOperations<Instance>(client);
            var instance = await ops.GetInstanceAsync(instanceID);

            Assert.AreEqual("i-abcd1234", instance.ID);
        }

        [TestMethod]
        public async Task GetInstanceAsync_InvalidInstanceName_ThrowsANSClientValidationException()
        {
            var ops = new InstanceOperations<Instance>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetInstanceAsync(""));
        }

        [TestMethod]
        public async Task GetInstanceAsync_NotFound_ThrowsANSClientNotFoundRequestException()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAsync<Instance>("/ecloud/v2/instances/i-abcd1234").Throws(
                new ANSClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new InstanceOperations<Instance>(client);

            await Assert.ThrowsExceptionAsync<ANSClientNotFoundRequestException>(() => ops.GetInstanceAsync("i-abcd1234"));
        }

        [TestMethod]
        public async Task UpdateInstanceAsync_ExpectedResult()
        {
            UpdateInstanceRequest req = new UpdateInstanceRequest()
            {
                Name = "test-instance"
            };

            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            var ops = new InstanceOperations<Instance>(client);
            await ops.UpdateInstanceAsync("i-abcd1234", req);

            await client.Received().PatchAsync("/ecloud/v2/instances/i-abcd1234", req);
        }

        [TestMethod]
        public async Task UpdateInstanceAsync_InvalidInstanceID_ThrowsANSClientValidationException()
        {
            var ops = new InstanceOperations<Instance>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.UpdateInstanceAsync("", null));
        }

        [TestMethod]
        public async Task UpdateInstanceAsync_NotFound_ThrowsANSClientNotFoundRequestException()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.PatchAsync("/ecloud/v2/instances/i-abcd1234").Throws(
                new ANSClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new InstanceOperations<Instance>(client);

            await Assert.ThrowsExceptionAsync<ANSClientNotFoundRequestException>(() => ops.UpdateInstanceAsync("i-abcd1234", null));
        }

        [TestMethod]
        public async Task DeleteInstanceAsync_ValidParameters()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            var ops = new InstanceOperations<Instance>(client);
            await ops.DeleteInstanceAsync("i-abcd1234");

            await client.Received().DeleteAsync("/ecloud/v2/instances/i-abcd1234");
        }

        [TestMethod]
        public async Task DeleteInstanceAsync_InvalidInstanceID_ThrowsANSClientValidationException()
        {
            var ops = new InstanceOperations<Instance>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.DeleteInstanceAsync(""));
        }

        [TestMethod]
        public async Task DeleteInstanceAsync_NotFound_ThrowsANSClientNotFoundRequestException()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.DeleteAsync("/ecloud/v2/instances/i-abcd1234").Throws(
                new ANSClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new InstanceOperations<Instance>(client);

            await Assert.ThrowsExceptionAsync<ANSClientNotFoundRequestException>(() => ops.DeleteInstanceAsync("i-abcd1234"));
        }

        [TestMethod]
        public async Task LockInstanceAsync_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            var ops = new InstanceOperations<Instance>(client);
            await ops.LockInstanceAsync("i-abcd1234");

            await client.Received().PutAsync("/ecloud/v2/instances/i-abcd1234/lock");
        }

        [TestMethod]
        public async Task LockInstanceAsync_InvalidInstanceID_ThrowsANSClientValidationException()
        {
            var ops = new InstanceOperations<Instance>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.LockInstanceAsync(""));
        }

        [TestMethod]
        public async Task LockInstanceAsync_NotFound_ThrowsANSClientNotFoundRequestException()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.PutAsync("/ecloud/v2/instances/i-abcd1234/lock").Throws(
                new ANSClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new InstanceOperations<Instance>(client);

            await Assert.ThrowsExceptionAsync<ANSClientNotFoundRequestException>(() => ops.LockInstanceAsync("i-abcd1234"));
        }

        [TestMethod]
        public async Task UnlockInstanceAsync_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            var ops = new InstanceOperations<Instance>(client);
            await ops.UnlockInstanceAsync("i-abcd1234");

            await client.Received().PutAsync("/ecloud/v2/instances/i-abcd1234/unlock");
        }

        [TestMethod]
        public async Task UnlockInstanceAsync_InvalidInstanceID_ThrowsANSClientValidationException()
        {
            var ops = new InstanceOperations<Instance>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.UnlockInstanceAsync(""));
        }

        [TestMethod]
        public async Task UnlockInstanceAsync_NotFound_ThrowsANSClientNotFoundRequestException()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.PutAsync("/ecloud/v2/instances/i-abcd1234/unlock").Throws(
                new ANSClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new InstanceOperations<Instance>(client);

            await Assert.ThrowsExceptionAsync<ANSClientNotFoundRequestException>(() => ops.UnlockInstanceAsync("i-abcd1234"));
        }

        [TestMethod]
        public async Task PowerOnInstanceAsync_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            var ops = new InstanceOperations<Instance>(client);
            await ops.PowerOnInstanceAsync("i-abcd1234");

            await client.Received().PutAsync("/ecloud/v2/instances/i-abcd1234/power-on");
        }

        [TestMethod]
        public async Task PowerOnInstanceAsync_InvalidInstanceID_ThrowsANSClientValidationException()
        {
            var ops = new InstanceOperations<Instance>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.PowerOnInstanceAsync(""));
        }

        [TestMethod]
        public async Task PowerOnInstanceAsync_NotFound_ThrowsANSClientNotFoundRequestException()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.PutAsync("/ecloud/v2/instances/i-abcd1234/power-on").Throws(
                new ANSClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new InstanceOperations<Instance>(client);

            await Assert.ThrowsExceptionAsync<ANSClientNotFoundRequestException>(() => ops.PowerOnInstanceAsync("i-abcd1234"));
        }

        [TestMethod]
        public async Task PowerOffInstanceAsync_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            var ops = new InstanceOperations<Instance>(client);
            await ops.PowerOffInstanceAsync("i-abcd1234");

            await client.Received().PutAsync("/ecloud/v2/instances/i-abcd1234/power-off");
        }

        [TestMethod]
        public async Task PowerOffInstanceAsync_InvalidInstanceID_ThrowsANSClientValidationException()
        {
            var ops = new InstanceOperations<Instance>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.PowerOffInstanceAsync(""));
        }

        [TestMethod]
        public async Task PowerOffInstanceAsync_NotFound_ThrowsANSClientNotFoundRequestException()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.PutAsync("/ecloud/v2/instances/i-abcd1234/power-off").Throws(
                new ANSClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new InstanceOperations<Instance>(client);

            await Assert.ThrowsExceptionAsync<ANSClientNotFoundRequestException>(() => ops.PowerOffInstanceAsync("i-abcd1234"));
        }

        [TestMethod]
        public async Task PowerResetInstanceAsync_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            var ops = new InstanceOperations<Instance>(client);
            await ops.PowerResetInstanceAsync("i-abcd1234");

            await client.Received().PutAsync("/ecloud/v2/instances/i-abcd1234/power-reset");
        }

        [TestMethod]
        public async Task PowerResetInstanceAsync_InvalidInstanceID_ThrowsANSClientValidationException()
        {
            var ops = new InstanceOperations<Instance>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.PowerResetInstanceAsync(""));
        }

        [TestMethod]
        public async Task PowerResetInstanceAsync_NotFound_ThrowsANSClientNotFoundRequestException()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.PutAsync("/ecloud/v2/instances/i-abcd1234/power-reset").Throws(
                new ANSClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new InstanceOperations<Instance>(client);

            await Assert.ThrowsExceptionAsync<ANSClientNotFoundRequestException>(() => ops.PowerResetInstanceAsync("i-abcd1234"));
        }

        [TestMethod]
        public async Task PowerShutdownInstanceAsync_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            var ops = new InstanceOperations<Instance>(client);
            await ops.PowerShutdownInstanceAsync("i-abcd1234");

            await client.Received().PutAsync("/ecloud/v2/instances/i-abcd1234/power-shutdown");
        }

        [TestMethod]
        public async Task PowerShutdownInstanceAsync_InvalidInstanceID_ThrowsANSClientValidationException()
        {
            var ops = new InstanceOperations<Instance>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.PowerShutdownInstanceAsync(""));
        }

        [TestMethod]
        public async Task PowerShutdownInstanceAsync_NotFound_ThrowsANSClientNotFoundRequestException()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.PutAsync("/ecloud/v2/instances/i-abcd1234/power-shutdown").Throws(
                new ANSClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new InstanceOperations<Instance>(client);

            await Assert.ThrowsExceptionAsync<ANSClientNotFoundRequestException>(() => ops.PowerShutdownInstanceAsync("i-abcd1234"));
        }

        [TestMethod]
        public async Task PowerRestartInstanceAsync_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            var ops = new InstanceOperations<Instance>(client);
            await ops.PowerRestartInstanceAsync("i-abcd1234");

            await client.Received().PutAsync("/ecloud/v2/instances/i-abcd1234/power-restart");
        }

        [TestMethod]
        public async Task PowerRestartInstanceAsync_InvalidInstanceID_ThrowsANSClientValidationException()
        {
            var ops = new InstanceOperations<Instance>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.PowerRestartInstanceAsync(""));
        }

        [TestMethod]
        public async Task PowerRestartInstanceAsync_NotFound_ThrowsANSClientNotFoundRequestException()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.PutAsync("/ecloud/v2/instances/i-abcd1234/power-restart").Throws(
                new ANSClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new InstanceOperations<Instance>(client);

            await Assert.ThrowsExceptionAsync<ANSClientNotFoundRequestException>(() => ops.PowerRestartInstanceAsync("i-abcd1234"));
        }

        [TestMethod]
        public async Task GetInstanceVolumesAsync_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAllAsync(Arg.Any<ANSClient.GetPaginatedAsyncFunc<Volume>>(), null)
                .Returns(Task.Run<IList<Volume>>(() => new List<Volume>()
                    {
                        new Volume(),
                        new Volume()
                    }));

            var ops = new InstanceOperations<Instance>(client);
            var volumes = await ops.GetInstanceVolumesAsync("i-abcd1234");

            Assert.AreEqual(2, volumes.Count);
        }

        [TestMethod]
        public async Task GetInstanceVolumesAsync_InvalidInstanceID_ThrowsANSClientValidationException()
        {
            var ops = new InstanceOperations<Instance>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetInstanceVolumesAsync(""));
        }

        [TestMethod]
        public async Task GetInstanceVolumesPaginatedAsync_ExpectedClientCall()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetPaginatedAsync<Volume>("/ecloud/v2/instances/i-abcd1234/volumes")
                .Returns(Task.Run(() => new Paginated<Volume>(client, "/ecloud/v2/instances/i-abcd1234/volumes", null,
                    new ClientResponse<IList<Volume>>()
                    {
                        Body = new ClientResponseBody<IList<Volume>>()
                        {
                            Data = new List<Volume>()
                                    {
                                        new Volume(),
                                        new Volume()
                                    }
                        }
                    })));

            var ops = new InstanceOperations<Instance>(client);
            var paginated = await ops.GetInstanceVolumesPaginatedAsync("i-abcd1234");

            Assert.AreEqual(2, paginated.Items.Count);
        }

        [TestMethod]
        public async Task GetInstanceVolumesPaginatedAsync_InvalidInstanceID_ThrowsANSClientValidationException()
        {
            var ops = new InstanceOperations<Instance>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetInstanceVolumesPaginatedAsync(""));
        }

        [TestMethod]
        public async Task GetInstanceVolumesPaginatedAsync_InstanceNotFound_ThrowsANSClientNotFoundRequestException()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetPaginatedAsync<Volume>("/ecloud/v2/instances/i-abcd1234/volumes").Throws(
                new ANSClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new InstanceOperations<Instance>(client);

            await Assert.ThrowsExceptionAsync<ANSClientNotFoundRequestException>(() => ops.GetInstanceVolumesPaginatedAsync("i-abcd1234"));
        }

        [TestMethod]
        public async Task GetInstanceCredentialsAsync_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAllAsync(Arg.Any<ANSClient.GetPaginatedAsyncFunc<Credential>>(), null)
                .Returns(Task.Run<IList<Credential>>(() => new List<Credential>()
                    {
                        new Credential(),
                        new Credential()
                    }));

            var ops = new InstanceOperations<Instance>(client);
            var credentials = await ops.GetInstanceCredentialsAsync("i-abcd1234");

            Assert.AreEqual(2, credentials.Count);
        }

        [TestMethod]
        public async Task GetInstanceCredentialsAsync_InvalidInstanceID_ThrowsANSClientValidationException()
        {
            var ops = new InstanceOperations<Instance>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetInstanceCredentialsAsync(""));
        }

        [TestMethod]
        public async Task GetInstanceCredentialsPaginatedAsync_ExpectedClientCall()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetPaginatedAsync<Credential>("/ecloud/v2/instances/i-abcd1234/credentials")
                .Returns(Task.Run(() => new Paginated<Credential>(client, "/ecloud/v2/instances/i-abcd1234/credentials", null,
                    new ClientResponse<IList<Credential>>()
                    {
                        Body = new ClientResponseBody<IList<Credential>>()
                        {
                            Data = new List<Credential>()
                                    {
                                        new Credential(),
                                        new Credential()
                                    }
                        }
                    })));

            var ops = new InstanceOperations<Instance>(client);
            var paginated = await ops.GetInstanceCredentialsPaginatedAsync("i-abcd1234");

            Assert.AreEqual(2, paginated.Items.Count);
        }

        [TestMethod]
        public async Task GetInstanceCredentialsPaginatedAsync_InvalidInstanceID_ThrowsANSClientValidationException()
        {
            var ops = new InstanceOperations<Instance>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetInstanceCredentialsPaginatedAsync(""));
        }

        [TestMethod]
        public async Task GetInstanceCredentialsPaginatedAsync_InstanceNotFound_ThrowsANSClientNotFoundRequestException()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetPaginatedAsync<Credential>("/ecloud/v2/instances/i-abcd1234/credentials").Throws(
                new ANSClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new InstanceOperations<Instance>(client);

            await Assert.ThrowsExceptionAsync<ANSClientNotFoundRequestException>(() => ops.GetInstanceCredentialsPaginatedAsync("i-abcd1234"));
        }

        [TestMethod]
        public async Task GetInstanceNICsAsync_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAllAsync(Arg.Any<ANSClient.GetPaginatedAsyncFunc<NIC>>(), null)
                .Returns(Task.Run<IList<NIC>>(() => new List<NIC>()
                    {
                        new NIC(),
                        new NIC()
                    }));

            var ops = new InstanceOperations<Instance>(client);
            var nic = await ops.GetInstanceNICsAsync("i-abcd1234");

            Assert.AreEqual(2, nic.Count);
        }

        [TestMethod]
        public async Task GetInstanceNICsAsync_InvalidInstanceID_ThrowsANSClientValidationException()
        {
            var ops = new InstanceOperations<Instance>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetInstanceNICsAsync(""));
        }

        [TestMethod]
        public async Task GetInstanceNICsPaginatedAsync_ExpectedClientCall()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetPaginatedAsync<NIC>("/ecloud/v2/instances/i-abcd1234/nics")
                .Returns(Task.Run(() => new Paginated<NIC>(client, "/ecloud/v2/instances/i-abcd1234/nics", null,
                    new ClientResponse<IList<NIC>>()
                    {
                        Body = new ClientResponseBody<IList<NIC>>()
                        {
                            Data = new List<NIC>()
                                    {
                                        new NIC(),
                                        new NIC()
                                    }
                        }
                    })));

            var ops = new InstanceOperations<Instance>(client);
            var paginated = await ops.GetInstanceNICsPaginatedAsync("i-abcd1234");

            Assert.AreEqual(2, paginated.Items.Count);
        }

        [TestMethod]
        public async Task GetInstanceNICsPaginatedAsync_InvalidInstanceID_ThrowsANSClientValidationException()
        {
            var ops = new InstanceOperations<Instance>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetInstanceNICsPaginatedAsync(""));
        }

        [TestMethod]
        public async Task GetInstanceNICsPaginatedAsync_InstanceNotFound_ThrowsANSClientNotFoundRequestException()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetPaginatedAsync<NIC>("/ecloud/v2/instances/i-abcd1234/nics").Throws(
                new ANSClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new InstanceOperations<Instance>(client);

            await Assert.ThrowsExceptionAsync<ANSClientNotFoundRequestException>(() => ops.GetInstanceNICsPaginatedAsync("i-abcd1234"));
        }
    }
}