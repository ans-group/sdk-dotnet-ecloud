using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V2;
using UKFast.API.Client.ECloud.Models.V2.Request;
using UKFast.API.Client.Exception;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public class RouterOperations<T> : ECloudOperations, IRouterOperations<T> where T : Router
    {
        public RouterOperations(IUKFastECloudClient client) : base(client)
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
                throw new UKFastClientValidationException("Invalid router id");
            }

            return await Client.GetAsync<T>($"/ecloud/v2/routers/{routerID}");
        }

        public async Task UpdateRouterAsync(string routerID, UpdateRouterRequest req)
        {
            if (string.IsNullOrWhiteSpace(routerID))
            {
                throw new UKFastClientValidationException("Invalid router id");
            }

            await Client.PatchAsync($"/ecloud/v2/routers/{routerID}", req);
        }

        public async Task DeleteRouterAsync(string routerID)
        {
            if (string.IsNullOrWhiteSpace(routerID))
            {
                throw new UKFastClientValidationException("Invalid router id");
            }

            await Client.DeleteAsync($"/ecloud/v2/routers/{routerID}");
        }
    }
}