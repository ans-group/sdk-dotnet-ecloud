using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V2;
using UKFast.API.Client.ECloud.Models.V2.Request;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public interface IVPCOperations<T> where T : VPC
    {
        Task<string> CreateVPCAsync(CreateVPCRequest req);

        Task<IList<T>> GetVPCsAsync(ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetVPCsPaginatedAsync(ClientRequestParameters parameters = null);

        Task<T> GetVPCAsync(string vpcID);

        Task UpdateVPCAsync(string vpcID, UpdateVPCRequest req);

        Task DeleteVPCAsync(string vpcID);
    }
}