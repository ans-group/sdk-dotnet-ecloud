using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V1;
using ANS.API.Client.ECloud.Models.V1.Request;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public interface ISolutionTemplateOperations<T> where T : Template
    {
        Task<IList<T>> GetSolutionTemplatesAsync(int solutionID, ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetSolutionTemplatesPaginatedAsync(int solutionID, ClientRequestParameters parameters = null);

        Task<T> GetSolutionTemplateAsync(int solutionID, string templateName);

        Task DeleteSolutionTemplateAsync(int solutionID, string templateName);

        Task RenameSolutionTemplateAsync(int solutionID, string templateName, RenameTemplateRequest req);
    }
}