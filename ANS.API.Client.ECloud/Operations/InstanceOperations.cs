using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V2;
using ANS.API.Client.ECloud.Models.V2.Request;
using ANS.API.Client.Exception;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public class InstanceOperations<T> : ECloudOperations, IInstanceOperations<T> where T : Instance
    {
        public InstanceOperations(IANSECloudClient client) : base(client)
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
                throw new ANSClientValidationException("Invalid instance id");
            }

            return await Client.GetAsync<T>($"/ecloud/v2/instances/{instanceID}");
        }

        public async Task UpdateInstanceAsync(string instanceID, UpdateInstanceRequest req)
        {
            if (string.IsNullOrWhiteSpace(instanceID))
            {
                throw new ANSClientValidationException("Invalid instance id");
            }

            await Client.PatchAsync($"/ecloud/v2/instances/{instanceID}", req);
        }

        public async Task DeleteInstanceAsync(string instanceID)
        {
            if (string.IsNullOrWhiteSpace(instanceID))
            {
                throw new ANSClientValidationException("Invalid instance id");
            }

            await Client.DeleteAsync($"/ecloud/v2/instances/{instanceID}");
        }

        public async Task LockInstanceAsync(string instanceID)
        {
            if (string.IsNullOrWhiteSpace(instanceID))
            {
                throw new ANSClientValidationException("Invalid instance id");
            }

            await Client.PutAsync($"/ecloud/v2/instances/{instanceID}/lock");
        }

        public async Task UnlockInstanceAsync(string instanceID)
        {
            if (string.IsNullOrWhiteSpace(instanceID))
            {
                throw new ANSClientValidationException("Invalid instance id");
            }

            await Client.PutAsync($"/ecloud/v2/instances/{instanceID}/unlock");
        }

        public async Task PowerOnInstanceAsync(string instanceID)
        {
            if (string.IsNullOrWhiteSpace(instanceID))
            {
                throw new ANSClientValidationException("Invalid instance id");
            }

            await Client.PutAsync($"/ecloud/v2/instances/{instanceID}/power-on");
        }

        public async Task PowerOffInstanceAsync(string instanceID)
        {
            if (string.IsNullOrWhiteSpace(instanceID))
            {
                throw new ANSClientValidationException("Invalid instance id");
            }

            await Client.PutAsync($"/ecloud/v2/instances/{instanceID}/power-off");
        }

        public async Task PowerResetInstanceAsync(string instanceID)
        {
            if (string.IsNullOrWhiteSpace(instanceID))
            {
                throw new ANSClientValidationException("Invalid instance id");
            }

            await Client.PutAsync($"/ecloud/v2/instances/{instanceID}/power-reset");
        }

        public async Task PowerShutdownInstanceAsync(string instanceID)
        {
            if (string.IsNullOrWhiteSpace(instanceID))
            {
                throw new ANSClientValidationException("Invalid instance id");
            }

            await Client.PutAsync($"/ecloud/v2/instances/{instanceID}/power-shutdown");
        }

        public async Task PowerRestartInstanceAsync(string instanceID)
        {
            if (string.IsNullOrWhiteSpace(instanceID))
            {
                throw new ANSClientValidationException("Invalid instance id");
            }

            await Client.PutAsync($"/ecloud/v2/instances/{instanceID}/power-restart");
        }

        public async Task<IList<Volume>> GetInstanceVolumesAsync(string instanceID, ClientRequestParameters parameters = null)
        {
            if (string.IsNullOrWhiteSpace(instanceID))
            {
                throw new ANSClientValidationException("Invalid instance id");
            }
            
            return await Client.GetAllAsync(funcParams => GetInstanceVolumesPaginatedAsync(instanceID, funcParams), parameters);
        }

        public async Task<Paginated<Volume>> GetInstanceVolumesPaginatedAsync(string instanceID, ClientRequestParameters parameters = null)
        {
            if (string.IsNullOrWhiteSpace(instanceID))
            {
                throw new ANSClientValidationException("Invalid instance id");
            }

            return await Client.GetPaginatedAsync<Volume>($"/ecloud/v2/instances/{instanceID}/volumes", parameters);
        }

        public async Task<IList<Credential>> GetInstanceCredentialsAsync(string instanceID, ClientRequestParameters parameters = null)
        {
            if (string.IsNullOrWhiteSpace(instanceID))
            {
                throw new ANSClientValidationException("Invalid instance id");
            }

            return await Client.GetAllAsync(funcParams => GetInstanceCredentialsPaginatedAsync(instanceID, funcParams), parameters);
        }

        public async Task<Paginated<Credential>> GetInstanceCredentialsPaginatedAsync(string instanceID, ClientRequestParameters parameters = null)
        {
            if (string.IsNullOrWhiteSpace(instanceID))
            {
                throw new ANSClientValidationException("Invalid instance id");
            }

            return await Client.GetPaginatedAsync<Credential>($"/ecloud/v2/instances/{instanceID}/credentials", parameters);
        }

        public async Task<IList<NIC>> GetInstanceNICsAsync(string instanceID, ClientRequestParameters parameters = null)
        {
            if (string.IsNullOrWhiteSpace(instanceID))
            {
                throw new ANSClientValidationException("Invalid instance id");
            }

            return await Client.GetAllAsync(funcParams => GetInstanceNICsPaginatedAsync(instanceID, funcParams), parameters);
        }

        public async Task<Paginated<NIC>> GetInstanceNICsPaginatedAsync(string instanceID, ClientRequestParameters parameters = null)
        {
            if (string.IsNullOrWhiteSpace(instanceID))
            {
                throw new ANSClientValidationException("Invalid instance id");
            }

            return await Client.GetPaginatedAsync<NIC>($"/ecloud/v2/instances/{instanceID}/nics", parameters);
        }
    }
}