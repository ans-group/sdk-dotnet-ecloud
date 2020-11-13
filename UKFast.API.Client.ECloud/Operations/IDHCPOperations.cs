using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V2;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public interface IDHCPOperations<T> where T : DHCP
    {
        Task<IList<T>> GetDHCPsAsync(ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetDHCPsPaginatedAsync(ClientRequestParameters parameters = null);

        Task<T> GetDHCPAsync(string dhcpID);
    }
}