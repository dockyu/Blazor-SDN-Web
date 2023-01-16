namespace SDN.Data
{
    public class ONOS
    {
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

        public VPLS? vpls { get; set; }
    }

}