using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V2;
using ANS.API.Client.ECloud.Models.V2.Request;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
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