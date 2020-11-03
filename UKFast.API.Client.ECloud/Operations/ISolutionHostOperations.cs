using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V1;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public interface ISolutionHostOperations<T> where T : Host
    {
        Task<IList<T>> GetSolutionHostsAsync(int solutionID, ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetSolutionHostsPaginatedAsync(int solutionID, ClientRequestParameters parameters = null);
    }
}