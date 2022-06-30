using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V1;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public class SolutionHostOperations<T> : ECloudOperations, ISolutionHostOperations<T> where T : Host
    {
        public SolutionHostOperations(IANSECloudClient client) : base(client)
        {
        }

        public async Task<IList<T>> GetSolutionHostsAsync(int solutionID, ClientRequestParameters parameters = null)
        {
            return await this.Client.GetAllAsync((ClientRequestParameters funcParameters) => GetSolutionHostsPaginatedAsync(solutionID, funcParameters), parameters);
        }

        public async Task<Paginated<T>> GetSolutionHostsPaginatedAsync(int solutionID, ClientRequestParameters parameters = null)
        {
            if (solutionID < 1)
            {
                throw new Client.Exception.ANSClientValidationException("Invalid solution id");
            }

            return await this.Client.GetPaginatedAsync<T>($"/ecloud/v1/solutions/{solutionID}/hosts", parameters);
        }
    }
}