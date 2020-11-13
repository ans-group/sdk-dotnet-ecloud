using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V1;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public class PodGPUProfileOperations<T> : ECloudOperations, IPodGPUProfileOperations<T> where T : GPUProfile
    {
        public PodGPUProfileOperations(IUKFastECloudClient client) : base(client)
        {
        }

        public async Task<IList<T>> GetPodGPUProfilesAsync(int podID, ClientRequestParameters parameters = null)
        {
            return await Client.GetAllAsync((ClientRequestParameters funcParameters) =>
               GetPodGPUProfilesPaginatedAsync(podID, funcParameters)
            , parameters);
        }

        public async Task<Paginated<T>> GetPodGPUProfilesPaginatedAsync(int podID, ClientRequestParameters parameters = null)
        {
            if (podID < 1)
            {
                throw new Client.Exception.UKFastClientValidationException("Invalid pod id");
            }

            return await this.Client.GetPaginatedAsync<T>($"/ecloud/v1/pods/{podID}/gpu-profiles", parameters);
        }
    }
}