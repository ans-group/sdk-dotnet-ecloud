using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V1;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public interface IPodOperations<T> where T : Pod
    {
        Task<IList<T>> GetPodsAsync(ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetPodsPaginatedAsync(ClientRequestParameters parameters = null);

        Task<T> GetPodAsync(int podID);
    }
}