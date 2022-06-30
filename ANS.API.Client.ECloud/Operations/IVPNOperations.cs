using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V2;
using ANS.API.Client.ECloud.Models.V2.Request;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
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