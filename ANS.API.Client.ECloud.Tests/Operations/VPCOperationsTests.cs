using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NSubstitute.ExceptionExtensions;
using ANS.API.Client.ECloud.Models.V2;
using ANS.API.Client.ECloud.Models.V2.Request;
using ANS.API.Client.ECloud.Operations;
using ANS.API.Client.Exception;
using ANS.API.Client.Models;
using ANS.API.Client.Response;

namespace ANS.API.Client.ECloud.Tests.Operations
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

            IANSECloudClient client = Substitute.For<IANSECloudClient>();
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
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAllAsync(Arg.Any<ANSClient.GetPaginatedAsyncFunc<VPC>>(), null).Returns(Task.Run<IList<VPC>>(() =>
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
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

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
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

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
        public async Task GetVPCAsync_InvalidVPCName_ThrowsANSClientValidationException()
        {
            var ops = new VPCOperations<VPC>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetVPCAsync(""));
        }

        [TestMethod]
        public async Task GetVPCAsync_NotFound_ThrowsANSClientNotFoundRequestException()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAsync<VPC>("/ecloud/v2/vpcs/vpc-abcd1234").Throws(
                new ANSClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new VPCOperations<VPC>(client);

            await Assert.ThrowsExceptionAsync<ANSClientNotFoundRequestException>(() => ops.GetVPCAsync("vpc-abcd1234"));
        }

        [TestMethod]
        public async Task UpdateVPCAsync_ExpectedResult()
        {
            UpdateVPCRequest req = new UpdateVPCRequest()
            {
                Name = "test-vpc"
            };

            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            var ops = new VPCOperations<VPC>(client);
            await ops.UpdateVPCAsync("vpc-abcd1234", req);

            await client.Received().PatchAsync("/ecloud/v2/vpcs/vpc-abcd1234", req);
        }

        [TestMethod]
        public async Task UpdateVPCAsync_InvalidVPCID_ThrowsANSClientValidationException()
        {
            var ops = new VPCOperations<VPC>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.UpdateVPCAsync("", null));
        }

        [TestMethod]
        public async Task UpdateVPCAsync_NotFound_ThrowsANSClientNotFoundRequestException()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.PatchAsync("/ecloud/v2/vpcs/vpc-abcd1234").Throws(
                new ANSClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new VPCOperations<VPC>(client);

            await Assert.ThrowsExceptionAsync<ANSClientNotFoundRequestException>(() => ops.UpdateVPCAsync("vpc-abcd1234", null));
        }

        [TestMethod]
        public async Task DeleteVPCAsync_ValidParameters()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            var ops = new VPCOperations<VPC>(client);
            await ops.DeleteVPCAsync("vpc-abcd1234");

            await client.Received().DeleteAsync("/ecloud/v2/vpcs/vpc-abcd1234");
        }

        [TestMethod]
        public async Task DeleteVPCAsync_InvalidVPCID_ThrowsANSClientValidationException()
        {
            var ops = new VPCOperations<VPC>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.DeleteVPCAsync(""));
        }

        [TestMethod]
        public async Task DeleteVPCAsync_NotFound_ThrowsANSClientNotFoundRequestException()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.DeleteAsync("/ecloud/v2/vpcs/vpc-abcd1234").Throws(
                new ANSClientNotFoundRequestException(
                    new Collection<ClientResponseError> { new ClientResponseError { Status = 404 } }));

            var ops = new VPCOperations<VPC>(client);

            await Assert.ThrowsExceptionAsync<ANSClientNotFoundRequestException>(() => ops.DeleteVPCAsync("vpc-abcd1234"));
        }

    }
}