using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V1;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public interface ISolutionNetworkOperations<T> where T : NetworkV1
    {
        Task<IList<T>> GetSolutionNetworksAsync(int solutionID, ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetSolutionNetworksPaginatedAsync(int solutionID, ClientRequestParameters parameters = null);
    }
}