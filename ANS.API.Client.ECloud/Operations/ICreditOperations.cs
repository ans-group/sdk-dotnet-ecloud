using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V1;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public interface ICreditOperations<T> where T : Credit
    {
        Task<IList<T>> GetCreditsAsync(ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetCreditsPaginatedAsync(ClientRequestParameters parameters = null);
    }
}