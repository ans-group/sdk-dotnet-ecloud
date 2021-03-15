using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V2;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public interface IImageOperations<T> where T : Image
    {
        Task<IList<T>> GetImagesAsync(ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetImagesPaginatedAsync(ClientRequestParameters parameters = null);

        Task<T> GetImageAsync(string imageID);
    }
}