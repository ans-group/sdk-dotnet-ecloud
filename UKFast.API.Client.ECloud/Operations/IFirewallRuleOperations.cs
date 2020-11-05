using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V2;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public interface IFirewallRuleOperations<T> where T : FirewallRule
    {
        Task<IList<T>> GetFirewallRulesAsync(ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetFirewallRulesPaginatedAsync(ClientRequestParameters parameters = null);

        Task<T> GetFirewallRuleAsync(string firewallRuleID);
    }
}