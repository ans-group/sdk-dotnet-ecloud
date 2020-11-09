using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using UKFast.API.Client.ECloud.Models.V1;
using UKFast.API.Client.ECloud.Operations;
using UKFast.API.Client.Exception;
using UKFast.API.Client.Models;

namespace UKFast.API.Client.ECloud.Tests.Operations
{
    [TestClass]
    public class ApplianceParameterOperationsTests
    {
        [TestMethod]
        public async Task GetApplianceParametersAsync_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetAllAsync(Arg.Any<UKFastClient.GetPaginatedAsyncFunc<ApplianceParameter>>(), null).Returns(Task.Run<IList<ApplianceParameter>>(() =>
            {
                return new List<ApplianceParameter>()
                 {
                        new ApplianceParameter(),
                        new ApplianceParameter()
                 };
            }));

            var ops = new ApplianceParameterOperations<ApplianceParameter>(client);
            var parameters = await ops.GetApplianceParametersAsync("00000000-0000-0000-0000-000000000000");

            Assert.AreEqual(2, parameters.Count);
        }

        [TestMethod]
        public async Task GetApplianceParametersPaginatedAsync_ExpectedClientCall()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetPaginatedAsync<ApplianceParameter>("/ecloud/v1/appliances/00000000-0000-0000-0000-000000000000/parameters").Returns(Task.Run(() =>
            {
                return new Paginated<ApplianceParameter>(client, "/ecloud/v1/appliances/00000000-0000-0000-0000-000000000000/parameters", null, new Response.ClientResponse<System.Collections.Generic.IList<ApplianceParameter>>()
                {
                    Body = new Response.ClientResponseBody<System.Collections.Generic.IList<ApplianceParameter>>()
                    {
                        Data = new List<ApplianceParameter>()
                        {
                            new ApplianceParameter(),
                            new ApplianceParameter()
                        }
                    }
                });
            }));

            var ops = new ApplianceParameterOperations<ApplianceParameter>(client);
            var paginated = await ops.GetApplianceParametersPaginatedAsync("00000000-0000-0000-0000-000000000000");

            Assert.AreEqual(2, paginated.Items.Count);
        }

        [TestMethod]
        public async Task GetApplianceParametersPaginatedAsync_InvalidApplianceID_ThrowsUKFastClientValidationException()
        {
            var ops = new ApplianceParameterOperations<ApplianceParameter>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.GetApplianceParametersPaginatedAsync(""));
        }
    }
}