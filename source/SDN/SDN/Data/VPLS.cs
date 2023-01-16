namespace SDN.Data
{
    public class VPLS
    {
        public List<Vlan>? vplsList { get; set; }
    }

    public class Vlan
    {
        public string? name { get; set; }
        public List<string>? interfaces { get; set; }
    }


}