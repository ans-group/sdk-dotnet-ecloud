using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V2;
using UKFast.API.Client.ECloud.Models.V2.Request;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public interface ILoadBalancerOperations<T> where T : LoadBalancerCluster
    {
        Task<string> CreateLoadBalancerClusterAsync(CreateLoadBalancerClusterRequest req);

        Task<IList<T>> GetLoadBalancerClustersAsync(ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetLoadBalancerClustersPaginatedAsync(ClientRequestParameters parameters = null);

        Task<T> GetLoadBalancerClusterAsync(string lbcID);

        Task UpdateLoadBalancerClusterAsync(string lbcID, UpdateLoadBalancerClusterRequest req);

        Task DeleteLoadBalancerClusterAsync(string lbcID);
    }
}