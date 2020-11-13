using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V1;
using UKFast.API.Client.ECloud.Models.V1.Request;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public interface IVirtualMachineTagOperations<T> where T : Tag
    {
        Task<string> CreateVirtualMachineTagAsync(int vmID, CreateTagRequest req);

        Task<IList<T>> GetVirtualMachineTagsAsync(int vmID, ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetVirtualMachineTagsPaginatedAsync(int vmID, ClientRequestParameters parameters = null);

        Task<T> GetVirtualMachineTagAsync(int vmID, string tagKey);

        Task UpdateVirtualMachineTagAsync(int vmID, string tagKey, UpdateTagRequest req);

        Task DeleteVirtualMachineTagAsync(int vmID, string tagKey);
    }
}