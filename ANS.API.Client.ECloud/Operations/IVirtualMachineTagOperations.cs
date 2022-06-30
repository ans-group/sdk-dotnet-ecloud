using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V1;
using ANS.API.Client.ECloud.Models.V1.Request;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
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