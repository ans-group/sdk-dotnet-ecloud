using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V1;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public interface IHostOperations<T> where T : Host
    {
        Task<IList<T>> GetHostsAsync(ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetHostsPaginatedAsync(ClientRequestParameters parameters = null);

        Task<T> GetHostAsync(int hostID);
    }
}