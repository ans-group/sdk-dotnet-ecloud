using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models;
using UKFast.API.Client.ECloud.Models.Request;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public class SolutionTemplateOperations<T> : ECloudOperations, ISolutionTemplateOperations<T> where T : Template
    {
        public SolutionTemplateOperations(IUKFastECloudClient client) : base(client) { }

        public async Task<IList<T>> GetSolutionTemplatesAsync(int solutionID, ClientRequestParameters parameters = null)
        {
            return await this.Client.GetAllAsync((ClientRequestParameters funcParameters) => GetSolutionTemplatesPaginatedAsync(solutionID, funcParameters), parameters);
        }

        public async Task<Paginated<T>> GetSolutionTemplatesPaginatedAsync(int solutionID, ClientRequestParameters parameters = null)
        {
            if (solutionID < 1)
            {
                throw new Client.Exception.UKFastClientValidationException("Invalid solution id");
            }

            return await this.Client.GetPaginatedAsync<T>($"/ecloud/v1/solutions/{solutionID}/templates", parameters);
        }

        public async Task<T> GetSolutionTemplateAsync(int solutionID, string templateName)
        {
            if (solutionID < 1)
            {
                throw new Client.Exception.UKFastClientValidationException("Invalid solution id");
            }
            if (string.IsNullOrEmpty(templateName))
            {
                throw new Client.Exception.UKFastClientValidationException("Invalid template name");
            }

            return await this.Client.GetAsync<T>($"/ecloud/v1/solutions/{solutionID}/templates/{templateName}");
        }

        public async Task DeleteSolutionTemplateAsync(int solutionID, string templateName)
        {
            if (solutionID < 1)
            {
                throw new Client.Exception.UKFastClientValidationException("Invalid solution id");
            }
            if (string.IsNullOrEmpty(templateName))
            {
                throw new Client.Exception.UKFastClientValidationException("Invalid template name");
            }

            await this.Client.DeleteAsync($"/ecloud/v1/solutions/{solutionID}/templates/{templateName}");
        }

        public async Task RenameSolutionTemplateAsync(int solutionID, string templateName, RenameTemplateRequest req)
        {
            if (solutionID < 1)
            {
                throw new Client.Exception.UKFastClientValidationException("Invalid solution id");
            }
            if (string.IsNullOrEmpty(templateName))
            {
                throw new Client.Exception.UKFastClientValidationException("Invalid template name");
            }

            await this.Client.PostAsync($"/ecloud/v1/solutions/{solutionID}/templates/{templateName}/move", req);
        }
    }
}
