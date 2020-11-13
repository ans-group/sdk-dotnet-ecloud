using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V2;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public interface IFloatingOperations<T> where T : FloatingIP
    {
        Task<IList<T>> GetFloatingIPsAsync(ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetFloatingIPsPaginatedAsync(ClientRequestParameters parameters = null);

        Task<T> GetFloatingIPAsync(string floatingIPID);
    }
}