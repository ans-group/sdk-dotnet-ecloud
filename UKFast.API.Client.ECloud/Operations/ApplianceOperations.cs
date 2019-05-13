using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public class ApplianceOperations<T> : ECloudOperations, IApplianceOperations<T> where T : Appliance
    {
        public ApplianceOperations(IUKFastECloudClient client) : base(client)
        {
            this._resource = this._resource + "/v1/appliances";
        }

        public async Task<IList<T>> GetAppliancesAsync(ClientRequestParameters parameters = null)
        {
            return await Client.GetAllAsync(GetAppliancesPaginatedAsync, parameters);
        }

        public async Task<Paginated<T>> GetAppliancesPaginatedAsync(ClientRequestParameters parameters = null)
        {
            return await this.Client.GetPaginatedAsync<T>(_resource, parameters);
        }

        public async Task<T> GetApplianceAsync(string applianceID)
        {
            if (string.IsNullOrWhiteSpace(applianceID))
            {
                throw new Client.Exception.UKFastClientValidationException("Invalid appliance id");
            }

            return await this.Client.GetAsync<T>($"{_resource }/{applianceID}");
        }
    }
}
