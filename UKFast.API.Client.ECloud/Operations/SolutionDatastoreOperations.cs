﻿using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V1;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public class SolutionDatastoreOperations<T> : ECloudOperations, ISolutionDatastoreOperations<T> where T : Datastore
    {
        public SolutionDatastoreOperations(IUKFastECloudClient client) : base(client)
        {
        }

        public async Task<IList<T>> GetSolutionDatastoresAsync(int solutionID, ClientRequestParameters parameters = null)
        {
            return await this.Client.GetAllAsync((ClientRequestParameters funcParameters) => GetSolutionDatastoresPaginatedAsync(solutionID, funcParameters), parameters);
        }

        public async Task<Paginated<T>> GetSolutionDatastoresPaginatedAsync(int solutionID, ClientRequestParameters parameters = null)
        {
            if (solutionID < 1)
            {
                throw new Client.Exception.UKFastClientValidationException("Invalid solution id");
            }

            return await this.Client.GetPaginatedAsync<T>($"/ecloud/v1/solutions/{solutionID}/datastores", parameters);
        }
    }
}