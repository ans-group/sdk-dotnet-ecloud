using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V2;
using ANS.API.Client.Exception;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public class RegionOperations<T> : ECloudOperations, IRegionOperations<T> where T : Region
    {
        public RegionOperations(IANSECloudClient client) : base(client)
        {
        }

        public async Task<IList<T>> GetRegionsAsync(ClientRequestParameters parameters = null)
        {
            return await Client.GetAllAsync(GetRegionsPaginatedAsync, parameters);
        }

        public async Task<Paginated<T>> GetRegionsPaginatedAsync(ClientRequestParameters parameters = null)
        {
            return await Client.GetPaginatedAsync<T>("/ecloud/v2/regions", parameters);
        }

        public async Task<T> GetRegionAsync(string regionID)
        {
            if (string.IsNullOrWhiteSpace(regionID))
            {
                throw new ANSClientValidationException("Invalid region id");
            }

            return await Client.GetAsync<T>($"/ecloud/v2/regions/{regionID}");
        }
    }
}