using UKFast.API.Client.ECloud.Models.V1;
using UKFast.API.Client.ECloud.Models.V2;
using UKFast.API.Client.ECloud.Operations;

namespace UKFast.API.Client.ECloud
{
    public interface IUKFastECloudClient : IUKFastClient
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

        IFloatingOperations<FloatingIP> FloatingIPOperations();

        IInstanceOperations<Instance> InstanceOperations();

        INetworkOperations<Network> NetworkOperations();

        IRouterOperations<Router> RouterOperations();

        IRegionOperations<Region> RegionOperations();

        IVPNOperations<VPN> VPNOperations();

        IVPCOperations<VPC> VPCOperations();

        ILoadBalancerOperations<LoadBalancerCluster> LoadBalancerOperations();
    }
}