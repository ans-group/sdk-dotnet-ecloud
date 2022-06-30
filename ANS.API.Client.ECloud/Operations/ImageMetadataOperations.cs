using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V2;
using ANS.API.Client.Exception;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public class ImageMetadataOperations<T> : ECloudOperations, IImageMetadataOperations<T> where T : ImageMetadata
    {
        public ImageMetadataOperations(IANSECloudClient client) : base(client)
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
                throw new ANSClientValidationException("Invalid image id");
            }

            return await Client.GetPaginatedAsync<T>($"/ecloud/v2/images/{imageID}/metadata", parameters);
        }
    }
}