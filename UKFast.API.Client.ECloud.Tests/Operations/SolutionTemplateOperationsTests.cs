using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using UKFast.API.Client.ECloud.Models.V1;
using UKFast.API.Client.ECloud.Models.V1.Request;
using UKFast.API.Client.ECloud.Operations;
using UKFast.API.Client.Exception;
using UKFast.API.Client.Models;

namespace UKFast.API.Client.ECloud.Tests.Operations
{
    [TestClass]
    public class SolutionTemplateOperationsTests
    {
        [TestMethod]
        public async Task GetSolutionTemplatesAsync_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetAllAsync(Arg.Any<UKFastClient.GetPaginatedAsyncFunc<Template>>(), null).Returns(Task.Run<IList<Template>>(() =>
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
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

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
        public async Task GetSolutionTemplatesPaginatedAsync_InvalidSolutionID_ThrowsUKFastClientValidationException()
        {
            var ops = new SolutionTemplateOperations<Template>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.GetSolutionTemplatesPaginatedAsync(0));
        }

        [TestMethod]
        public async Task GetSolutionTemplateAsync_ValidParameters_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.GetAsync<Template>("/ecloud/v1/solutions/123/templates/testtemplate").Returns(new Template()
            {
                Name = "testtemplate"
            });

            var ops = new SolutionTemplateOperations<Template>(client);
            var solution = await ops.GetSolutionTemplateAsync(123, "testtemplate");

            Assert.AreEqual("testtemplate", solution.Name);
        }

        [TestMethod]
        public async Task GetSolutionTemplateAsync_InvalidSolutionID_ThrowsUKFastClientValidationException()
        {
            var ops = new SolutionTemplateOperations<Template>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.GetSolutionTemplateAsync(0, "testtemplate"));
        }

        [TestMethod]
        public async Task GetSolutionTemplateAsync_InvalidTemplateName_ThrowsUKFastClientValidationException()
        {
            var ops = new SolutionTemplateOperations<Template>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.GetSolutionTemplateAsync(123, ""));
        }

        [TestMethod]
        public async Task DeleteSolutionTemplateAsync_ValidParameters_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            client.DeleteAsync<Template>("/ecloud/v1/solutions/123/templates/testtemplate").Returns(new Template()
            {
                Name = "testtemplate"
            });

            var ops = new SolutionTemplateOperations<Template>(client);
            await ops.DeleteSolutionTemplateAsync(123, "testtemplate");

            await client.Received().DeleteAsync("/ecloud/v1/solutions/123/templates/testtemplate");
        }

        [TestMethod]
        public async Task DeleteSolutionTemplateAsync_InvalidSolutionID_ThrowsUKFastClientValidationException()
        {
            var ops = new SolutionTemplateOperations<Template>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.DeleteSolutionTemplateAsync(0, "testtemplate"));
        }

        [TestMethod]
        public async Task DeleteSolutionTemplateAsync_InvalidTemplateName_ThrowsUKFastClientValidationException()
        {
            var ops = new SolutionTemplateOperations<Template>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.DeleteSolutionTemplateAsync(123, ""));
        }

        [TestMethod]
        public async Task RenameSolutionTemplateAsync_ValidParameters_ExpectedResult()
        {
            IUKFastECloudClient client = Substitute.For<IUKFastECloudClient>();

            var req = new RenameTemplateRequest()
            {
                Destination = "newtemplate"
            };

            var ops = new SolutionTemplateOperations<Template>(client);
            await ops.RenameSolutionTemplateAsync(123, "testtemplate", req);

            await client.Received().PostAsync("/ecloud/v1/solutions/123/templates/testtemplate/move", req);
        }

        [TestMethod]
        public async Task RenameSolutionTemplateAsync_InvalidSolutionID_ThrowsUKFastClientValidationException()
        {
            var ops = new SolutionTemplateOperations<Template>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.RenameSolutionTemplateAsync(0, "testtemplate", null));
        }

        [TestMethod]
        public async Task RenameSolutionTemplateAsync_InvalidTemplateName_ThrowsUKFastClientValidationException()
        {
            var ops = new SolutionTemplateOperations<Template>(null);

            await Assert.ThrowsExceptionAsync<UKFastClientValidationException>(() => ops.RenameSolutionTemplateAsync(123, "", null));
        }
    }
}