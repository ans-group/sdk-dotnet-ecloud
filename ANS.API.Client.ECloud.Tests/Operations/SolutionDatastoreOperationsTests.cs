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
    public class SolutionDatastoreOperationsTests
    {
        [TestMethod]
        public async Task GetSolutionDatastoresAsync_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAllAsync(Arg.Any<ANSClient.GetPaginatedAsyncFunc<Datastore>>(), null).Returns(Task.Run<IList<Datastore>>(() =>
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
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

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
        public async Task GetSolutionDatastoresPaginatedAsync_InvalidSolutionID_ThrowsANSClientValidationException()
        {
            var ops = new SolutionDatastoreOperations<Datastore>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetSolutionDatastoresPaginatedAsync(0));
        }
    }
}