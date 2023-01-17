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
        }
        public string? onosipadd { get; set; }
        public string? onosport { get; set; }
        public string? devices_json { get; set; }
        public string? hosts_json { get; set; }
        public string? links_json { get; set; }
        public string? flows_json { get; set; }
        public string? networkconfiguration_json { get; set; }

        //public List<string?>? devices_list { get; set; }
        //public List<string?>? hosts_list { get; set; }
        //public List<string?>? links_list { get; set; }
        //public List<string?>? flows_list { get; set; }
        //public List<string?>? networkconfiguration_list { get; set; }

        public VPLS? Vpls { get; set; }
        public PORTS? Ports { get; set; }
    }

}