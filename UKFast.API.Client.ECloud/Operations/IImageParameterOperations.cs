using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V2;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public interface IImageParameterOperations<T> where T : ImageParameter
    {
        Task<IList<T>> GetImageParametersAsync(string imageID, ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetImageParametersPaginatedAsync(string imageID, ClientRequestParameters parameters = null);
    }
}