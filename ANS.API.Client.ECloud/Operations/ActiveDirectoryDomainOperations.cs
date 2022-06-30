using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V1;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public class ActiveDirectoryDomainOperations<T> : ECloudOperations, IActiveDirectoryDomainOperations<T> where T : ActiveDirectoryDomain
    {
        public ActiveDirectoryDomainOperations(IANSECloudClient client) : base(client)
        {
        }

        public async Task<IList<T>> GetActiveDirectoryDomainsAsync(ClientRequestParameters parameters = null)
        {
            return await Client.GetAllAsync(GetActiveDirectoryDomainsPaginatedAsync, parameters);
        }

        public async Task<Paginated<T>> GetActiveDirectoryDomainsPaginatedAsync(ClientRequestParameters parameters = null)
        {
            return await this.Client.GetPaginatedAsync<T>("/ecloud/v1/active-directory/domains", parameters);
        }

        public async Task<T> GetActiveDirectoryDomainAsync(int domainID)
        {
            if (domainID < 1)
            {
                throw new Client.Exception.ANSClientValidationException("Invalid domain id");
            }

            return await this.Client.GetAsync<T>($"/ecloud/v1/active-directory/domains/{domainID}");
        }
    }
}