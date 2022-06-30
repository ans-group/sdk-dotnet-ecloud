using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V1;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public interface IApplianceOperations<T> where T : Appliance
    {
        Task<IList<T>> GetAppliancesAsync(ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetAppliancesPaginatedAsync(ClientRequestParameters parameters = null);

        Task<T> GetApplianceAsync(string applianceID);
    }
}