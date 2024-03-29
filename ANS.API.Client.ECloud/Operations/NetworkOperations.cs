﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V2;
using ANS.API.Client.ECloud.Models.V2.Request;
using ANS.API.Client.Exception;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public class NetworkOperations<T> : ECloudOperations, INetworkOperations<T> where T : Network
    {
        public NetworkOperations(IANSECloudClient client) : base(client)
        {
        }

        public async Task<string> CreateNetworkAsync(CreateNetworkRequest req)
        {
            return (await Client.PostAsync<Network>("/ecloud/v2/networks", req)).ID;
        }

        public async Task<IList<T>> GetNetworksAsync(ClientRequestParameters parameters = null)
        {
            return await Client.GetAllAsync(GetNetworksPaginatedAsync, parameters);
        }

        public async Task<Paginated<T>> GetNetworksPaginatedAsync(ClientRequestParameters parameters = null)
        {
            return await Client.GetPaginatedAsync<T>("/ecloud/v2/networks", parameters);
        }

        public async Task<T> GetNetworkAsync(string networkID)
        {
            if (string.IsNullOrWhiteSpace(networkID))
            {
                throw new ANSClientValidationException("Invalid network id");
            }

            return await Client.GetAsync<T>($"/ecloud/v2/networks/{networkID}");
        }

        public async Task UpdateNetworkAsync(string networkID, UpdateNetworkRequest req)
        {
            if (string.IsNullOrWhiteSpace(networkID))
            {
                throw new ANSClientValidationException("Invalid network id");
            }

            await Client.PatchAsync($"/ecloud/v2/networks/{networkID}", req);
        }

        public async Task DeleteNetworkAsync(string networkID)
        {
            if (string.IsNullOrWhiteSpace(networkID))
            {
                throw new ANSClientValidationException("Invalid network id");
            }

            await Client.DeleteAsync($"/ecloud/v2/networks/{networkID}");
        }
    }
}