using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V1;
using UKFast.API.Client.ECloud.Models.V1.Request;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public class SolutionTagOperations<T> : ECloudOperations, ISolutionTagOperations<T> where T : Tag
    {
        public SolutionTagOperations(IUKFastECloudClient client) : base(client)
        {
        }

        public async Task<string> CreateSolutionTagAsync(int solutionID, CreateTagRequest req)
        {
            if (solutionID < 1)
            {
                throw new Client.Exception.UKFastClientValidationException("Invalid solution id");
            }

            return (await this.Client.PostAsync<Tag>($"/ecloud/v1/solutions/{solutionID}/tags", req)).Key;
        }

        public async Task<IList<T>> GetSolutionTagsAsync(int solutionID, ClientRequestParameters parameters = null)
        {
            return await this.Client.GetAllAsync((ClientRequestParameters funcParameters) => GetSolutionTagsPaginatedAsync(solutionID, funcParameters), parameters);
        }

        public async Task<Paginated<T>> GetSolutionTagsPaginatedAsync(int solutionID, ClientRequestParameters parameters = null)
        {
            if (solutionID < 1)
            {
                throw new Client.Exception.UKFastClientValidationException("Invalid solution id");
            }

            return await this.Client.GetPaginatedAsync<T>($"/ecloud/v1/solutions/{solutionID}/tags", parameters);
        }

        public async Task<T> GetSolutionTagAsync(int solutionID, string tagKey)
        {
            if (solutionID < 1)
            {
                throw new Client.Exception.UKFastClientValidationException("Invalid solution id");
            }
            if (string.IsNullOrEmpty(tagKey))
            {
                throw new Client.Exception.UKFastClientValidationException("Invalid tag key");
            }

            return await this.Client.GetAsync<T>($"/ecloud/v1/solutions/{solutionID}/tags/{tagKey}");
        }

        public async Task UpdateSolutionTagAsync(int solutionID, string tagKey, UpdateTagRequest req)
        {
            if (solutionID < 1)
            {
                throw new Client.Exception.UKFastClientValidationException("Invalid solution id");
            }
            if (string.IsNullOrEmpty(tagKey))
            {
                throw new Client.Exception.UKFastClientValidationException("Invalid tag key");
            }

            await this.Client.PatchAsync($"/ecloud/v1/solutions/{solutionID}/tags/{tagKey}", req);
        }

        public async Task DeleteSolutionTagAsync(int solutionID, string tagKey)
        {
            if (solutionID < 1)
            {
                throw new Client.Exception.UKFastClientValidationException("Invalid solution id");
            }
            if (string.IsNullOrEmpty(tagKey))
            {
                throw new Client.Exception.UKFastClientValidationException("Invalid tag key");
            }

            await this.Client.DeleteAsync($"/ecloud/v1/solutions/{solutionID}/tags/{tagKey}");
        }
    }
}