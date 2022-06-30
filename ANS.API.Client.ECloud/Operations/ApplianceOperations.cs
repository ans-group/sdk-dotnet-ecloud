using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V1;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public class ApplianceOperations<T> : ECloudOperations, IApplianceOperations<T> where T : Appliance
    {
        public ApplianceOperations(IANSECloudClient client) : base(client)
        {
        }

        public async Task<IList<T>> GetAppliancesAsync(ClientRequestParameters parameters = null)
        {
            return await Client.GetAllAsync(GetAppliancesPaginatedAsync, parameters);
        }

        public async Task<Paginated<T>> GetAppliancesPaginatedAsync(ClientRequestParameters parameters = null)
        {
            return await this.Client.GetPaginatedAsync<T>("/ecloud/v1/appliances", parameters);
        }

        public async Task<T> GetApplianceAsync(string applianceID)
        {
            if (string.IsNullOrWhiteSpace(applianceID))
            {
                throw new Client.Exception.ANSClientValidationException("Invalid appliance id");
            }

            return await this.Client.GetAsync<T>($"/ecloud/v1/appliances/{applianceID}");
        }
    }
}