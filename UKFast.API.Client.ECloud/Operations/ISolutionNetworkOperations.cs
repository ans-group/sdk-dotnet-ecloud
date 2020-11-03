using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V1;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public interface ISolutionNetworkOperations<T> where T : Network
    {
        Task<IList<T>> GetSolutionNetworksAsync(int solutionID, ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetSolutionNetworksPaginatedAsync(int solutionID, ClientRequestParameters parameters = null);
    }
}