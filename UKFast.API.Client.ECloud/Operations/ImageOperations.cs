using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V2;
using UKFast.API.Client.Exception;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public class ImageOperations<T> : ECloudOperations, IImageOperations<T> where T : Image
    {
        public ImageOperations(IUKFastECloudClient client) : base(client)
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
                throw new UKFastClientValidationException("Invalid image id");
            }

            return await Client.GetAsync<T>($"/ecloud/v2/images/{imageID}");
        }
    }
}