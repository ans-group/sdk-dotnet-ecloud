using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V1;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public interface IHostOperations<T> where T : Host
    {
        Task<IList<T>> GetHostsAsync(ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetHostsPaginatedAsync(ClientRequestParameters parameters = null);

        Task<T> GetHostAsync(int hostID);
    }
}