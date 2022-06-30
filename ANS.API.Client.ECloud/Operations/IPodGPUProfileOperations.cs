using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V1;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public interface IPodGPUProfileOperations<T> where T : GPUProfile
    {
        Task<IList<T>> GetPodGPUProfilesAsync(int podID, ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetPodGPUProfilesPaginatedAsync(int podID, ClientRequestParameters parameters = null);
    }
}