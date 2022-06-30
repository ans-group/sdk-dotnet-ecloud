using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V2;
using ANS.API.Client.Exception;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public class ImageOperations<T> : ECloudOperations, IImageOperations<T> where T : Image
    {
        public ImageOperations(IANSECloudClient client) : base(client)
        {
        }

        public async Task<IList<T>> GetImagesAsync(ClientRequestParameters parameters = null)
        {
            return await Client.GetAllAsync(GetImagesPaginatedAsync, parameters);
        }

        public async Task<Paginated<T>> GetImagesPaginatedAsync(ClientRequestParameters parameters = null)
        {
            return await Client.GetPaginatedAsync<T>("/ecloud/v2/images", parameters);
        }

        public async Task<T> GetImageAsync(string imageID)
        {
            if (string.IsNullOrWhiteSpace(imageID))
            {
                throw new ANSClientValidationException("Invalid image id");
            }

            return await Client.GetAsync<T>($"/ecloud/v2/images/{imageID}");
        }
    }
}