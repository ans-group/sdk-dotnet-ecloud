using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public interface IPodGPUProfileOperations<T> where T : GPUProfile
    {
        Task<IList<T>> GetPodGPUProfilesAsync(int podID, ClientRequestParameters parameters = null);
        Task<Paginated<T>> GetPodGPUProfilesPaginatedAsync(int podID, ClientRequestParameters parameters = null);
    }
}
