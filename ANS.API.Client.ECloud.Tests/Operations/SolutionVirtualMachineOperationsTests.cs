using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ANS.API.Client.ECloud.Models.V1;
using ANS.API.Client.ECloud.Operations;
using ANS.API.Client.Exception;
using ANS.API.Client.Models;

namespace ANS.API.Client.ECloud.Tests.Operations
{
    [TestClass]
    public class SolutionVirtualMachineOperationsTests
    {
        [TestMethod]
        public async Task GetSolutionVirtualMachinesAsync_ExpectedResult()
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

            var ops = new SolutionVirtualMachineOperations<VirtualMachine>(client);
            var solutions = await ops.GetSolutionVirtualMachinesAsync(123);

            Assert.AreEqual(2, solutions.Count);
        }

        [TestMethod]
        public async Task GetSolutionVirtualMachinesPaginatedAsync_ExpectedClientCall()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetPaginatedAsync<VirtualMachine>("/ecloud/v1/solutions/123/vms").Returns(Task.Run(() =>
            {
                return new Paginated<VirtualMachine>(client, "/ecloud/v1/solutions/123/vms", null, new Response.ClientResponse<System.Collections.Generic.IList<VirtualMachine>>()
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

            var ops = new SolutionVirtualMachineOperations<VirtualMachine>(client);
            var paginated = await ops.GetSolutionVirtualMachinesPaginatedAsync(123);

            Assert.AreEqual(2, paginated.Items.Count);
        }

        [TestMethod]
        public async Task GetSolutionVirtualMachinesPaginatedAsync_InvalidSolutionID_ThrowsANSClientValidationException()
        {
            var ops = new SolutionVirtualMachineOperations<VirtualMachine>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetSolutionVirtualMachinesPaginatedAsync(0));
        }
    }
}