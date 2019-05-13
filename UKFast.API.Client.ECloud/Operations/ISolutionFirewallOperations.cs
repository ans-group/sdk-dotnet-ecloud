using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public interface ISolutionFirewallOperations<T> where T : Firewall
    {
        Task<IList<T>> GetSolutionFirewallsAsync(int solutionID, ClientRequestParameters parameters = null);
        Task<Paginated<T>> GetSolutionFirewallsPaginatedAsync(int solutionID, ClientRequestParameters parameters = null);
    }
}
