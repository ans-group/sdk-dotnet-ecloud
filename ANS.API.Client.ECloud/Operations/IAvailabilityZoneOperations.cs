using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V2;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public interface IAvailabilityZoneOperations<T> where T : AvailabilityZone
    {
        Task<IList<T>> GetAvailabilityZonesAsync(ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetAvailabilityZonesPaginatedAsync(ClientRequestParameters parameters = null);

        Task<T> GetAvailabilityZoneAsync(string azID);
    }
}