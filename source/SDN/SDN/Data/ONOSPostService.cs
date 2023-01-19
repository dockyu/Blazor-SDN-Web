using Newtonsoft.Json;
using SDN.Data.ONOSStructure.NetworkConfiguration;
using System.Net;
using System.Text;

namespace SDN.Data
{
    public class ONOSPostService
    {

        public void UpdateONOSPorts(ONOS onos) // post whole vpls json to network/configuration
        {
            // renew Vpls and Ports
            onos.Ports.Delete();
            onos.Ports = new PORTS();

            foreach (var host in onos.hostsList)
            {
                if (!String.IsNullOrEmpty(host.interfaceName))
                {
                    onos.Ports.AddPort(host.location.elementId + "/" + host.location.port, host.interfaceName);
                }
            }
            return;
        }

        public void UpdateONOSVpls(ONOS onos) // post whole vpls json to network/configuration
        {
            // renew Vpls and Ports
            onos.Vpls.Delete();
            onos.Vpls = new VPLS();

            foreach (var host in onos.hostsList)
            {
                if (host.vlanName != "none" && !String.IsNullOrEmpty(host.interfaceName))
                {
                    onos.Vpls.AddvplsList(host.vlanName, host.interfaceName);
                    Console.WriteLine("AddvplsList : vlanName:" + host.vlanName + ", interfaceName:" + host.interfaceName);
                }
            }
            return;
        }
        public async void PostVPLSports(ONOS onos)
        {
            foreach (var oneinterface in onos.Ports.interface_list)
            {
                var interfaces = new List<object>();
                interfaces.Add(onos.Ports.Interfaces(oneinterface));
                var port = new
                {
                    ports = new Dictionary<string, object>
                    {
                        {
                            oneinterface.port, new
                            {
                                interfaces = interfaces
                            }
                        }
                    }
                };

                HttpClientHandler httpClientHandler = new HttpClientHandler()
                {
                    Credentials = new NetworkCredential("onos", "rocks"),
                };

                var json = JsonConvert.SerializeObject(port);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = "http://" + onos.onosipadd + ":" + onos.onosport + "/onos/v1/network/configuration";
                using var client = new HttpClient(httpClientHandler);
                var response = await client.PostAsync(url, data);
                string result = response.Content.ReadAsStringAsync().Result;
            }

        }

        public async Task<HttpResponseMessage> PostVPLSapps(ONOS onos)
        {
            var apps = new
            {
                apps = new Dictionary<string, object>
                {
                    {
                        "org.onosproject.vpls", new
                        {
                            vpls = onos.Vpls
                        }
                    }

                }
            };

            HttpClientHandler httpClientHandler = new HttpClientHandler()
            {
                Credentials = new NetworkCredential("onos", "rocks"),
            };

            var json = JsonConvert.SerializeObject(apps);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var url = "http://" + onos.onosipadd + ":" + onos.onosport + "/onos/v1/network/configuration";
            using var client = new HttpClient(httpClientHandler);

            HttpResponseMessage responseMessage = null;
            try
            {
                var response = await client.PostAsync(url, data);
            }
            catch (Exception ex)
            {
                if (responseMessage == null)
                {
                    responseMessage = new HttpResponseMessage();
                }
                responseMessage.StatusCode = HttpStatusCode.InternalServerError;
                responseMessage.ReasonPhrase = string.Format("RestHttpClient.SendRequest failed: {0}", ex);
            }
            return responseMessage;
        }
        public async Task<HttpResponseMessage> PostVlanTest()
        {
            var Vpls = new VPLS();
            var vlan1 = new Vlan();
            var vlan2 = new Vlan();

            vlan1.name = "VPLS1";
            vlan1.interfaces = new List<string>(){ "h10", "h30", "h50"};
            vlan2.name = "VPLS2";
            vlan2.interfaces = new List<string>() { "h2", "h4", "h6" };

            Vpls.vplsList = new List<Vlan> { vlan1, vlan2 };
            

            var senddata = new
            {
                apps = new Dictionary<string, object>
                {
                    {
                        "org.onosproject.vpls", new
                        {
                            vpls = Vpls
                        }
                    }
                    
                }
            };

            HttpClientHandler httpClientHandler = new HttpClientHandler()
            {
                Credentials = new NetworkCredential("onos", "rocks"),
            };

            var json = JsonConvert.SerializeObject(senddata);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var url = "http://192.168.98.142:8181/onos/v1/network/configuration";
            using var client = new HttpClient(httpClientHandler);

            HttpResponseMessage responseMessage = null;
            try
            {
                var response = await client.PostAsync(url, data);
            }
            catch (Exception ex)
            {
                if (responseMessage == null)
                {
                    responseMessage = new HttpResponseMessage();
                }
                responseMessage.StatusCode = HttpStatusCode.InternalServerError;
                responseMessage.ReasonPhrase = string.Format("RestHttpClient.SendRequest failed: {0}", ex);
            }
            return responseMessage;
        }
    }
}