using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V1;
using ANS.API.Client.ECloud.Models.V1.Request;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public class SolutionTemplateOperations<T> : ECloudOperations, ISolutionTemplateOperations<T> where T : Template
    {
        public SolutionTemplateOperations(IANSECloudClient client) : base(client)
        {
        }

        public async Task<IList<T>> GetSolutionTemplatesAsync(int solutionID, ClientRequestParameters parameters = null)
        {
            return await this.Client.GetAllAsync((ClientRequestParameters funcParameters) => GetSolutionTemplatesPaginatedAsync(solutionID, funcParameters), parameters);
        }

        public async Task<Paginated<T>> GetSolutionTemplatesPaginatedAsync(int solutionID, ClientRequestParameters parameters = null)
        {
            if (solutionID < 1)
            {
                throw new Client.Exception.ANSClientValidationException("Invalid solution id");
            }

            return await this.Client.GetPaginatedAsync<T>($"/ecloud/v1/solutions/{solutionID}/templates", parameters);
        }

        public async Task<T> GetSolutionTemplateAsync(int solutionID, string templateName)
        {
            if (solutionID < 1)
            {
                throw new Client.Exception.ANSClientValidationException("Invalid solution id");
            }
            if (string.IsNullOrEmpty(templateName))
            {
                throw new Client.Exception.ANSClientValidationException("Invalid template name");
            }

            return await this.Client.GetAsync<T>($"/ecloud/v1/solutions/{solutionID}/templates/{templateName}");
        }

        public async Task DeleteSolutionTemplateAsync(int solutionID, string templateName)
        {
            if (solutionID < 1)
            {
                throw new Client.Exception.ANSClientValidationException("Invalid solution id");
            }
            if (string.IsNullOrEmpty(templateName))
            {
                throw new Client.Exception.ANSClientValidationException("Invalid template name");
            }

            await this.Client.DeleteAsync($"/ecloud/v1/solutions/{solutionID}/templates/{templateName}");
        }

        public async Task RenameSolutionTemplateAsync(int solutionID, string templateName, RenameTemplateRequest req)
        {
            if (solutionID < 1)
            {
                throw new Client.Exception.ANSClientValidationException("Invalid solution id");
            }
            if (string.IsNullOrEmpty(templateName))
            {
                throw new Client.Exception.ANSClientValidationException("Invalid template name");
            }

            await this.Client.PostAsync($"/ecloud/v1/solutions/{solutionID}/templates/{templateName}/move", req);
        }
    }
}