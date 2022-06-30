using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V2;
using ANS.API.Client.Exception;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public class ImageParameterOperations<T> : ECloudOperations, IImageParameterOperations<T> where T : ImageParameter
    {
        public ImageParameterOperations(IANSECloudClient client) : base(client)
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
                throw new ANSClientValidationException("Invalid image id");
            }

            return await Client.GetPaginatedAsync<T>($"/ecloud/v2/images/{imageID}/parameters", parameters);
        }
    }
}