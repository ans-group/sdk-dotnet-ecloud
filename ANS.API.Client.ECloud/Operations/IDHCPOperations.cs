using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V2;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public interface IDHCPOperations<T> where T : DHCP
    {
        Task<IList<T>> GetDHCPsAsync(ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetDHCPsPaginatedAsync(ClientRequestParameters parameters = null);

        Task<T> GetDHCPAsync(string dhcpID);
    }
}