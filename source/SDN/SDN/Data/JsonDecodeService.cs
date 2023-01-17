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
            var vplsList = data.SelectToken("apps.['org.onosproject.vpls'].vpls.vplsList").ToList();
            
            foreach (var vpls in vplsList)
            {
                var vpls_jobj = (JObject)JsonConvert.DeserializeObject(vpls.ToString());
                string vlanName = vpls_jobj.SelectToken("name").ToString();
                //Console.WriteLine(vlanName);
                var hosts = vpls_jobj.SelectToken("interfaces").ToList();
                foreach (var hostname in hosts) // all get hosts in vpls
                {
                    var host = hostList.SingleOrDefault(x => x.interfaceName == hostname.ToString()); // find the same hostname host in hostList in onos
                    if (host != null)
                    {
                        host.vlanName = vlanName;
                    }
                }
                //Console.WriteLine(hosts);
            }

            //Console.WriteLine(vplsList);

            return;
        }


    }
}