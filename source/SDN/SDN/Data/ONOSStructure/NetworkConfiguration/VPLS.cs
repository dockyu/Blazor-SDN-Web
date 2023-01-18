namespace SDN.Data.ONOSStructure.NetworkConfiguration
{
    public class VPLS
    {
        public VPLS()
        {
            this.vplsList = new List<Vlan>();

        }
        public void Delete()
        {

        }
        public List<Vlan>? vplsList { get; set; }

        public void AddvplsList(string vlanname, string hostname)
        {
            var vlan = this.vplsList.SingleOrDefault(x => x.name == vlanname);
            if (vlan != null) // exist
            {
                vlan.interfaces.Add(hostname);
            }
            else
            {
                var addVlan = new Vlan();
                addVlan.name = vlanname;
                addVlan.interfaces.Add(hostname);
                this.vplsList.Add(addVlan);
            }
        }

    }

    public class Vlan
    {
        public Vlan()
        {
            this.interfaces = new List<string>();
        }
        public string? name { get; set; }
        public List<string>? interfaces { get; set; }
    }


}