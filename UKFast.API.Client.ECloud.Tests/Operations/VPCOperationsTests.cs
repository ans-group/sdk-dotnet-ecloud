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
    public class VPCOperationsTests
    {
        [TestMethod]
        public async Task CreateVPCAsync_ExpectedResult()
        {
            CreateVPCRequest req = new CreateVPCRequest()
            {
                Name = "test-vpc"
            };

            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();
            client.PostAsync<VPC>("/ecloud/v2/vpcs", req).Returns(new VPC()
            {
                ID = "vpc-abcd1234"
            });

            var ops = new VPCOperations<VPC>(client);
            var vpcID = await ops.CreateVPCAsync(req);

            Assert.AreEqual("vpc-abcd1234", vpcID);
        }

        [TestMethod]
        public async Task GetVPCsAsync_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetAllAsync(Arg.Any<UKFastClient.GetPaginatedAsyncFunc<VPC>>(), null).Returns(Task.Run<IList<VPC>>(() =>
            {
                return new List<VPC>()
                {
                    new VPC(),
                    new VPC()
                };
            }));

            var ops = new VPCOperations<VPC>(client);
            var vpcs = await ops.GetVPCsAsync();

            Assert.AreEqual(2, vpcs.Count);
        }

        [TestMethod]
        public async Task GetVPCsPaginatedAsync_ExpectedClientCall()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetPaginatedAsync<VPC>("/ecloud/v2/vpcs").Returns(Task.Run(() =>
            {
                return new Paginated<VPC>(client, "/ecloud/v2/vpcs", null,
                    new ClientResponse<IList<VPC>>()
                    {
                        Body = new ClientResponseBody<IList<VPC>>()
                        {
                            Data = new List<VPC>()
                            {
                                new VPC(),
                                new VPC()
                            }
                        }
                    });
            }));

            var ops = new VPCOperations<VPC>(client);
            var paginated = await ops.GetVPCsPaginatedAsync();

            Assert.AreEqual(2, paginated.Items.Count);
        }

        [TestMethod]
        public async Task GetVPCAsync_ValidParameters_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            string vpcID = "vpc-abcd1234";

            client.GetAsync<VPC>($"/ecloud/v2/vpcs/{vpcID}").Returns(new VPC()
            {
                ID = vpcID
            });

            var ops = new VPCOperations<VPC>(client);
            var vpc = await ops.GetVPCAsync(vpcID);

            Assert.AreEqual("vpc-abcd1234", vpc.ID);
        }

        [TestMethod]
        public async Task GetVPCAsync_InvalidVPCName_ThrowsUKFastClientValidationException()
        {
            var ops = new VPCOperations<VPC>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.GetVPCAsync(""));
        }

        [TestMethod]
        public async Task UpdateVPCAsync_ExpectedResult()
        {
            UpdateVPCRequest req = new UpdateVPCRequest()
            {
                Name = "test-vpc"
            };

            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            var ops = new VPCOperations<VPC>(client);
            await ops.UpdateVPCAsync("vpc-abcd1234", req);

            await client.Received().PatchAsync("/ecloud/v2/vpcs/vpc-abcd1234", req);
        }

        [TestMethod]
        public async Task UpdateVPCAsync_InvalidVPCID_ThrowsUKFastClientValidationException()
        {
            var ops = new VPCOperations<VPC>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.UpdateVPCAsync("", null));
        }

        [TestMethod]
        public async Task DeleteVPCAsync_ValidParameters()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            var ops = new VPCOperations<VPC>(client);
            await ops.DeleteVPCAsync("vpc-abcd1234");

            await client.Received().DeleteAsync("/ecloud/v2/vpcs/vpc-abcd1234");
        }

        [TestMethod]
        public async Task DeleteVPCAsync_InvalidVPCID_ThrowsUKFastClientValidationException()
        {
            var ops = new VPCOperations<VPC>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.DeleteVPCAsync(""));
        }
    }
}