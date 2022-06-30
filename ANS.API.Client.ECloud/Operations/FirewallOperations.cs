using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V1;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public class FirewallOperations<T> : ECloudOperations, IFirewallOperations<T> where T : Firewall
    {
        public FirewallOperations(IANSECloudClient client) : base(client)
        {
        }

        public async Task<IList<T>> GetFirewallsAsync(ClientRequestParameters parameters = null)
        {
            return await Client.GetAllAsync(GetFirewallsPaginatedAsync, parameters);
        }

        public async Task<Paginated<T>> GetFirewallsPaginatedAsync(ClientRequestParameters parameters = null)
        {
            return await this.Client.GetPaginatedAsync<T>("/ecloud/v1/firewalls", parameters);
        }

        public async Task<T> GetFirewallAsync(int firewallID)
        {
            if (firewallID < 1)
            {
                throw new Client.Exception.ANSClientValidationException("Invalid firewall id");
            }

            return await this.Client.GetAsync<T>($"/ecloud/v1/firewalls/{firewallID}");
        }

        public async Task<FirewallConfig> GetFirewallConfigAsync(int firewallID)
        {
            if (firewallID < 1)
            {
                throw new Client.Exception.ANSClientValidationException("Invalid firewall id");
            }

            return await this.Client.GetAsync<FirewallConfig>($"/ecloud/v1/firewalls/{firewallID}/config");
        }
    }
}