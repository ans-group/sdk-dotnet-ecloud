using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public interface IPodOperations<T> where T : Pod
    {
        Task<IList<T>> GetPodsAsync(ClientRequestParameters parameters = null);
        Task<Paginated<T>> GetPodsPaginatedAsync(ClientRequestParameters parameters = null);
        Task<T> GetPodAsync(int podID);
    }
}
