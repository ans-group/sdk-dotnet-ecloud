using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V1;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public interface ISolutionVirtualMachineOperations<T> where T : VirtualMachine
    {
        Task<IList<T>> GetSolutionVirtualMachinesAsync(int solutionID, ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetSolutionVirtualMachinesPaginatedAsync(int solutionID, ClientRequestParameters parameters = null);
    }
}