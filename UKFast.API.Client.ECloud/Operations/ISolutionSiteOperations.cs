﻿using System.Collections.Generic;
using System.Threading.Tasks;
using UKFast.API.Client.ECloud.Models.V1;
using UKFast.API.Client.Models;
using UKFast.API.Client.Request;

namespace UKFast.API.Client.ECloud.Operations
{
    public interface ISolutionSiteOperations<T> where T : Site
    {
        Task<IList<T>> GetSolutionSitesAsync(int solutionID, ClientRequestParameters parameters = null);

        Task<Paginated<T>> GetSolutionSitesPaginatedAsync(int solutionID, ClientRequestParameters parameters = null);
    }
}