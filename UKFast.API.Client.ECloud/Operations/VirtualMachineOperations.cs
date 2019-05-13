using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models;
using UKFast.API.Client.ECloud.Models.Request;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public class VirtualMachineOperations<T> : ECloudOperations, IVirtualMachineOperations<T> where T : VirtualMachine
    {
        public VirtualMachineOperations(IUKFastECloudClient client) : base(client) { }

        public async Task<int> CreateVirtualMachineAsync(CreateVirtualMachineRequest req)
        {
            return (await this.Client.PostAsync<VirtualMachine>($"/ecloud/v1/vms", req)).ID;
        }

        public async Task<IList<T>> GetVirtualMachinesAsync(ClientRequestParameters parameters = null)
        {
            return await this.Client.GetAllAsync(GetVirtualMachinesPaginatedAsync, parameters);
        }

        public async Task<Paginated<T>> GetVirtualMachinesPaginatedAsync(ClientRequestParameters parameters = null)
        {
            return await this.Client.GetPaginatedAsync<T>("/ecloud/v1/vms", parameters);
        }

        public async Task<T> GetVirtualMachineAsync(int vmID)
        {
            if (vmID < 1)
            {
                throw new Client.Exception.UKFastClientValidationException("Invalid virtual machine id");
            }

            return await this.Client.GetAsync<T>($"/ecloud/v1/vms/{vmID}");
        }

        public async Task UpdateVirtualMachineAsync(int vmID, UpdateVirtualMachineRequest req)
        {
            if (vmID < 1)
            {
                throw new Client.Exception.UKFastClientValidationException("Invalid virtual machine id");
            }

            await this.Client.PatchAsync($"/ecloud/v1/vms/{vmID}", req);
        }

        public async Task DeleteVirtualMachineAsync(int vmID)
        {
            if (vmID < 1)
            {
                throw new Client.Exception.UKFastClientValidationException("Invalid virtual machine id");
            }

            await this.Client.DeleteAsync($"/ecloud/v1/vms/{vmID}");
        }

        public async Task<int> CloneVirtualMachineAsync(int vmID, CloneVirtualMachineRequest req)
        {
            if (vmID < 1)
            {
                throw new Client.Exception.UKFastClientValidationException("Invalid virtual machine id");
            }

            return (await this.Client.PostAsync<VirtualMachine>($"/ecloud/v1/vms/{vmID}/clone", req)).ID;
        }

        public async Task CreateVirtualMachineTemplateAsync(int vmID, CreateVirtualMachineTemplateRequest req)
        {
            if (vmID < 1)
            {
                throw new Client.Exception.UKFastClientValidationException("Invalid virtual machine id");
            }

            await this.Client.PostAsync($"/ecloud/v1/vms/{vmID}/clone-to-template", req);
        }

        public async Task PowerOnVirtualMachineAsync(int vmID)
        {
            await PowerActionVirtualMachineAsync(vmID, "on");
        }

        public async Task PowerOffVirtualMachineAsync(int vmID)
        {
            await PowerActionVirtualMachineAsync(vmID, "off");
        }

        public async Task PowerResetVirtualMachineAsync(int vmID)
        {
            await PowerActionVirtualMachineAsync(vmID, "reset");
        }

        public async Task PowerShutdownVirtualMachineAsync(int vmID)
        {
            await PowerActionVirtualMachineAsync(vmID, "shutdown");
        }

        public async Task PowerRestartVirtualMachineAsync(int vmID)
        {
            await PowerActionVirtualMachineAsync(vmID, "restart");
        }

        protected virtual async Task PowerActionVirtualMachineAsync(int vmID, string action)
        {
            if (vmID < 1)
            {
                throw new Client.Exception.UKFastClientValidationException("Invalid virtual machine id");
            }

            await this.Client.PutAsync($"/ecloud/v1/vms/{vmID}/power-{action}");
        }
    }
}
