using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V2.Request;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;
using Network = UKFast.API.Client.ECloud.Models.V2.Network;

namespace UKFast.API.Client.ECloud.Operations
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