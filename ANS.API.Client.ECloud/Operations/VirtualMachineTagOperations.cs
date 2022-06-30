using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V1;
using ANS.API.Client.ECloud.Models.V1.Request;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public class VirtualMachineTagOperations<T> : ECloudOperations, IVirtualMachineTagOperations<T> where T : Tag
    {
        public VirtualMachineTagOperations(IANSECloudClient client) : base(client)
        {
        }

        public async Task<string> CreateVirtualMachineTagAsync(int vmID, CreateTagRequest req)
        {
            if (vmID < 1)
            {
                throw new Client.Exception.ANSClientValidationException("Invalid virtual machine id");
            }

            return (await this.Client.PostAsync<Tag>($"/ecloud/v1/vms/{vmID}/tags", req)).Key;
        }

        public async Task<IList<T>> GetVirtualMachineTagsAsync(int vmID, ClientRequestParameters parameters = null)
        {
            return await this.Client.GetAllAsync((ClientRequestParameters funcParameters) =>
                GetVirtualMachineTagsPaginatedAsync(vmID, funcParameters)
            , parameters);
        }

        public async Task<Paginated<T>> GetVirtualMachineTagsPaginatedAsync(int vmID, ClientRequestParameters parameters = null)
        {
            if (vmID < 1)
            {
                throw new Client.Exception.ANSClientValidationException("Invalid virtual machine id");
            }

            return await this.Client.GetPaginatedAsync<T>($"/ecloud/v1/vms/{vmID}/tags", parameters);
        }

        public async Task<T> GetVirtualMachineTagAsync(int vmID, string tagKey)
        {
            if (vmID < 1)
            {
                throw new Client.Exception.ANSClientValidationException("Invalid virtual machine id");
            }
            if (string.IsNullOrEmpty(tagKey))
            {
                throw new Client.Exception.ANSClientValidationException("Invalid tag key");
            }

            return await this.Client.GetAsync<T>($"/ecloud/v1/vms/{vmID}/tags/{tagKey}");
        }

        public async Task UpdateVirtualMachineTagAsync(int vmID, string tagKey, UpdateTagRequest req)
        {
            if (vmID < 1)
            {
                throw new Client.Exception.ANSClientValidationException("Invalid virtual machine id");
            }
            if (string.IsNullOrEmpty(tagKey))
            {
                throw new Client.Exception.ANSClientValidationException("Invalid tag key");
            }

            await this.Client.PatchAsync($"/ecloud/v1/vms/{vmID}/tags/{tagKey}", req);
        }

        public async Task DeleteVirtualMachineTagAsync(int vmID, string tagKey)
        {
            if (vmID < 1)
            {
                throw new Client.Exception.ANSClientValidationException("Invalid virtual machine id");
            }
            if (string.IsNullOrEmpty(tagKey))
            {
                throw new Client.Exception.ANSClientValidationException("Invalid tag key");
            }

            await this.Client.DeleteAsync($"/ecloud/v1/vms/{vmID}/tags/{tagKey}");
        }
    }
}