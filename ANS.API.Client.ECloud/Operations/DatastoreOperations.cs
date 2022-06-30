using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V1;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public class DatastoreOperations<T> : ECloudOperations, IDatastoreOperations<T> where T : Datastore
    {
        public DatastoreOperations(IANSECloudClient client) : base(client)
        {
        }

        public async Task<IList<T>> GetDatastoresAsync(ClientRequestParameters parameters = null)
        {
            return await Client.GetAllAsync(GetDatastoresPaginatedAsync, parameters);
        }

        public async Task<Paginated<T>> GetDatastoresPaginatedAsync(ClientRequestParameters parameters = null)
        {
            return await this.Client.GetPaginatedAsync<T>("/ecloud/v1/datastores", parameters);
        }

        public async Task<T> GetDatastoreAsync(int datastoreID)
        {
            if (datastoreID < 1)
            {
                throw new Client.Exception.ANSClientValidationException("Invalid datastore id");
            }

            return await this.Client.GetAsync<T>($"/ecloud/v1/datastores/{datastoreID}");
        }
    }
}