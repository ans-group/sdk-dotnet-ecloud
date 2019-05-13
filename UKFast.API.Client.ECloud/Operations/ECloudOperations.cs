using UKFast.API.Client.Operations;

namespace UKFast.API.Client.ECloud.Operations
{
    public abstract class ECloudOperations : OperationsBase<IUKFastECloudClient>
    {
        public ECloudOperations(IUKFastECloudClient client) : base(client) { }

        protected string _resource = "/ecloud";
    }
}