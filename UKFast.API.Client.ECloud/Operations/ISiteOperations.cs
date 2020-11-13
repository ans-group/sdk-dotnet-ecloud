using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V1;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public interface ISiteOperations<T> where T : Site
    {
        Task<IList<T>> GetSitesAsync(ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetSitesPaginatedAsync(ClientRequestParameters parameters = null);

        Task<T> GetSiteAsync(int siteID);
    }
}