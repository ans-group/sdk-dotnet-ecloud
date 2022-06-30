using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V2;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public interface IFirewallRuleOperations<T> where T : FirewallRule
    {
        Task<IList<T>> GetFirewallRulesAsync(ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetFirewallRulesPaginatedAsync(ClientRequestParameters parameters = null);

        Task<T> GetFirewallRuleAsync(string firewallRuleID);
    }
}