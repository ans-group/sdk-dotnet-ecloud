using UKFast.API.Client.ECloud.Models.V1;
using UKFast.API.Client.ECloud.Models.V2;
using UKFast.API.Client.ECloud.Operations;

namespace UKFast.API.Client.ECloud
{
    public partial class UKFastECloudClient : UKFastClient, IUKFastECloudClient
    {
        public UKFastECloudClient(IConnection connection) : base(connection)
        {
        }

        public UKFastECloudClient(IConnection connection, ClientConfig config) : base(connection, config)
        {
        }

        public IVirtualMachineOperations<VirtualMachine> VirtualMachineOperations()
        {
            return new VirtualMachineOperations<VirtualMachine>(this);
        }

        public IVirtualMachineTagOperations<Tag> VirtualMachineTagOperations()
        {
            return new VirtualMachineTagOperations<Tag>(this);
        }

        public ISolutionOperations<Solution> SolutionOperations()
        {
            return new SolutionOperations<Solution>(this);
        }

        public ISolutionTemplateOperations<Template> SolutionTemplateOperations()
        {
            return new SolutionTemplateOperations<Template>(this);
        }

        public ISolutionVirtualMachineOperations<VirtualMachine> SolutionVirtualMachineOperations()
        {
            return new SolutionVirtualMachineOperations<VirtualMachine>(this);
        }

        public ISolutionHostOperations<Host> SolutionHostOperations()
        {
            return new SolutionHostOperations<Host>(this);
        }

        public ISolutionDatastoreOperations<Datastore> SolutionDatastoreOperations()
        {
            return new SolutionDatastoreOperations<Datastore>(this);
        }

        public ISolutionSiteOperations<Site> SolutionSiteOperations()
        {
            return new SolutionSiteOperations<Site>(this);
        }

        public ISolutionNetworkOperations<NetworkV1> SolutionNetworkOperations()
        {
            return new SolutionNetworkOperations<NetworkV1>(this);
        }

        public ISolutionFirewallOperations<Firewall> SolutionFirewallOperations()
        {
            return new SolutionFirewallOperations<Firewall>(this);
        }

        public ISolutionTagOperations<Tag> SolutionTagOperations()
        {
            return new SolutionTagOperations<Tag>(this);
        }

        public ISiteOperations<Site> SiteOperations()
        {
            return new SiteOperations<Site>(this);
        }

        public IHostOperations<Host> HostOperations()
        {
            return new HostOperations<Host>(this);
        }

        public IDatastoreOperations<Datastore> DatastoreOperations()
        {
            return new DatastoreOperations<Datastore>(this);
        }

        public IFirewallOperations<Firewall> FirewallOperations()
        {
            return new FirewallOperations<Firewall>(this);
        }

        public IPodOperations<Pod> PodOperations()
        {
            return new PodOperations<Pod>(this);
        }

        public IPodApplianceOperations<Appliance> PodApplianceOperations()
        {
            return new PodApplianceOperations<Appliance>(this);
        }

        public IPodTemplateOperations<Template> PodTemplateOperations()
        {
            return new PodTemplateOperations<Template>(this);
        }

        public IPodGPUProfileOperations<GPUProfile> PodGPUProfileOperations()
        {
            return new PodGPUProfileOperations<GPUProfile>(this);
        }

        public IActiveDirectoryDomainOperations<ActiveDirectoryDomain> ActiveDirectoryDomainOperations()
        {
            return new ActiveDirectoryDomainOperations<ActiveDirectoryDomain>(this);
        }

        public IApplianceOperations<Appliance> ApplianceOperations()
        {
            return new ApplianceOperations<Appliance>(this);
        }

        public IApplianceParameterOperations<ApplianceParameter> ApplianceParameterOperations()
        {
            return new ApplianceParameterOperations<ApplianceParameter>(this);
        }

        public ICreditOperations<Credit> CreditOperations()
        {
            return new CreditOperations<Credit>(this);
        }

        public IAvailabilityZoneOperations<AvailabilityZone> AvailabilityZoneOperations()
        {
            return new AvailabilityZoneOperations<AvailabilityZone>(this);
        }

        public IDHCPOperations<DHCP> DHCPOperations()
        {
            return new DHCPOperations<DHCP>(this);
        }

        public IFirewallRuleOperations<FirewallRule> FirewallRuleOperations()
        {
            return new FirewallRuleOperations<FirewallRule>(this);
        }

        public IFirewallPolicyOperations<FirewallPolicy> FirewallPolicyOperations()
        {
            return new FirewallPolicyOperations<FirewallPolicy>(this);
        }

        public IFloatingOperations<FloatingIP> FloatingIPOperations()
        {
            return new FloatingIPOperations<FloatingIP>(this);
        }

        public IInstanceOperations<Instance> InstanceOperations()
        {
            return new InstanceOperations<Instance>(this);
        }

        public INetworkOperations<Network> NetworkOperations()
        {
            return new NetworkOperations<Network>(this);
        }

        public IRouterOperations<Router> RouterOperations()
        {
            return new RouterOperations<Router>(this);
        }

        public IRegionOperations<Region> RegionOperations()
        {
            return new RegionOperations<Region>(this);
        }

        public IVPNOperations<VPN> VPNOperations()
        {
            return new VPNOperations<VPN>(this);
        }

        public IVPCOperations<VPC> VPCOperations()
        {
            return new VPCOperations<VPC>(this);
        }

        public ILoadBalancerOperations<LoadBalancerCluster> LoadBalancerOperations()
        {
            return new LoadBalancerOperations<LoadBalancerCluster>(this);
        }
    }
}