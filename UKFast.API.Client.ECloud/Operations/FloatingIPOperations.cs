using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V2;
using UKFast.API.Client.Exception;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public class FloatingIPOperations<T> : ECloudOperations, IFloatingOperations<T> where T : FloatingIP
    {
        public FloatingIPOperations(IUKFastECloudClient client) : base(client)
        {
        }

        public async Task<IList<T>> GetFloatingIPsAsync(ClientRequestParameters parameters = null)
        {
            return await Client.GetAllAsync(GetFloatingIPsPaginatedAsync, parameters);
        }

        public async Task<Paginated<T>> GetFloatingIPsPaginatedAsync(ClientRequestParameters parameters = null)
        {
            return await Client.GetPaginatedAsync<T>("/ecloud/v2/floating-ips", parameters);
        }

        public async Task<T> GetFloatingIPAsync(string floatingIPID)
        {
            if (string.IsNullOrWhiteSpace(floatingIPID))
            {
                throw new UKFastClientValidationException("Invalid floating ip");
            }
            return await Client.GetAsync<T>($"/ecloud/v2/floating-ips/{floatingIPID}");
        }
    }
}