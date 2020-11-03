using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V1;
using UKFast.API.Client.Exception;
using UKFast.API.Client.Models;

namespace UKFast.API.Client.ECloud.Operations.Tests
{
    [TestClass]
    public class SolutionDatastoreOperationsTests
    {
        [TestMethod]
        public async Task GetSolutionDatastoresAsync_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetAllAsync(Arg.Any<UKFastClient.GetPaginatedAsyncFunc<Datastore>>(), null).Returns(Task.Run<IList<Datastore>>(() =>
            {
                return new List<Datastore>()
                {
                    new Datastore(),
                    new Datastore()
                };
            }));

            var ops = new SolutionDatastoreOperations<Datastore>(client);
            var solutions = await ops.GetSolutionDatastoresAsync(123);

            Assert.AreEqual(2, solutions.Count);
        }

        [TestMethod]
        public async Task GetSolutionDatastoresPaginatedAsync_ExpectedClientCall()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetPaginatedAsync<Datastore>("/ecloud/v1/solutions/123/datastores").Returns(Task.Run(() =>
            {
                return new Paginated<Datastore>(client, "/ecloud/v1/solutions/123/datastores", null, new Response.ClientResponse<System.Collections.Generic.IList<Datastore>>()
                {
                    Body = new Response.ClientResponseBody<System.Collections.Generic.IList<Datastore>>()
                    {
                        Data = new List<Datastore>()
                        {
                            new Datastore(),
                            new Datastore()
                        }
                    }
                });
            }));

            var ops = new SolutionDatastoreOperations<Datastore>(client);
            var paginated = await ops.GetSolutionDatastoresPaginatedAsync(123);

            Assert.AreEqual(2, paginated.Items.Count);
        }

        [TestMethod]
        public async Task GetSolutionDatastoresPaginatedAsync_InvalidSolutionID_ThrowsUKFastClientValidationException()
        {
            var ops = new SolutionDatastoreOperations<Datastore>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.GetSolutionDatastoresPaginatedAsync(0));
        }
    }
}