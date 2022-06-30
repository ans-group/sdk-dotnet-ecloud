using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V2;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public interface IFloatingOperations<T> where T : FloatingIP
    {
        Task<IList<T>> GetFloatingIPsAsync(ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetFloatingIPsPaginatedAsync(ClientRequestParameters parameters = null);

        Task<T> GetFloatingIPAsync(string floatingIPID);
    }
}