using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V1;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public interface ISolutionSiteOperations<T> where T : Site
    {
        Task<IList<T>> GetSolutionSitesAsync(int solutionID, ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetSolutionSitesPaginatedAsync(int solutionID, ClientRequestParameters parameters = null);
    }
}