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
    public class PodTemplateOperationsTests
    {
        [TestMethod]
        public async Task GetPodTemplatesAsync_ExpectedResult()
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

            var ops = new PodTemplateOperations<Template>(client);
            var pods = await ops.GetPodTemplatesAsync(123);

            Assert.AreEqual(2, pods.Count);
        }

        [TestMethod]
        public async Task GetPodTemplatesPaginatedAsync_ExpectedClientCall()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetPaginatedAsync<Template>("/ecloud/v1/pods/123/templates").Returns(Task.Run(() =>
            {
                return new Paginated<Template>(client, "/ecloud/v1/pods/123/templates", null, new Response.ClientResponse<System.Collections.Generic.IList<Template>>()
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

            var ops = new PodTemplateOperations<Template>(client);
            var paginated = await ops.GetPodTemplatesPaginatedAsync(123);

            Assert.AreEqual(2, paginated.Items.Count);
        }

        [TestMethod]
        public async Task GetPodTemplatesPaginatedAsync_InvalidPodID_ThrowsANSClientValidationException()
        {
            var ops = new PodTemplateOperations<Template>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetPodTemplatesPaginatedAsync(0));
        }

        [TestMethod]
        public async Task GetPodTemplateAsync_ValidParameters_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.GetAsync<Template>("/ecloud/v1/pods/123/templates/testtemplate").Returns(new Template()
            {
                Name = "testtemplate"
            });

            var ops = new PodTemplateOperations<Template>(client);
            var pod = await ops.GetPodTemplateAsync(123, "testtemplate");

            Assert.AreEqual("testtemplate", pod.Name);
        }

        [TestMethod]
        public async Task GetPodTemplateAsync_InvalidPodID_ThrowsANSClientValidationException()
        {
            var ops = new PodTemplateOperations<Template>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetPodTemplateAsync(0, "testtemplate"));
        }

        [TestMethod]
        public async Task GetPodTemplateAsync_InvalidTemplateName_ThrowsANSClientValidationException()
        {
            var ops = new PodTemplateOperations<Template>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.GetPodTemplateAsync(123, ""));
        }

        [TestMethod]
        public async Task DeletePodTemplateAsync_ValidParameters_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            client.DeleteAsync<Template>("/ecloud/v1/pods/123/templates/testtemplate").Returns(new Template()
            {
                Name = "testtemplate"
            });

            var ops = new PodTemplateOperations<Template>(client);
            await ops.DeletePodTemplateAsync(123, "testtemplate");

            await client.Received().DeleteAsync("/ecloud/v1/pods/123/templates/testtemplate");
        }

        [TestMethod]
        public async Task DeletePodTemplateAsync_InvalidPodID_ThrowsANSClientValidationException()
        {
            var ops = new PodTemplateOperations<Template>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.DeletePodTemplateAsync(0, "testtemplate"));
        }

        [TestMethod]
        public async Task DeletePodTemplateAsync_InvalidTemplateName_ThrowsANSClientValidationException()
        {
            var ops = new PodTemplateOperations<Template>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.DeletePodTemplateAsync(123, ""));
        }

        [TestMethod]
        public async Task RenamePodTemplateAsync_ValidParameters_ExpectedResult()
        {
            IANSECloudClient client = Substitute.For<IANSECloudClient>();

            var req = new RenameTemplateRequest()
            {
                Destination = "newtemplate"
            };

            var ops = new PodTemplateOperations<Template>(client);
            await ops.RenamePodTemplateAsync(123, "testtemplate", req);

            await client.Received().PostAsync("/ecloud/v1/pods/123/templates/testtemplate/move", req);
        }

        [TestMethod]
        public async Task RenamePodTemplateAsync_InvalidPodID_ThrowsANSClientValidationException()
        {
            var ops = new PodTemplateOperations<Template>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.RenamePodTemplateAsync(0, "testtemplate", null));
        }

        [TestMethod]
        public async Task RenamePodTemplateAsync_InvalidTemplateName_ThrowsANSClientValidationException()
        {
            var ops = new PodTemplateOperations<Template>(null);

            await Assert.ThrowsExceptionAsync<ANSClientValidationException>(() => ops.RenamePodTemplateAsync(123, "", null));
        }
    }
}