using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V2;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public interface IImageOperations<T> where T : Image
    {
        Task<IList<T>> GetImagesAsync(ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetImagesPaginatedAsync(ClientRequestParameters parameters = null);

        Task<T> GetImageAsync(string imageID);
    }
}