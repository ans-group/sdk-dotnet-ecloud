using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V1;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public interface IFirewallOperations<T> where T : Firewall
    {
        Task<IList<T>> GetFirewallsAsync(ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetFirewallsPaginatedAsync(ClientRequestParameters parameters = null);

        Task<T> GetFirewallAsync(int firewallID);
    }
}