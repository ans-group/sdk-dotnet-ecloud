using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V1;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public interface ISolutionDatastoreOperations<T> where T : Datastore
    {
        Task<IList<T>> GetSolutionDatastoresAsync(int solutionID, ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetSolutionDatastoresPaginatedAsync(int solutionID, ClientRequestParameters parameters = null);
    }
}