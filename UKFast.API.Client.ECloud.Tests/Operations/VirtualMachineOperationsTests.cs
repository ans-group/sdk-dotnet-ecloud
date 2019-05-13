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
    public class VirtualMachineOperationsTests
    {
        [TestMethod]
        public async Task CreateVirtualMachineAsync_ExpectedResult()
        {
            CreateVirtualMachineRequest req = new CreateVirtualMachineRequest()
            {
                Template = "testtemplate"
            };

            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();
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
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetAllAsync(Arg.Any<UKFastClient.GetPaginatedAsyncFunc<VirtualMachine>>(), null).Returns(Task.Run<IList<VirtualMachine>>(() =>
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
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

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
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetAsync<VirtualMachine>("/ecloud/v1/vms/123").Returns(new VirtualMachine()
            {
                ID = 123
            });

            var ops = new VirtualMachineOperations<VirtualMachine>(client);
            var vm = await ops.GetVirtualMachineAsync(123);

            Assert.AreEqual(123, vm.ID);
        }

        [TestMethod]
        public async Task GetVirtualMachineAsync_InvalidVirtualMachineID_ThrowsUKFastClientValidationException()
        {
            var ops = new VirtualMachineOperations<VirtualMachine>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.GetVirtualMachineAsync(0));
        }

        [TestMethod]
        public async Task UpdateVirtualMachineAsync_ExpectedResult()
        {
            UpdateVirtualMachineRequest req = new UpdateVirtualMachineRequest()
            {
                Name = "testvm"
            };

            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            var ops = new VirtualMachineOperations<VirtualMachine>(client);
            await ops.UpdateVirtualMachineAsync(123, req);

            await client.Received().PatchAsync("/ecloud/v1/vms/123", req);
        }

        [TestMethod]
        public async Task UpdateVirtualMachineAsync_InvalidVirtualMachineID_ThrowsUKFastClientValidationException()
        {
            var ops = new VirtualMachineOperations<VirtualMachine>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.UpdateVirtualMachineAsync(0, null));
        }

        [TestMethod]
        public async Task DeleteVirtualMachineAsync_ValidParameters()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            var ops = new VirtualMachineOperations<VirtualMachine>(client);
            await ops.DeleteVirtualMachineAsync(123);

            await client.Received().DeleteAsync("/ecloud/v1/vms/123");
        }

        [TestMethod]
        public async Task DeleteVirtualMachineAsync_InvalidVirtualMachineID_ThrowsUKFastClientValidationException()
        {
            var ops = new VirtualMachineOperations<VirtualMachine>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.DeleteVirtualMachineAsync(0));
        }

        [TestMethod]
        public async Task CloneVirtualMachineAsync_ExpectedResult()
        {
            CloneVirtualMachineRequest req = new CloneVirtualMachineRequest()
            {
                Name = "testvm"
            };

            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.PostAsync<VirtualMachine>("/ecloud/v1/vms/123/clone", req).Returns(new VirtualMachine()
            {
                ID = 123
            });

            var ops = new VirtualMachineOperations<VirtualMachine>(client);
            int vmID = await ops.CloneVirtualMachineAsync(123, req);

            Assert.AreEqual(123, vmID);
        }

        [TestMethod]
        public async Task CloneVirtualMachineAsync_InvalidVirtualMachineID_ThrowsUKFastClientValidationException()
        {
            var ops = new VirtualMachineOperations<VirtualMachine>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.CloneVirtualMachineAsync(0, null));
        }

        [TestMethod]
        public async Task CreateVirtualMachineTemplateAsync_ExpectedResult()
        {
            CreateVirtualMachineTemplateRequest req = new CreateVirtualMachineTemplateRequest()
            {
                TemplateName = "testtemplate"
            };

            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            var ops = new VirtualMachineOperations<VirtualMachine>(client);
            await ops.CreateVirtualMachineTemplateAsync(123, req);

            await client.Received().PostAsync("/ecloud/v1/vms/123/clone-to-template", req);
        }

        [TestMethod]
        public async Task CreateVirtualMachineTemplateAsync_InvalidVirtualMachineID_ThrowsUKFastClientValidationException()
        {
            var ops = new VirtualMachineOperations<VirtualMachine>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.CreateVirtualMachineTemplateAsync(0, null));
        }

        [TestMethod]
        public async Task PowerOnVirtualMachineAsync_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            var ops = new VirtualMachineOperations<VirtualMachine>(client);
            await ops.PowerOnVirtualMachineAsync(123);

            await client.Received().PutAsync("/ecloud/v1/vms/123/power-on");
        }

        [TestMethod]
        public async Task PowerOnVirtualMachineAsync_InvalidVirtualMachineID_ThrowsUKFastClientValidationException()
        {
            var ops = new VirtualMachineOperations<VirtualMachine>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.PowerOnVirtualMachineAsync(0));
        }

        [TestMethod]
        public async Task PowerOffVirtualMachineAsync_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            var ops = new VirtualMachineOperations<VirtualMachine>(client);
            await ops.PowerOffVirtualMachineAsync(123);

            await client.Received().PutAsync("/ecloud/v1/vms/123/power-off");
        }

        [TestMethod]
        public async Task PowerOffVirtualMachineAsync_InvalidVirtualMachineID_ThrowsUKFastClientValidationException()
        {
            var ops = new VirtualMachineOperations<VirtualMachine>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.PowerOffVirtualMachineAsync(0));
        }

        [TestMethod]
        public async Task PowerResetVirtualMachineAsync_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            var ops = new VirtualMachineOperations<VirtualMachine>(client);
            await ops.PowerResetVirtualMachineAsync(123);

            await client.Received().PutAsync("/ecloud/v1/vms/123/power-reset");
        }

        [TestMethod]
        public async Task PowerResetVirtualMachineAsync_InvalidVirtualMachineID_ThrowsUKFastClientValidationException()
        {
            var ops = new VirtualMachineOperations<VirtualMachine>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.PowerResetVirtualMachineAsync(0));
        }

        [TestMethod]
        public async Task PowerShutdownVirtualMachineAsync_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            var ops = new VirtualMachineOperations<VirtualMachine>(client);
            await ops.PowerShutdownVirtualMachineAsync(123);

            await client.Received().PutAsync("/ecloud/v1/vms/123/power-shutdown");
        }

        [TestMethod]
        public async Task PowerShutdownVirtualMachineAsync_InvalidVirtualMachineID_ThrowsUKFastClientValidationException()
        {
            var ops = new VirtualMachineOperations<VirtualMachine>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.PowerShutdownVirtualMachineAsync(0));
        }

        [TestMethod]
        public async Task PowerRestartVirtualMachineAsync_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            var ops = new VirtualMachineOperations<VirtualMachine>(client);
            await ops.PowerRestartVirtualMachineAsync(123);

            await client.Received().PutAsync("/ecloud/v1/vms/123/power-restart");
        }

        [TestMethod]
        public async Task PowerRestartVirtualMachineAsync_InvalidVirtualMachineID_ThrowsUKFastClientValidationException()
        {
            var ops = new VirtualMachineOperations<VirtualMachine>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.PowerRestartVirtualMachineAsync(0));
        }
    }
}
