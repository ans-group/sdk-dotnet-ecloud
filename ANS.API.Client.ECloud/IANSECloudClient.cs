using ANS.API.Client.ECloud.Models.V1;
using ANS.API.Client.ECloud.Models.V2;
using ANS.API.Client.ECloud.Operations;

namespace ANS.API.Client.ECloud
{
    public interface IANSECloudClient : IANSClient
    {
        IVirtualMachineOperations<VirtualMachine> VirtualMachineOperations();

        IVirtualMachineTagOperations<Tag> VirtualMachineTagOperations();

        ISolutionOperations<Solution> SolutionOperations();

        ISolutionTemplateOperations<Template> SolutionTemplateOperations();

        ISolutionVirtualMachineOperations<VirtualMachine> SolutionVirtualMachineOperations();

        ISolutionHostOperations<Host> SolutionHostOperations();

        ISolutionDatastoreOperations<Datastore> SolutionDatastoreOperations();

        ISolutionSiteOperations<Site> SolutionSiteOperations();

        ISolutionNetworkOperations<NetworkV1> SolutionNetworkOperations();

        ISolutionFirewallOperations<Firewall> SolutionFirewallOperations();

        ISolutionTagOperations<Tag> SolutionTagOperations();

        ISiteOperations<Site> SiteOperations();

        IHostOperations<Host> HostOperations();

        IDatastoreOperations<Datastore> DatastoreOperations();

        IFirewallOperations<Firewall> FirewallOperations();

        IPodOperations<Pod> PodOperations();

        IPodApplianceOperations<Appliance> PodApplianceOperations();

        IPodTemplateOperations<Template> PodTemplateOperations();

        IPodGPUProfileOperations<GPUProfile> PodGPUProfileOperations();

        IActiveDirectoryDomainOperations<ActiveDirectoryDomain> ActiveDirectoryDomainOperations();

        IApplianceOperations<Appliance> ApplianceOperations();

        IApplianceParameterOperations<ApplianceParameter> ApplianceParameterOperations();

        ICreditOperations<Credit> CreditOperations();

        IAvailabilityZoneOperations<AvailabilityZone> AvailabilityZoneOperations();

        IDHCPOperations<DHCP> DHCPOperations();

        IFirewallRuleOperations<FirewallRule> FirewallRuleOperations();

        IFirewallPolicyOperations<FirewallPolicy> FirewallPolicyOperations();

        IFloatingOperations<FloatingIP> FloatingIPOperations();

        IImageOperations<Image> ImageOperations();

        IImageParameterOperations<ImageParameter> ImageParameterOperations();

        IImageMetadataOperations<ImageMetadata> ImageMetadataOperations();

        IInstanceOperations<Instance> InstanceOperations();

        INetworkOperations<Network> NetworkOperations();

        IRouterOperations<Router> RouterOperations();

        IRegionOperations<Region> RegionOperations();

        IVPNOperations<VPN> VPNOperations();

        IVPCOperations<VPC> VPCOperations();

        ILoadBalancerOperations<LoadBalancerCluster> LoadBalancerOperations();
    }
}