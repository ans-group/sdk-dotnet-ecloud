using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public interface IDatastoreOperations<T> where T : Datastore
    {
        Task<IList<T>> GetDatastoresAsync(ClientRequestParameters parameters = null);
        Task<Paginated<T>> GetDatastoresPaginatedAsync(ClientRequestParameters parameters = null);
        Task<T> GetDatastoreAsync(int datastoreID);
    }
}
