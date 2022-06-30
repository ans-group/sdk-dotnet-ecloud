using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V2;
using ANS.API.Client.ECloud.Models.V2.Request;
using ANS.API.Client.Exception;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public class RouterOperations<T> : ECloudOperations, IRouterOperations<T> where T : Router
    {
        public RouterOperations(IANSECloudClient client) : base(client)
        {
        }

        public async Task<string> CreateRouterAsync(CreateRouterRequest req)
        {
            return (await Client.PostAsync<Router>("/ecloud/v2/routers", req)).ID;
        }

        public async Task<IList<T>> GetRoutersAsync(ClientRequestParameters parameters = null)
        {
            return await Client.GetAllAsync(GetRoutersPaginatedAsync, parameters);
        }

        public async Task<Paginated<T>> GetRoutersPaginatedAsync(ClientRequestParameters parameters = null)
        {
            return await Client.GetPaginatedAsync<T>("/ecloud/v2/routers", parameters);
        }

        public async Task<T> GetRouterAsync(string routerID)
        {
            if (string.IsNullOrWhiteSpace(routerID))
            {
                throw new ANSClientValidationException("Invalid router id");
            }

            return await Client.GetAsync<T>($"/ecloud/v2/routers/{routerID}");
        }

        public async Task UpdateRouterAsync(string routerID, UpdateRouterRequest req)
        {
            if (string.IsNullOrWhiteSpace(routerID))
            {
                throw new ANSClientValidationException("Invalid router id");
            }

            await Client.PatchAsync($"/ecloud/v2/routers/{routerID}", req);
        }

        public async Task DeleteRouterAsync(string routerID)
        {
            if (string.IsNullOrWhiteSpace(routerID))
            {
                throw new ANSClientValidationException("Invalid router id");
            }

            await Client.DeleteAsync($"/ecloud/v2/routers/{routerID}");
        }
    }
}