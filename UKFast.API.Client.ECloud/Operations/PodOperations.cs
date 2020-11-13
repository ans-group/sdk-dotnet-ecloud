using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V1;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public class PodOperations<T> : ECloudOperations, IPodOperations<T> where T : Pod
    {
        public PodOperations(IUKFastECloudClient client) : base(client)
        {
        }

        public async Task<IList<T>> GetPodsAsync(ClientRequestParameters parameters = null)
        {
            return await Client.GetAllAsync(GetPodsPaginatedAsync, parameters);
        }

        public async Task<Paginated<T>> GetPodsPaginatedAsync(ClientRequestParameters parameters = null)
        {
            return await this.Client.GetPaginatedAsync<T>("/ecloud/v1/pods", parameters);
        }

        public async Task<T> GetPodAsync(int podID)
        {
            if (podID < 1)
            {
                throw new Client.Exception.UKFastClientValidationException("Invalid pod id");
            }

            return await this.Client.GetAsync<T>($"/ecloud/v1/pods/{podID}");
        }
    }
}