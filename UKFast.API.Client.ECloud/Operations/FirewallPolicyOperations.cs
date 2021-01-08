using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V2;
using UKFast.API.Client.ECloud.Models.V2.Request;
using UKFast.API.Client.Exception;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public class FirewallPolicyOperations<T> : ECloudOperations, IFirewallPolicyOperations<T> where T : FirewallPolicy
    {
        public FirewallPolicyOperations(IUKFastECloudClient client) : base(client)
        {
        }

        public async Task<string> CreateFirewallPolicyAsync(CreateFirewallPolicyRequest req)
        {
            return (await Client.PostAsync<FirewallPolicy>("/ecloud/v2/firewall-policies", req)).ID;
        }

        public async Task<IList<T>> GetFirewallPoliciesAsync(ClientRequestParameters parameters = null)
        {
            return await Client.GetAllAsync(GetFirewallPoliciesPaginatedAsync, parameters);
        }

        public async Task<Paginated<T>> GetFirewallPoliciesPaginatedAsync(ClientRequestParameters parameters = null)
        {
            return await Client.GetPaginatedAsync<T>("/ecloud/v2/firewall-policies", parameters);
        }

        public async Task<T> GetFirewallPolicyAsync(string policyID)
        {
            if (string.IsNullOrWhiteSpace(policyID))
            {
                throw new UKFastClientValidationException("Invalid firewall policy id");
            }

            return await Client.GetAsync<T>($"/ecloud/v2/firewall-policies/{policyID}");
        }

        public async Task UpdateFirewallPolicyAsync(string policyID, UpdateFirewallPolicyRequest req)
        {
            if (string.IsNullOrWhiteSpace(policyID))
            {
                throw new UKFastClientValidationException("Invalid firewall policy id");
            }

            await Client.PatchAsync($"/ecloud/v2/firewall-policies/{policyID}", req);
        }

        public async Task DeleteFirewallPolicyAsync(string policyID)
        {
            if (string.IsNullOrWhiteSpace(policyID))
            {
                throw new UKFastClientValidationException("Invalid firewall policy id");
            }

            await Client.DeleteAsync($"/ecloud/v2/firewall-policies/{policyID}");
        }
    }
}