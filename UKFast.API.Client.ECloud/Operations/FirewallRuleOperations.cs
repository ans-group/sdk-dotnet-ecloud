using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V2;
using UKFast.API.Client.Exception;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public class FirewallRuleOperations<T> : ECloudOperations, IFirewallRuleOperations<T> where T : FirewallRule
    {
        public FirewallRuleOperations(IUKFastECloudClient client) : base(client)
        {
        }

        public async Task<IList<T>> GetFirewallRulesAsync(ClientRequestParameters parameters = null)
        {
            return await Client.GetAllAsync(GetFirewallRulesPaginatedAsync, parameters);
        }

        public async Task<Paginated<T>> GetFirewallRulesPaginatedAsync(ClientRequestParameters parameters = null)
        {
            return await Client.GetPaginatedAsync<T>("/ecloud/v2/firewall-rules", parameters);
        }

        public async Task<T> GetFirewallRuleAsync(string firewallRuleID)
        {
            if (string.IsNullOrWhiteSpace(firewallRuleID))
            {
                throw new UKFastClientValidationException("Invalid firewall rule id");
            }

            return await Client.GetAsync<T>($"/ecloud/v2/firewall-rules/{firewallRuleID}");
        }
    }
}