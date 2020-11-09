using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V2;
using UKFast.API.Client.ECloud.Models.V2.Request;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public interface IVPNOperations<T> where T : VPN
    {
        Task<string> CreateVPNAsync(CreateVPNRequest req);

        Task<IList<T>> GetVPNsAsync(ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetVPNsPaginatedAsync(ClientRequestParameters parameters = null);

        Task<T> GetVPNAsync(string vpnID);

        Task UpdateVPNAsync(string vpnID, UpdateVPNRequest req);

        Task DeleteVPNAsync(string vpnID);
    }
}