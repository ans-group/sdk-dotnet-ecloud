using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V2;
using ANS.API.Client.Exception;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public class FirewallRuleOperations<T> : ECloudOperations, IFirewallRuleOperations<T> where T : FirewallRule
    {
        public FirewallRuleOperations(IANSECloudClient client) : base(client)
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
                throw new ANSClientValidationException("Invalid firewall rule id");
            }

            return await Client.GetAsync<T>($"/ecloud/v2/firewall-rules/{firewallRuleID}");
        }
    }
}