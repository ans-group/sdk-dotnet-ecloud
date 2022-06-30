using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V2;
using ANS.API.Client.ECloud.Models.V2.Request;
using ANS.API.Client.Exception;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public class VPCOperations<T> : ECloudOperations, IVPCOperations<T> where T : VPC
    {
        public VPCOperations(IANSECloudClient client) : base(client)
        {
        }

        public async Task<string> CreateVPCAsync(CreateVPCRequest req)
        {
            return (await Client.PostAsync<VPC>("/ecloud/v2/vpcs", req)).ID;
        }

        public async Task<IList<T>> GetVPCsAsync(ClientRequestParameters parameters = null)
        {
            return await Client.GetAllAsync(GetVPCsPaginatedAsync, parameters);
        }

        public async Task<Paginated<T>> GetVPCsPaginatedAsync(ClientRequestParameters parameters = null)
        {
            return await Client.GetPaginatedAsync<T>("/ecloud/v2/vpcs", parameters);
        }

        public async Task<T> GetVPCAsync(string vpcID)
        {
            if (string.IsNullOrWhiteSpace(vpcID))
            {
                throw new ANSClientValidationException("Invalid vpc id");
            }

            return await Client.GetAsync<T>($"/ecloud/v2/vpcs/{vpcID}");
        }

        public async Task UpdateVPCAsync(string vpcID, UpdateVPCRequest req)
        {
            if (string.IsNullOrWhiteSpace(vpcID))
            {
                throw new ANSClientValidationException("Invalid vpc id");
            }

            await Client.PatchAsync($"/ecloud/v2/vpcs/{vpcID}", req);
        }

        public async Task DeleteVPCAsync(string vpcID)
        {
            if (string.IsNullOrWhiteSpace(vpcID))
            {
                throw new ANSClientValidationException("Invalid vpc id");
            }

            await Client.DeleteAsync($"/ecloud/v2/vpcs/{vpcID}");
        }
    }
}