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
    public class DatastoreOperationsTests
    {
        [TestMethod]
        public async Task GetDatastoresAsync_ExpectedResult()
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

            var ops = new DatastoreOperations<Datastore>(client);
            var datastores = await ops.GetDatastoresAsync();

            Assert.AreEqual(2, datastores.Count);
        }

        [TestMethod]
        public async Task GetDatastoresPaginatedAsync_ExpectedClientCall()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

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
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetAsync<Datastore>("/ecloud/v1/datastores/123").Returns(new Datastore()
            {
                ID = 123
            });

            var ops = new DatastoreOperations<Datastore>(client);
            var datastore = await ops.GetDatastoreAsync(123);

            Assert.AreEqual(123, datastore.ID);
        }

        [TestMethod]
        public async Task GetDatastoreAsync_InvalidDatastoreID_ThrowsUKFastClientValidationException()
        {
            var ops = new DatastoreOperations<Datastore>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.GetDatastoreAsync(0));
        }
    }
}