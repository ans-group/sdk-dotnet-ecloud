using ANS.API.Client.Operations;

namespace ANS.API.Client.ECloud.Operations
{
    public abstract class ECloudOperations : OperationsBase<IANSECloudClient>
    {
        public ECloudOperations(IANSECloudClient client) : base(client)
        {
        }
    }
}