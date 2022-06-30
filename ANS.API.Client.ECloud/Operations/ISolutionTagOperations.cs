using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V1;
using ANS.API.Client.ECloud.Models.V1.Request;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public interface ISolutionTagOperations<T> where T : Tag
    {
        Task<string> CreateSolutionTagAsync(int solutionID, CreateTagRequest req);

        Task<IList<T>> GetSolutionTagsAsync(int solutionID, ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetSolutionTagsPaginatedAsync(int solutionID, ClientRequestParameters parameters = null);

        Task<T> GetSolutionTagAsync(int solutionID, string tagKey);

        Task UpdateSolutionTagAsync(int solutionID, string tagKey, UpdateTagRequest req);

        Task DeleteSolutionTagAsync(int solutionID, string tagKey);
    }
}