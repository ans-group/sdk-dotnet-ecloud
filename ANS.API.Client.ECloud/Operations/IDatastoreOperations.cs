using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V1;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public interface IDatastoreOperations<T> where T : Datastore
    {
        Task<IList<T>> GetDatastoresAsync(ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetDatastoresPaginatedAsync(ClientRequestParameters parameters = null);

        Task<T> GetDatastoreAsync(int datastoreID);
    }
}