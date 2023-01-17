namespace SDN.Data.ONOSStructure.NetworkConfiguration
{
    public class VPLS
    {
        public VPLS()
        {
            this.vplsList = new List<Vlan>();

        }
        public List<Vlan>? vplsList { get; set; }

        public void AddvplsList(string vlanname, string hostname)
        {
            foreach (var vlan in this.vplsList)
            {
                if (vlan.name == vlanname)
                {
                    vlan.interfaces.Add(hostname);
                    return;
                }
            }
            
            // have no vlan with vlanname
            this.vplsList.Add(new Vlan { 
                name=vlanname, interfaces=new List<string> { hostname } }
            );
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