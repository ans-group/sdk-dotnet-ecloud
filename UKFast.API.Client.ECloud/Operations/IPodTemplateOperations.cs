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
    public interface IPodTemplateOperations<T> where T : Template
    {
        Task<IList<T>> GetPodTemplatesAsync(int podID, ClientRequestParameters parameters = null);
        Task<Paginated<T>> GetPodTemplatesPaginatedAsync(int podID, ClientRequestParameters parameters = null);
        Task<T> GetPodTemplateAsync(int podID, string templateName);
        Task DeletePodTemplateAsync(int podID, string templateName);
        Task RenamePodTemplateAsync(int podID, string templateName, RenameTemplateRequest req);
    }
}
