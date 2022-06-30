using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V2;
using ANS.API.Client.ECloud.Models.V2.Request;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public interface INetworkOperations<T> where T : Network
    {
        Task<string> CreateNetworkAsync(CreateNetworkRequest req);

        Task<IList<T>> GetNetworksAsync(ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetNetworksPaginatedAsync(ClientRequestParameters parameters = null);

        Task<T> GetNetworkAsync(string networkID);

        Task UpdateNetworkAsync(string networkID, UpdateNetworkRequest req);

        Task DeleteNetworkAsync(string networkID);
    }
}