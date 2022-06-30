using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ANS.API.Client.ECloud.Models.V1;
using ANS.API.Client.ECloud.Models.V1.Request;
using ANS.API.Client.ECloud.Operations;
using ANS.API.Client.Exception;
using ANS.API.Client.Models;

namespace ANS.API.Client.ECloud.Tests.Operations
{
    [TestClass]
    public class SolutionTemplateOperationsTests
    {
        [TestMethod]
        public async Task GetSolutionTemplatesAsync_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAllAsync(Arg.Any<ANSClient.GetPaginatedAsyncFunc<Template>>(), null).Returns(Task.Run<IList<Template>>(() =>
            {
                return new List<Template>()
                {
                    new Template(),
                    new Template()
                };
            }));

            var ops = new SolutionTemplateOperations<Template>(client);
            var solutions = await ops.GetSolutionTemplatesAsync(123);

            Assert.AreEqual(2, solutions.Count);
        }

        [TestMethod]
        public async Task GetSolutionTemplatesPaginatedAsync_ExpectedClientCall()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetPaginatedAsync<Template>("/ecloud/v1/solutions/123/templates").Returns(Task.Run(() =>
            {
                return new Paginated<Template>(client, "/ecloud/v1/solutions/123/templates", null, new Response.ClientResponse<System.Collections.Generic.IList<Template>>()
                {
                    Body = new Response.ClientResponseBody<System.Collections.Generic.IList<Template>>()
                    {
                        Data = new List<Template>()
                        {
                            new Template(),
                            new Template()
                        }
                    }
                });
            }));

            var ops = new SolutionTemplateOperations<Template>(client);
            var paginated = await ops.GetSolutionTemplatesPaginatedAsync(123);

            Assert.AreEqual(2, paginated.Items.Count);
        }

        [TestMethod]
        public async Task GetSolutionTemplatesPaginatedAsync_InvalidSolutionID_ThrowsANSClientValidationException()
        {
            var ops = new SolutionTemplateOperations<Template>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetSolutionTemplatesPaginatedAsync(0));
        }

        [TestMethod]
        public async Task GetSolutionTemplateAsync_ValidParameters_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAsync<Template>("/ecloud/v1/solutions/123/templates/testtemplate").Returns(new Template()
            {
                Name = "testtemplate"
            });

            var ops = new SolutionTemplateOperations<Template>(client);
            var solution = await ops.GetSolutionTemplateAsync(123, "testtemplate");

            Assert.AreEqual("testtemplate", solution.Name);
        }

        [TestMethod]
        public async Task GetSolutionTemplateAsync_InvalidSolutionID_ThrowsANSClientValidationException()
        {
            var ops = new SolutionTemplateOperations<Template>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetSolutionTemplateAsync(0, "testtemplate"));
        }

        [TestMethod]
        public async Task GetSolutionTemplateAsync_InvalidTemplateName_ThrowsANSClientValidationException()
        {
            var ops = new SolutionTemplateOperations<Template>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetSolutionTemplateAsync(123, ""));
        }

        [TestMethod]
        public async Task DeleteSolutionTemplateAsync_ValidParameters_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.DeleteAsync<Template>("/ecloud/v1/solutions/123/templates/testtemplate").Returns(new Template()
            {
                Name = "testtemplate"
            });

            var ops = new SolutionTemplateOperations<Template>(client);
            await ops.DeleteSolutionTemplateAsync(123, "testtemplate");

            await client.Received().DeleteAsync("/ecloud/v1/solutions/123/templates/testtemplate");
        }

        [TestMethod]
        public async Task DeleteSolutionTemplateAsync_InvalidSolutionID_ThrowsANSClientValidationException()
        {
            var ops = new SolutionTemplateOperations<Template>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.DeleteSolutionTemplateAsync(0, "testtemplate"));
        }

        [TestMethod]
        public async Task DeleteSolutionTemplateAsync_InvalidTemplateName_ThrowsANSClientValidationException()
        {
            var ops = new SolutionTemplateOperations<Template>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.DeleteSolutionTemplateAsync(123, ""));
        }

        [TestMethod]
        public async Task RenameSolutionTemplateAsync_ValidParameters_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            var req = new RenameTemplateRequest()
            {
                Destination = "newtemplate"
            };

            var ops = new SolutionTemplateOperations<Template>(client);
            await ops.RenameSolutionTemplateAsync(123, "testtemplate", req);

            await client.Received().PostAsync("/ecloud/v1/solutions/123/templates/testtemplate/move", req);
        }

        [TestMethod]
        public async Task RenameSolutionTemplateAsync_InvalidSolutionID_ThrowsANSClientValidationException()
        {
            var ops = new SolutionTemplateOperations<Template>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.RenameSolutionTemplateAsync(0, "testtemplate", null));
        }

        [TestMethod]
        public async Task RenameSolutionTemplateAsync_InvalidTemplateName_ThrowsANSClientValidationException()
        {
            var ops = new SolutionTemplateOperations<Template>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.RenameSolutionTemplateAsync(123, "", null));
        }
    }
}