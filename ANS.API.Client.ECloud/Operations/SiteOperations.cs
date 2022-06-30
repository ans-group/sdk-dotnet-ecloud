using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V1;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public class SiteOperations<T> : ECloudOperations, ISiteOperations<T> where T : Site
    {
        public SiteOperations(IANSECloudClient client) : base(client)
        {
        }

        public async Task<IList<T>> GetSitesAsync(ClientRequestParameters parameters = null)
        {
            return await Client.GetAllAsync(GetSitesPaginatedAsync, parameters);
        }

        public async Task<Paginated<T>> GetSitesPaginatedAsync(ClientRequestParameters parameters = null)
        {
            return await this.Client.GetPaginatedAsync<T>("/ecloud/v1/sites", parameters);
        }

        public async Task<T> GetSiteAsync(int siteID)
        {
            if (siteID < 1)
            {
                throw new Client.Exception.ANSClientValidationException("Invalid site id");
            }

            return await this.Client.GetAsync<T>($"/ecloud/v1/sites/{siteID}");
        }
    }
}