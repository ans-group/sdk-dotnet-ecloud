using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V1;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public interface ISolutionVirtualMachineOperations<T> where T : VirtualMachine
    {
        Task<IList<T>> GetSolutionVirtualMachinesAsync(int solutionID, ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetSolutionVirtualMachinesPaginatedAsync(int solutionID, ClientRequestParameters parameters = null);
    }
}