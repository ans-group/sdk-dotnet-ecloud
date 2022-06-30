using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V1;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public class PodApplianceOperations<T> : ECloudOperations, IPodApplianceOperations<T> where T : Appliance
    {
        public PodApplianceOperations(IANSECloudClient client) : base(client)
        {
        }

        public async Task<IList<T>> GetPodAppliancesAsync(int podID, ClientRequestParameters parameters = null)
        {
            return await Client.GetAllAsync((ClientRequestParameters funcParameters) =>
               GetPodAppliancesPaginatedAsync(podID, funcParameters)
            , parameters);
        }

        public async Task<Paginated<T>> GetPodAppliancesPaginatedAsync(int podID, ClientRequestParameters parameters = null)
        {
            if (podID < 1)
            {
                throw new Client.Exception.ANSClientValidationException("Invalid pod id");
            }

            return await this.Client.GetPaginatedAsync<T>($"/ecloud/v1/pods/{podID}/appliances", parameters);
        }
    }
}