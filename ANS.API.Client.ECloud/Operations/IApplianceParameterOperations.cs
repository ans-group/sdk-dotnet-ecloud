using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V1;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public interface IApplianceParameterOperations<T> where T : ApplianceParameter
    {
        Task<IList<T>> GetApplianceParametersAsync(string applianceID, ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetApplianceParametersPaginatedAsync(string applianceID, ClientRequestParameters parameters = null);
    }
}