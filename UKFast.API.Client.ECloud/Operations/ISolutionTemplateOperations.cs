using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models;
using UKFast.API.Client.ECloud.Models.Request;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
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
