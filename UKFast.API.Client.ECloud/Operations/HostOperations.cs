using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V1;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public class HostOperations<T> : ECloudOperations, IHostOperations<T> where T : Host
    {
        public HostOperations(IUKFastECloudClient client) : base(client)
        {
        }

        public async Task<IList<T>> GetHostsAsync(ClientRequestParameters parameters = null)
        {
            return await Client.GetAllAsync(GetHostsPaginatedAsync, parameters);
        }

        public async Task<Paginated<T>> GetHostsPaginatedAsync(ClientRequestParameters parameters = null)
        {
            return await this.Client.GetPaginatedAsync<T>("/ecloud/v1/hosts", parameters);
        }

        public async Task<T> GetHostAsync(int hostID)
        {
            if (hostID < 1)
            {
                throw new Client.Exception.UKFastClientValidationException("Invalid host id");
            }

            return await this.Client.GetAsync<T>($"/ecloud/v1/hosts/{hostID}");
        }
    }
}