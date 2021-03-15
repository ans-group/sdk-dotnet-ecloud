using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V2;
using UKFast.API.Client.Exception;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public class ImageMetadataOperations<T> : ECloudOperations, IImageMetadataOperations<T> where T : ImageMetadata
    {
        public ImageMetadataOperations(IUKFastECloudClient client) : base(client)
        {
        }

        public async Task<IList<T>> GetImageMetadataAsync(string imageID, ClientRequestParameters parameters = null)
        {
            return await this.Client.GetAllAsync((ClientRequestParameters funcMetadata) => GetImageMetadataPaginatedAsync(imageID, funcMetadata), parameters);
        }

        public async Task<Paginated<T>> GetImageMetadataPaginatedAsync(string imageID, ClientRequestParameters parameters = null)
        {
            if (string.IsNullOrWhiteSpace(imageID))
            {
                throw new UKFastClientValidationException("Invalid image id");
            }

            return await Client.GetPaginatedAsync<T>($"/ecloud/v2/images/{imageID}/metadata", parameters);
        }
    }
}