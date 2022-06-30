using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V2;
using ANS.API.Client.ECloud.Models.V2.Request;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
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