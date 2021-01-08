using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V2;
using UKFast.API.Client.ECloud.Models.V2.Request;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public interface IFirewallPolicyOperations<T> where T : FirewallPolicy
    {
        Task<string> CreateFirewallPolicyAsync(CreateFirewallPolicyRequest req);

        Task<IList<T>> GetFirewallPoliciesAsync(ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetFirewallPoliciesPaginatedAsync(ClientRequestParameters parameters = null);

        Task<T> GetFirewallPolicyAsync(string policyID);

        Task UpdateFirewallPolicyAsync(string policyID, UpdateFirewallPolicyRequest req);

        Task DeleteFirewallPolicyAsync(string policyID);
    }
}