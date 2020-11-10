using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V2;
using UKFast.API.Client.ECloud.Models.V2.Request;
using UKFast.API.Client.Exception;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public class LoadBalancerOperations<T> : ECloudOperations, ILoadBalancerOperations<T> where T : LoadBalancerCluster
    {
        public LoadBalancerOperations(IUKFastECloudClient client) : base(client)
        {
        }

        public async Task<string> CreateLoadBalancerClusterAsync(CreateLoadBalancerClusterRequest req)
        {
            return (await Client.PostAsync<LoadBalancerCluster>("/ecloud/v2/lbcs", req)).ID;
        }

        public async Task<IList<T>> GetLoadBalancerClustersAsync(ClientRequestParameters parameters = null)
        {
            return await Client.GetAllAsync(GetLoadBalancerClustersPaginatedAsync, parameters);
        }

        public async Task<Paginated<T>> GetLoadBalancerClustersPaginatedAsync(ClientRequestParameters parameters = null)
        {
            return await Client.GetPaginatedAsync<T>("/ecloud/v2/lbcs", parameters);
        }

        public async Task<T> GetLoadBalancerClusterAsync(string lbcID)
        {
            if (string.IsNullOrWhiteSpace(lbcID))
            {
                throw new UKFastClientValidationException("Invalid lbc id");
            }

            return await Client.GetAsync<T>($"/ecloud/v2/lbcs/{lbcID}");
        }

        public async Task UpdateLoadBalancerClusterAsync(string lbcID, UpdateLoadBalancerClusterRequest req)
        {
            if (string.IsNullOrWhiteSpace(lbcID))
            {
                throw new UKFastClientValidationException("Invalid lbc id");
            }

            await Client.PatchAsync($"/ecloud/v2/lbcs/{lbcID}", req);
        }

        public async Task DeleteLoadBalancerClusterAsync(string lbcID)
        {
            if (string.IsNullOrWhiteSpace(lbcID))
            {
                throw new UKFastClientValidationException("Invalid lbc id");
            }

            await Client.DeleteAsync($"/ecloud/v2/lbcs/{lbcID}");
        }
    }
}