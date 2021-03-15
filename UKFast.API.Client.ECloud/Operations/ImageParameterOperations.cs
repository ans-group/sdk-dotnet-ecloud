using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V2;
using UKFast.API.Client.Exception;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public class ImageParameterOperations<T> : ECloudOperations, IImageParameterOperations<T> where T : ImageParameter
    {
        public ImageParameterOperations(IUKFastECloudClient client) : base(client)
        {
        }

        public async Task<IList<T>> GetImageParametersAsync(string imageID, ClientRequestParameters parameters = null)
        {
            return await this.Client.GetAllAsync((ClientRequestParameters funcParameters) => GetImageParametersPaginatedAsync(imageID, funcParameters), parameters);
        }

        public async Task<Paginated<T>> GetImageParametersPaginatedAsync(string imageID, ClientRequestParameters parameters = null)
        {
            if (string.IsNullOrWhiteSpace(imageID))
            {
                throw new UKFastClientValidationException("Invalid image id");
            }

            return await Client.GetPaginatedAsync<T>($"/ecloud/v2/images/{imageID}/parameters", parameters);
        }
    }
}