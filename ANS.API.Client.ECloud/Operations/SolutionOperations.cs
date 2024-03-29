﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V1;
using ANS.API.Client.ECloud.Models.V1.Request;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public class SolutionOperations<T> : ECloudOperations, ISolutionOperations<T> where T : Solution
    {
        public SolutionOperations(IANSECloudClient client) : base(client)
        {
        }

        public async Task<IList<T>> GetSolutionsAsync(ClientRequestParameters parameters = null)
        {
            return await this.Client.GetAllAsync(GetSolutionsPaginatedAsync, parameters);
        }

        public async Task<Paginated<T>> GetSolutionsPaginatedAsync(ClientRequestParameters parameters = null)
        {
            return await this.Client.GetPaginatedAsync<T>("/ecloud/v1/solutions", parameters);
        }

        public async Task<T> GetSolutionAsync(int solutionID)
        {
            if (solutionID < 1)
            {
                throw new Client.Exception.ANSClientValidationException("Invalid solution id");
            }

            return await this.Client.GetAsync<T>($"/ecloud/v1/solutions/{solutionID}");
        }

        public async Task UpdateSolutionAsync(int solutionID, UpdateSolutionRequest req)
        {
            if (solutionID < 1)
            {
                throw new Client.Exception.ANSClientValidationException("Invalid solution id");
            }

            await this.Client.PatchAsync($"/ecloud/v1/solutions/{solutionID}", req);
        }
    }
}