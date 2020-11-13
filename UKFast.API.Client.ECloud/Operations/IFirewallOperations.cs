using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V1;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public interface IFirewallOperations<T> where T : Firewall
    {
        Task<IList<T>> GetFirewallsAsync(ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetFirewallsPaginatedAsync(ClientRequestParameters parameters = null);

        Task<T> GetFirewallAsync(int firewallID);
    }
}