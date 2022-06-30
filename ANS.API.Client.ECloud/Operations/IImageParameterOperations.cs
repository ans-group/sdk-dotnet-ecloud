using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V2;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public interface IImageParameterOperations<T> where T : ImageParameter
    {
        Task<IList<T>> GetImageParametersAsync(string imageID, ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetImageParametersPaginatedAsync(string imageID, ClientRequestParameters parameters = null);
    }
}