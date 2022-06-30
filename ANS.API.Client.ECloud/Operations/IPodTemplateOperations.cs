using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V1;
using ANS.API.Client.ECloud.Models.V1.Request;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public interface IPodTemplateOperations<T> where T : Template
    {
        Task<IList<T>> GetPodTemplatesAsync(int podID, ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetPodTemplatesPaginatedAsync(int podID, ClientRequestParameters parameters = null);

        Task<T> GetPodTemplateAsync(int podID, string templateName);

        Task DeletePodTemplateAsync(int podID, string templateName);

        Task RenamePodTemplateAsync(int podID, string templateName, RenameTemplateRequest req);
    }
}