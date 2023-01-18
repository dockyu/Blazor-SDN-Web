using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace SDN.Data
{
    public class JsonDecodeService
    {
        public List<string> DecodeVlan(ONOS onso, string jsonstring)
        {
            var result = new List<string>();
            return result;
        }

        public static List<Host> DecodeHosts(string hosts_jsonstring)
        {
            List<Host> hostList = new List<Host>();
            var data = (JObject)JsonConvert.DeserializeObject(hosts_jsonstring);
            var hosts_list = data.SelectToken("hosts").ToList();
            foreach (var host in hosts_list)
            {
                var location = (JObject)host.SelectToken("locations").ToList()[0];
                Host addHost = new Host();
                addHost.location.elementId = location.SelectToken("elementId").ToString();
                addHost.location.port = location.SelectToken("port").ToString();
                //Console.WriteLine("host");
                //Console.WriteLine("elementId="+ addHost.location.elementId);
                //Console.WriteLine("port=" + addHost.location.port);
                hostList.Add(addHost);
            }
            return hostList;
        }

        public static void DecodeNetworkConfiguration(List<Host> hostList, string networkconfiguration_jsonstring)
        {
            var data = (JObject)JsonConvert.DeserializeObject(networkconfiguration_jsonstring);
            
            try
            {
                var ports = data.SelectToken("ports").ToList();
                //Console.WriteLine(ports.Count);
                foreach (var port in ports) // all port in network/configuration posts
                {
                    var portString = port.ToString();
                    int ofPosition = portString.IndexOf("of");
                    string? location = portString.Substring(ofPosition, 21); // get the location (deviceId/port)
                    //Console.WriteLine(portString);
                    var port_jobj = (JObject)JsonConvert.DeserializeObject("{"+port.ToString()+"}");
                    string? interfaceName = ((JObject)JsonConvert.DeserializeObject(port_jobj.SelectToken(location + ".interfaces").ToList()[0].ToString())).SelectToken("name").ToString(); // get the interfaceName
                    //Console.WriteLine(interfaceName + "\n");
                    var host = hostList.SingleOrDefault(x => (x.location.elementId+"/"+x.location.port) == location); // find the same hostname host in hostList in onos
                    if (host != null)
                    {
                        host.interfaceName = interfaceName;
                    }
                }

                var vplsList = data.SelectToken("apps.['org.onosproject.vpls'].vpls.vplsList").ToList(); // maybe throw ArgumentNullException when never post vpls
                foreach (var vpls in vplsList) // all get vlan from vpls
                {
                    var vpls_jobj = (JObject)JsonConvert.DeserializeObject(vpls.ToString());
                    string vlanName = vpls_jobj.SelectToken("name").ToString();
                    //Console.WriteLine(vlanName);
                    var interfaceNameList = vpls_jobj.SelectToken("interfaces").ToList();
                    //Console.WriteLine(interfaceNameList.ToString());
                    foreach (var interfaceName in interfaceNameList) // all get hosts from vpls
                    {
                        //Console.WriteLine("interface Name:" + interfaceName.ToString());
                        if (String.IsNullOrEmpty(interfaceName.ToString()))
                        {
                            continue;
                        }
                        var host = hostList.SingleOrDefault(x => x.interfaceName == interfaceName.ToString()); // find the same hostname host in hostList in onos
                        if (host != null)
                        {
                            host.vlanName = vlanName;
                        }
                    }
                    //Console.WriteLine(hosts);
                }
            }
            catch (ArgumentNullException e)
            {

            }

            //Console.WriteLine(vplsList);

            return;
        }


    }
}