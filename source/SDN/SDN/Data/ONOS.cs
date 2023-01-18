using SDN.Data.ONOSStructure.NetworkConfiguration;

namespace SDN.Data
{
    public class ONOS
    {
        //public void Initial()
        //{
        //    this.Vpls = new VPLS();
        //    this.Ports = new PORTS();
        //}

        public ONOS()
        {
            this.Vpls = new VPLS();
            this.Ports = new PORTS();
            this.hostsList = new List<Host>();
        }
        public string? onosipadd { get; set; }
        public string? onosport { get; set; }
        public string? devices_jsonstring { get; set; }
        public string? hosts_jsonstring { get; set; }
        public string? links_jsonstring { get; set; }
        public string? flows_jsonstring { get; set; }
        public string? networkconfiguration_jsonstring { get; set; }

        //public List<string?>? devices_list { get; set; }
        //public List<string?>? hosts_list { get; set; }
        //public List<string?>? links_list { get; set; }
        //public List<string?>? flows_list { get; set; }
        //public List<string?>? networkconfiguration_list { get; set; }

        public VPLS? Vpls { get; set; }
        public PORTS? Ports { get; set; }
        public List<Host>? hostsList { get; set; }
    }

    public class Host
    {
        public Host()
        {
            this.interfaceName = new string("");
            this.location = new Location();
            this.vlanName = new string("none");
        }

        public string? interfaceName; // at hosts <=> API
        public Location? location;

        public string? vlanName; // vplsList at network/configuration <=> API
    }

    public class Location
    {
        public Location()
        {
            this.elementId = new string("");
            this.port = new string("");
        }

        public string? elementId;
        public string? port;
    }

}