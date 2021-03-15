using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V2;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public interface IImageMetadataOperations<T> where T : ImageMetadata
    {
        Task<IList<T>> GetImageMetadataAsync(string imageID, ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetImageMetadataPaginatedAsync(string imageID, ClientRequestParameters parameters = null);
    }
}