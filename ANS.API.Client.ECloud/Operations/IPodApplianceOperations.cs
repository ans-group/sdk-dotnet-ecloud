using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V1;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public interface IPodApplianceOperations<T> where T : Appliance
    {
        Task<IList<T>> GetPodAppliancesAsync(int podID, ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetPodAppliancesPaginatedAsync(int podID, ClientRequestParameters parameters = null);
    }
}