using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ANS.API.Client.ECloud.Models.V1;
using ANS.API.Client.ECloud.Operations;
using ANS.API.Client.Models;

namespace ANS.API.Client.ECloud.Tests.Operations
{
    [TestClass]
    public class CreditOperationsTests
    {
        [TestMethod]
        public async Task GetCreditsAsync_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAllAsync(Arg.Any<ANSClient.GetPaginatedAsyncFunc<Credit>>(), null).Returns(Task.Run<IList<Credit>>(() =>
            {
                return new List<Credit>()
                {
                    new Credit(),
                    new Credit()
                };
            }));

            var ops = new CreditOperations<Credit>(client);
            var credits = await ops.GetCreditsAsync();

            Assert.AreEqual(2, credits.Count);
        }

        [TestMethod]
        public async Task GetCreditsPaginatedAsync_ExpectedClientCall()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetPaginatedAsync<Credit>("/ecloud/v1/credits").Returns(Task.Run(() =>
            {
                return new Paginated<Credit>(client, "/ecloud/v1/credits", null, new Response.ClientResponse<System.Collections.Generic.IList<Credit>>()
                {
                    Body = new Response.ClientResponseBody<System.Collections.Generic.IList<Credit>>()
                    {
                        Data = new List<Credit>()
                        {
                            new Credit(),
                            new Credit()
                        }
                    }
                });
            }));

            var ops = new CreditOperations<Credit>(client);
            var paginated = await ops.GetCreditsPaginatedAsync();

            Assert.AreEqual(2, paginated.Items.Count);
        }
    }
}