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
    public class VirtualMachineOperationsTests
    {
        [TestMethod]
        public async Task CreateVirtualMachineAsync_ExpectedResult()
        {
            CreateVirtualMachineRequest req = new CreateVirtualMachineRequest()
            {
                Template = "testtemplate"
            };

            IANSECloudClient client = Substitute.For<IANSECloudClient>();
            client.PostAsync<VirtualMachine>("/ecloud/v1/vms", req).Returns(new VirtualMachine()
            {
                ID = 123
            });

            var ops = new VirtualMachineOperations<VirtualMachine>(client);
            var vmID = await ops.CreateVirtualMachineAsync(req);

            Assert.AreEqual(123, vmID);
        }

        [TestMethod]
        public async Task GetVirtualMachinesAsync_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAllAsync(Arg.Any<ANSClient.GetPaginatedAsyncFunc<VirtualMachine>>(), null).Returns(Task.Run<IList<VirtualMachine>>(() =>
            {
                return new List<VirtualMachine>()
                 {
                        new VirtualMachine(),
                        new VirtualMachine()
                 };
            }));

            var ops = new VirtualMachineOperations<VirtualMachine>(client);
            var vms = await ops.GetVirtualMachinesAsync();

            Assert.AreEqual(2, vms.Count);
        }

        [TestMethod]
        public async Task GetVirtualMachinesPaginatedAsync_ExpectedClientCall()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetPaginatedAsync<VirtualMachine>("/ecloud/v1/vms").Returns(Task.Run(() =>
            {
                return new Paginated<VirtualMachine>(client, "/ecloud/v1/vms", null, new Response.ClientResponse<System.Collections.Generic.IList<VirtualMachine>>()
                {
                    Body = new Response.ClientResponseBody<System.Collections.Generic.IList<VirtualMachine>>()
                    {
                        Data = new List<VirtualMachine>()
                        {
                            new VirtualMachine(),
                            new VirtualMachine()
                        }
                    }
                });
            }));

            var ops = new VirtualMachineOperations<VirtualMachine>(client);
            var paginated = await ops.GetVirtualMachinesPaginatedAsync();

            Assert.AreEqual(2, paginated.Items.Count);
        }

        [TestMethod]
        public async Task GetVirtualMachineAsync_ValidParameters_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAsync<VirtualMachine>("/ecloud/v1/vms/123").Returns(new VirtualMachine()
            {
                ID = 123
            });

            var ops = new VirtualMachineOperations<VirtualMachine>(client);
            var vm = await ops.GetVirtualMachineAsync(123);

            Assert.AreEqual(123, vm.ID);
        }

        [TestMethod]
        public async Task GetVirtualMachineAsync_InvalidVirtualMachineID_ThrowsANSClientValidationException()
        {
            var ops = new VirtualMachineOperations<VirtualMachine>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetVirtualMachineAsync(0));
        }

        [TestMethod]
        public async Task UpdateVirtualMachineAsync_ExpectedResult()
        {
            UpdateVirtualMachineRequest req = new UpdateVirtualMachineRequest()
            {
                Name = "testvm"
            };

            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            var ops = new VirtualMachineOperations<VirtualMachine>(client);
            await ops.UpdateVirtualMachineAsync(123, req);

            await client.Received().PatchAsync("/ecloud/v1/vms/123", req);
        }

        [TestMethod]
        public async Task UpdateVirtualMachineAsync_InvalidVirtualMachineID_ThrowsANSClientValidationException()
        {
            var ops = new VirtualMachineOperations<VirtualMachine>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.UpdateVirtualMachineAsync(0, null));
        }

        [TestMethod]
        public async Task DeleteVirtualMachineAsync_ValidParameters()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            var ops = new VirtualMachineOperations<VirtualMachine>(client);
            await ops.DeleteVirtualMachineAsync(123);

            await client.Received().DeleteAsync("/ecloud/v1/vms/123");
        }

        [TestMethod]
        public async Task DeleteVirtualMachineAsync_InvalidVirtualMachineID_ThrowsANSClientValidationException()
        {
            var ops = new VirtualMachineOperations<VirtualMachine>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.DeleteVirtualMachineAsync(0));
        }

        [TestMethod]
        public async Task CloneVirtualMachineAsync_ExpectedResult()
        {
            CloneVirtualMachineRequest req = new CloneVirtualMachineRequest()
            {
                Name = "testvm"
            };

            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.PostAsync<VirtualMachine>("/ecloud/v1/vms/123/clone", req).Returns(new VirtualMachine()
            {
                ID = 123
            });

            var ops = new VirtualMachineOperations<VirtualMachine>(client);
            int vmID = await ops.CloneVirtualMachineAsync(123, req);

            Assert.AreEqual(123, vmID);
        }

        [TestMethod]
        public async Task CloneVirtualMachineAsync_InvalidVirtualMachineID_ThrowsANSClientValidationException()
        {
            var ops = new VirtualMachineOperations<VirtualMachine>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.CloneVirtualMachineAsync(0, null));
        }

        [TestMethod]
        public async Task CreateVirtualMachineTemplateAsync_ExpectedResult()
        {
            CreateVirtualMachineTemplateRequest req = new CreateVirtualMachineTemplateRequest()
            {
                TemplateName = "testtemplate"
            };

            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            var ops = new VirtualMachineOperations<VirtualMachine>(client);
            await ops.CreateVirtualMachineTemplateAsync(123, req);

            await client.Received().PostAsync("/ecloud/v1/vms/123/clone-to-template", req);
        }

        [TestMethod]
        public async Task CreateVirtualMachineTemplateAsync_InvalidVirtualMachineID_ThrowsANSClientValidationException()
        {
            var ops = new VirtualMachineOperations<VirtualMachine>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.CreateVirtualMachineTemplateAsync(0, null));
        }

        [TestMethod]
        public async Task PowerOnVirtualMachineAsync_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            var ops = new VirtualMachineOperations<VirtualMachine>(client);
            await ops.PowerOnVirtualMachineAsync(123);

            await client.Received().PutAsync("/ecloud/v1/vms/123/power-on");
        }

        [TestMethod]
        public async Task PowerOnVirtualMachineAsync_InvalidVirtualMachineID_ThrowsANSClientValidationException()
        {
            var ops = new VirtualMachineOperations<VirtualMachine>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.PowerOnVirtualMachineAsync(0));
        }

        [TestMethod]
        public async Task PowerOffVirtualMachineAsync_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            var ops = new VirtualMachineOperations<VirtualMachine>(client);
            await ops.PowerOffVirtualMachineAsync(123);

            await client.Received().PutAsync("/ecloud/v1/vms/123/power-off");
        }

        [TestMethod]
        public async Task PowerOffVirtualMachineAsync_InvalidVirtualMachineID_ThrowsANSClientValidationException()
        {
            var ops = new VirtualMachineOperations<VirtualMachine>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.PowerOffVirtualMachineAsync(0));
        }

        [TestMethod]
        public async Task PowerResetVirtualMachineAsync_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            var ops = new VirtualMachineOperations<VirtualMachine>(client);
            await ops.PowerResetVirtualMachineAsync(123);

            await client.Received().PutAsync("/ecloud/v1/vms/123/power-reset");
        }

        [TestMethod]
        public async Task PowerResetVirtualMachineAsync_InvalidVirtualMachineID_ThrowsANSClientValidationException()
        {
            var ops = new VirtualMachineOperations<VirtualMachine>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.PowerResetVirtualMachineAsync(0));
        }

        [TestMethod]
        public async Task PowerShutdownVirtualMachineAsync_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            var ops = new VirtualMachineOperations<VirtualMachine>(client);
            await ops.PowerShutdownVirtualMachineAsync(123);

            await client.Received().PutAsync("/ecloud/v1/vms/123/power-shutdown");
        }

        [TestMethod]
        public async Task PowerShutdownVirtualMachineAsync_InvalidVirtualMachineID_ThrowsANSClientValidationException()
        {
            var ops = new VirtualMachineOperations<VirtualMachine>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.PowerShutdownVirtualMachineAsync(0));
        }

        [TestMethod]
        public async Task PowerRestartVirtualMachineAsync_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            var ops = new VirtualMachineOperations<VirtualMachine>(client);
            await ops.PowerRestartVirtualMachineAsync(123);

            await client.Received().PutAsync("/ecloud/v1/vms/123/power-restart");
        }

        [TestMethod]
        public async Task PowerRestartVirtualMachineAsync_InvalidVirtualMachineID_ThrowsANSClientValidationException()
        {
            var ops = new VirtualMachineOperations<VirtualMachine>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.PowerRestartVirtualMachineAsync(0));
        }
    }
}