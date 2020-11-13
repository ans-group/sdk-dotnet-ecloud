using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V2;
using UKFast.API.Client.ECloud.Models.V2.Request;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public interface IRouterOperations<T> where T : Router
    {
        Task<string> CreateRouterAsync(CreateRouterRequest req);

        Task<IList<T>> GetRoutersAsync(ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetRoutersPaginatedAsync(ClientRequestParameters parameters = null);

        Task<T> GetRouterAsync(string routerID);

        Task UpdateRouterAsync(string routerID, UpdateRouterRequest req);

        Task DeleteRouterAsync(string routerID);
    }
}