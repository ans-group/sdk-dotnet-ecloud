using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V2;
using ANS.API.Client.ECloud.Models.V2.Request;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
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