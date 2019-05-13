using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public interface IApplianceParameterOperations<T> where T : ApplianceParameter
    {
        Task<IList<T>> GetApplianceParametersAsync(string applianceID, ClientRequestParameters parameters = null);
        Task<Paginated<T>> GetApplianceParametersPaginatedAsync(string applianceID, ClientRequestParameters parameters = null);
    }
}
