using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V2;
using UKFast.API.Client.Exception;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public class DHCPOperations<T> : ECloudOperations, IDHCPOperations<T> where T : DHCP
    {
        public DHCPOperations(IUKFastECloudClient client) : base(client)
        {
        }

        public async Task<IList<T>> GetDHCPsAsync(ClientRequestParameters parameters = null)
        {
            return await Client.GetAllAsync(GetDHCPsPaginatedAsync, parameters);
        }

        public async Task<Paginated<T>> GetDHCPsPaginatedAsync(ClientRequestParameters parameters = null)
        {
            return await Client.GetPaginatedAsync<T>("/ecloud/v2/dhcps", parameters);
        }

        public async Task<T> GetDHCPAsync(string dhcpID)
        {
            if (string.IsNullOrWhiteSpace(dhcpID))
            {
                throw new UKFastClientValidationException("Invalid dhcp id");
            }

            return await Client.GetAsync<T>($"/ecloud/v2/dhcps/{dhcpID}");
        }
    }
}