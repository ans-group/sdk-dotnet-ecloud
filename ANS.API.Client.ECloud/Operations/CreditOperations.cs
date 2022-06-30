using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V1;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public class CreditOperations<T> : ECloudOperations, ICreditOperations<T> where T : Credit
    {
        public CreditOperations(IANSECloudClient client) : base(client)
        {
        }

        public async Task<IList<T>> GetCreditsAsync(ClientRequestParameters parameters = null)
        {
            return await Client.GetAllAsync(GetCreditsPaginatedAsync, parameters);
        }

        public async Task<Paginated<T>> GetCreditsPaginatedAsync(ClientRequestParameters parameters = null)
        {
            return await this.Client.GetPaginatedAsync<T>("/ecloud/v1/credits", parameters);
        }
    }
}