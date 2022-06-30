using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V1;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public interface ISolutionOperations<T> where T : Solution
    {
        Task<IList<T>> GetSolutionsAsync(ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetSolutionsPaginatedAsync(ClientRequestParameters parameters = null);

        Task<T> GetSolutionAsync(int solutionID);
    }
}