using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public interface IApplianceOperations<T> where T : Appliance
    {
        Task<IList<T>> GetAppliancesAsync(ClientRequestParameters parameters = null);
        Task<Paginated<T>> GetAppliancesPaginatedAsync(ClientRequestParameters parameters = null);
        Task<T> GetApplianceAsync(string applianceID);
    }
}
