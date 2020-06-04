using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public interface IActiveDirectoryDomainOperations<T> where T : ActiveDirectoryDomain
    {
        Task<IList<T>> GetActiveDirectoryDomainsAsync(ClientRequestParameters parameters = null);
        Task<Paginated<T>> GetActiveDirectoryDomainsPaginatedAsync(ClientRequestParameters parameters = null);
        Task<T> GetActiveDirectoryDomainAsync(int domainID);
    }
}
