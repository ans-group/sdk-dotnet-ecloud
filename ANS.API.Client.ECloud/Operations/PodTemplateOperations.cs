﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ANS.API.Client.ECloud.Models.V1;
using ANS.API.Client.ECloud.Models.V1.Request;
using ANS.API.Client.Models;
using ANS.API.Client.Request;

namespace ANS.API.Client.ECloud.Operations
{
    public class PodTemplateOperations<T> : ECloudOperations, IPodTemplateOperations<T> where T : Template
    {
        public PodTemplateOperations(IANSECloudClient client) : base(client)
        {
        }

        public async Task<IList<T>> GetPodTemplatesAsync(int podID, ClientRequestParameters parameters = null)
        {
            return await this.Client.GetAllAsync((ClientRequestParameters funcParameters) => GetPodTemplatesPaginatedAsync(podID, funcParameters), parameters);
        }

        public async Task<Paginated<T>> GetPodTemplatesPaginatedAsync(int podID, ClientRequestParameters parameters = null)
        {
            if (podID < 1)
            {
                throw new Client.Exception.ANSClientValidationException("Invalid pod id");
            }

            return await this.Client.GetPaginatedAsync<T>($"/ecloud/v1/pods/{podID}/templates", parameters);
        }

        public async Task<T> GetPodTemplateAsync(int podID, string templateName)
        {
            if (podID < 1)
            {
                throw new Client.Exception.ANSClientValidationException("Invalid pod id");
            }
            if (string.IsNullOrEmpty(templateName))
            {
                throw new Client.Exception.ANSClientValidationException("Invalid template name");
            }

            return await this.Client.GetAsync<T>($"/ecloud/v1/pods/{podID}/templates/{templateName}");
        }

        public async Task DeletePodTemplateAsync(int podID, string templateName)
        {
            if (podID < 1)
            {
                throw new Client.Exception.ANSClientValidationException("Invalid pod id");
            }
            if (string.IsNullOrEmpty(templateName))
            {
                throw new Client.Exception.ANSClientValidationException("Invalid template name");
            }

            await this.Client.DeleteAsync($"/ecloud/v1/pods/{podID}/templates/{templateName}");
        }

        public async Task RenamePodTemplateAsync(int podID, string templateName, RenameTemplateRequest req)
        {
            if (podID < 1)
            {
                throw new Client.Exception.ANSClientValidationException("Invalid pod id");
            }
            if (string.IsNullOrEmpty(templateName))
            {
                throw new Client.Exception.ANSClientValidationException("Invalid template name");
            }

            await this.Client.PostAsync($"/ecloud/v1/pods/{podID}/templates/{templateName}/move", req);
        }
    }
}