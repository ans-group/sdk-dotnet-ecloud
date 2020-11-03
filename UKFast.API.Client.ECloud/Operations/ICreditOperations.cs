using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V1;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public interface ICreditOperations<T> where T : Credit
    {
        Task<IList<T>> GetCreditsAsync(ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetCreditsPaginatedAsync(ClientRequestParameters parameters = null);
    }
}