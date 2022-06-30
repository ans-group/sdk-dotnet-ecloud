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
    public class DatastoreOperationsTests
    {
        [TestMethod]
        public async Task GetDatastoresAsync_ExpectedResult()
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

            var ops = new DatastoreOperations<Datastore>(client);
            var datastores = await ops.GetDatastoresAsync();

            Assert.AreEqual(2, datastores.Count);
        }

        [TestMethod]
        public async Task GetDatastoresPaginatedAsync_ExpectedClientCall()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetPaginatedAsync<Datastore>("/ecloud/v1/datastores").Returns(Task.Run(() =>
            {
                return new Paginated<Datastore>(client, "/ecloud/v1/datastores", null, new Response.ClientResponse<System.Collections.Generic.IList<Datastore>>()
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

            var ops = new DatastoreOperations<Datastore>(client);
            var paginated = await ops.GetDatastoresPaginatedAsync();

            Assert.AreEqual(2, paginated.Items.Count);
        }

        [TestMethod]
        public async Task GetDatastoreAsync_ValidParameters_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAsync<Datastore>("/ecloud/v1/datastores/123").Returns(new Datastore()
            {
                ID = 123
            });

            var ops = new DatastoreOperations<Datastore>(client);
            var datastore = await ops.GetDatastoreAsync(123);

            Assert.AreEqual(123, datastore.ID);
        }

        [TestMethod]
        public async Task GetDatastoreAsync_InvalidDatastoreID_ThrowsANSClientValidationException()
        {
            var ops = new DatastoreOperations<Datastore>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetDatastoreAsync(0));
        }
    }
}