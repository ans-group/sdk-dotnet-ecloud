using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V1;
using UKFast.API.Client.ECloud.Models.V1.Request;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public interface IVirtualMachineOperations<T> where T : VirtualMachine
    {
        Task<int> CreateVirtualMachineAsync(CreateVirtualMachineRequest req);

        Task<IList<T>> GetVirtualMachinesAsync(ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetVirtualMachinesPaginatedAsync(ClientRequestParameters parameters = null);

        Task<T> GetVirtualMachineAsync(int vmID);

        Task UpdateVirtualMachineAsync(int vmID, UpdateVirtualMachineRequest req);

        Task DeleteVirtualMachineAsync(int vmID);

        Task<int> CloneVirtualMachineAsync(int vmID, CloneVirtualMachineRequest req);

        Task CreateVirtualMachineTemplateAsync(int vmID, CreateVirtualMachineTemplateRequest req);

        Task PowerOnVirtualMachineAsync(int vmID);

        Task PowerOffVirtualMachineAsync(int vmID);

        Task PowerResetVirtualMachineAsync(int vmID);

        Task PowerShutdownVirtualMachineAsync(int vmID);

        Task PowerRestartVirtualMachineAsync(int vmID);
    }
}