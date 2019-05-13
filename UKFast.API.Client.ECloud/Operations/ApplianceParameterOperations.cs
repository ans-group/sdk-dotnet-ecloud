using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public class ApplianceParameterOperations<T> : ECloudOperations, IApplianceParameterOperations<T> where T : ApplianceParameter
    {
        public ApplianceParameterOperations(IUKFastECloudClient client) : base(client) { }

        public async Task<IList<T>> GetApplianceParametersAsync(string applianceID, ClientRequestParameters parameters = null)
        {
            return await Client.GetAllAsync((ClientRequestParameters funcParameters) =>
                GetApplianceParametersPaginatedAsync(applianceID, funcParameters)
            , parameters);
        }

        public async Task<Paginated<T>> GetApplianceParametersPaginatedAsync(string applianceID, ClientRequestParameters parameters = null)
        {
            if (string.IsNullOrWhiteSpace(applianceID))
            {
                throw new Client.Exception.UKFastClientValidationException("Invalid appliance id");
            }

            return await this.Client.GetPaginatedAsync<T>($"/ecloud/v1/appliances/{applianceID}/parameters", parameters);
        }
    }
}
