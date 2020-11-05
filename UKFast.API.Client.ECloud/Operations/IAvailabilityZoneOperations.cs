using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V2;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public interface IAvailabilityZoneOperations<T> where T : AvailabilityZone
    {
        Task<IList<T>> GetAvailabilityZonesAsync(ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetAvailabilityZonesPaginatedAsync(ClientRequestParameters parameters = null);

        Task<T> GetAvailabilityZoneAsync(string azID);
    }
}