using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V2;
using UKFast.API.Client.ECloud.Models.V2.Request;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public interface IInstanceOperations<T> where T : Instance
    {
        Task<string> CreateInstanceAsync(CreateInstanceRequest req);

        Task<IList<T>> GetInstancesAsync(ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetInstancesPaginatedAsync(ClientRequestParameters parameters = null);

        Task<T> GetInstanceAsync(string instanceID);

        Task UpdateInstanceAsync(string instanceID, UpdateInstanceRequest req);

        Task DeleteInstanceAsync(string instanceID);

        Task LockInstanceAsync(string instanceID);

        Task UnlockInstanceAsync(string instanceID);

        Task PowerOnInstanceAsync(string instanceID);

        Task PowerOffInstanceAsync(string instanceID);

        Task PowerResetInstanceAsync(string instanceID);

        Task PowerShutdownInstanceAsync(string instanceID);

        Task PowerRestartInstanceAsync(string instanceID);

        Task<IList<Volume>> GetInstanceVolumesAsync(string instanceID, ClientRequestParameters parameters = null);

        Task<Paginated<Volume>> GetInstanceVolumesPaginatedAsync(string instanceID, ClientRequestParameters parameters = null);

        Task<IList<Credential>> GetInstanceCredentialsAsync(string instanceID, ClientRequestParameters parameters = null);

        Task<Paginated<Credential>> GetInstanceCredentialsPaginatedAsync(string instanceID, ClientRequestParameters parameters = null);

        Task<IList<NIC>> GetInstanceNICsAsync(string instanceID, ClientRequestParameters parameters = null);

        Task<Paginated<NIC>> GetInstanceNICsPaginatedAsync(string instanceID, ClientRequestParameters parameters = null);
    }
}