using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public interface IPodApplianceOperations<T> where T : Appliance
    {
        Task<IList<T>> GetPodAppliancesAsync(int podID, ClientRequestParameters parameters = null);
        Task<Paginated<T>> GetPodAppliancesPaginatedAsync(int podID, ClientRequestParameters parameters = null);
    }
}
