using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V1;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public interface ISolutionDatastoreOperations<T> where T : Datastore
    {
        Task<IList<T>> GetSolutionDatastoresAsync(int solutionID, ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetSolutionDatastoresPaginatedAsync(int solutionID, ClientRequestParameters parameters = null);
    }
}