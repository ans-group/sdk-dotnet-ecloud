using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V2;
using UKFast.API.Client.ECloud.Models.V2.Request;
using UKFast.API.Client.Exception;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public class InstanceOperations<T> : ECloudOperations, IInstanceOperations<T> where T : Instance
    {
        public InstanceOperations(IUKFastECloudClient client) : base(client)
        {
        }

        public async Task<string> CreateInstanceAsync(CreateInstanceRequest req)
        {
            return (await Client.PostAsync<Instance>($"/ecloud/v2/instances", req)).ID;
        }

        public async Task<IList<T>> GetInstancesAsync(ClientRequestParameters parameters = null)
        {
            return await Client.GetAllAsync(GetInstancesPaginatedAsync, parameters);
        }

        public async Task<Paginated<T>> GetInstancesPaginatedAsync(ClientRequestParameters parameters = null)
        {
            return await Client.GetPaginatedAsync<T>("/ecloud/v2/instances", parameters);
        }

        public async Task<T> GetInstanceAsync(string instanceID)
        {
            if (string.IsNullOrWhiteSpace(instanceID))
            {
                throw new UKFastClientValidationException("Invalid instance id");
            }

            return await Client.GetAsync<T>($"/ecloud/v2/instances/{instanceID}");
        }

        public async Task UpdateInstanceAsync(string instanceID, UpdateInstanceRequest req)
        {
            if (string.IsNullOrWhiteSpace(instanceID))
            {
                throw new UKFastClientValidationException("Invalid instance id");
            }

            await Client.PatchAsync($"/ecloud/v2/instances/{instanceID}", req);
        }

        public async Task DeleteInstanceAsync(string instanceID)
        {
            if (string.IsNullOrWhiteSpace(instanceID))
            {
                throw new UKFastClientValidationException("Invalid instance id");
            }

            await Client.DeleteAsync($"/ecloud/v2/instances/{instanceID}");
        }

        public async Task PowerOnInstanceAsync(string instanceID)
        {
            if (string.IsNullOrWhiteSpace(instanceID))
            {
                throw new UKFastClientValidationException("Invalid instance id");
            }

            await Client.PutAsync($"/ecloud/v2/instances/{instanceID}/power-on");
        }

        public async Task PowerOffInstanceAsync(string instanceID)
        {
            if (string.IsNullOrWhiteSpace(instanceID))
            {
                throw new UKFastClientValidationException("Invalid instance id");
            }

            await Client.PutAsync($"/ecloud/v2/instances/{instanceID}/power-off");
        }

        public async Task PowerResetInstanceAsync(string instanceID)
        {
            if (string.IsNullOrWhiteSpace(instanceID))
            {
                throw new UKFastClientValidationException("Invalid instance id");
            }

            await Client.PutAsync($"/ecloud/v2/instances/{instanceID}/power-reset");
        }

        public async Task PowerShutdownInstanceAsync(string instanceID)
        {
            if (string.IsNullOrWhiteSpace(instanceID))
            {
                throw new UKFastClientValidationException("Invalid instance id");
            }

            await Client.PutAsync($"/ecloud/v2/instances/{instanceID}/power-shutdown");
        }

        public async Task PowerRestartInstanceAsync(string instanceID)
        {
            if (string.IsNullOrWhiteSpace(instanceID))
            {
                throw new UKFastClientValidationException("Invalid instance id");
            }

            await Client.PutAsync($"/ecloud/v2/instances/{instanceID}/power-restart");
        }
    }
}