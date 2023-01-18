using System.Reflection.Metadata.Ecma335;

namespace SDN.Data.ONOSStructure.NetworkConfiguration
{
    public class PORTS
    {
        public PORTS()
        {
            this.interface_list = new List<Interface>();

        }
        public void Delete()
        {

        }
        public List<Interface>? interface_list = new List<Interface>();

        public void AddPort(string addport, string addname)
        {
            Interface addinterface = new Interface { port = addport, name = addname };
            this.interface_list.Add(addinterface);
        }

        public void RemovePort(string removename)
        {
            foreach(var removeinterface in this.interface_list)
            {
                if (removeinterface.name == removename)
                {
                    this.interface_list.Remove(removeinterface);
                }
            }
        }

        public object Interfaces(Interface oneinterface)
        {
            var obj = new
            {
                name = oneinterface.name
            };
            return obj;
        }
    }

    public class Interface
    {
        public string? port { get; set; }
        public string? name { get; set; }
    }
}