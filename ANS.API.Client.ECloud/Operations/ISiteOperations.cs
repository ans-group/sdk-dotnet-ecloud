using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V1;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public interface ISiteOperations<T> where T : Site
    {
        Task<IList<T>> GetSitesAsync(ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetSitesPaginatedAsync(ClientRequestParameters parameters = null);

        Task<T> GetSiteAsync(int siteID);
    }
}