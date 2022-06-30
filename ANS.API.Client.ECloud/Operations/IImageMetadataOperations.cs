using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V2;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public interface IImageMetadataOperations<T> where T : ImageMetadata
    {
        Task<IList<T>> GetImageMetadataAsync(string imageID, ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetImageMetadataPaginatedAsync(string imageID, ClientRequestParameters parameters = null);
    }
}