using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V2;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public interface IRegionOperations<T> where T : Region
    {
        Task<IList<T>> GetRegionsAsync(ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetRegionsPaginatedAsync(ClientRequestParameters parameters = null);

        Task<T> GetRegionAsync(string regionID);
    }
}