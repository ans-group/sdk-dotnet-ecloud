using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V1;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public class ApplianceParameterOperations<T> : ECloudOperations, IApplianceParameterOperations<T> where T : ApplianceParameter
    {
        public ApplianceParameterOperations(IANSECloudClient client) : base(client)
        {
        }

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
                throw new Client.Exception.ANSClientValidationException("Invalid appliance id");
            }

            return await this.Client.GetPaginatedAsync<T>($"/ecloud/v1/appliances/{applianceID}/parameters", parameters);
        }
    }
}