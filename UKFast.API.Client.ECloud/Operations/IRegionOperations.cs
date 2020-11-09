using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V2;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public interface IRegionOperations<T> where T : Region
    {
        Task<IList<T>> GetRegionsAsync(ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetRegionsPaginatedAsync(ClientRequestParameters parameters = null);

        Task<T> GetRegionAsync(string regionID);
    }
}