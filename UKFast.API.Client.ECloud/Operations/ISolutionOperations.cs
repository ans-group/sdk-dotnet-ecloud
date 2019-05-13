using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public interface ISolutionOperations<T> where T : Solution
    {
        Task<IList<T>> GetSolutionsAsync(ClientRequestParameters parameters = null);
        Task<Paginated<T>> GetSolutionsPaginatedAsync(ClientRequestParameters parameters = null);
        Task<T> GetSolutionAsync(int solutionID);
    }
}
