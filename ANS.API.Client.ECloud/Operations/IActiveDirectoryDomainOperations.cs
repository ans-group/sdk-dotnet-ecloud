using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V1;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public interface IActiveDirectoryDomainOperations<T> where T : ActiveDirectoryDomain
    {
        Task<IList<T>> GetActiveDirectoryDomainsAsync(ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetActiveDirectoryDomainsPaginatedAsync(ClientRequestParameters parameters = null);

        Task<T> GetActiveDirectoryDomainAsync(int domainID);
    }
}