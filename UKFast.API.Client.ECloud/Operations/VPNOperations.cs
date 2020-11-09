using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V2;
using UKFast.API.Client.ECloud.Models.V2.Request;
using UKFast.API.Client.Exception;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public class VPNOperations<T> : ECloudOperations, IVPNOperations<T> where T : VPN
    {
        public VPNOperations(IUKFastECloudClient client) : base(client)
        {
        }

        public async Task<string> CreateVPNAsync(CreateVPNRequest req)
        {
            return (await Client.PostAsync<VPN>("/ecloud/v2/vpns", req)).ID;
        }

        public async Task<IList<T>> GetVPNsAsync(ClientRequestParameters parameters = null)
        {
            return await Client.GetAllAsync(GetVPNsPaginatedAsync, parameters);
        }

        public async Task<Paginated<T>> GetVPNsPaginatedAsync(ClientRequestParameters parameters = null)
        {
            return await Client.GetPaginatedAsync<T>("/ecloud/v2/vpns", parameters);
        }

        public async Task<T> GetVPNAsync(string vpnID)
        {
            if (string.IsNullOrWhiteSpace(vpnID))
            {
                throw new UKFastClientValidationException("Invalid vpn id");
            }

            return await Client.GetAsync<T>($"/ecloud/v2/vpns/{vpnID}");
        }

        public async Task UpdateVPNAsync(string vpnID, UpdateVPNRequest req)
        {
            if (string.IsNullOrWhiteSpace(vpnID))
            {
                throw new UKFastClientValidationException("Invalid vpn id");
            }

            await Client.PatchAsync($"/ecloud/v2/vpns/{vpnID}", req);
        }

        public async Task DeleteVPNAsync(string vpnID)
        {
            if (string.IsNullOrWhiteSpace(vpnID))
            {
                throw new UKFastClientValidationException("Invalid vpn id");
            }

            await Client.DeleteAsync($"/ecloud/v2/vpns/{vpnID}");
        }
    }
}