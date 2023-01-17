using Newtonsoft.Json;
using System.Net;

namespace SDN.Data
{
    public class ONOSGetService
    {
        
        public String GetONOSInformation(ONOS onos, string getItem)
        {
            HttpClientHandler httpClientHandler = new HttpClientHandler()
            {
                Credentials = new NetworkCredential("onos", "rocks"),
            };

            using (var client = new HttpClient(httpClientHandler))
            {
                var endpoint = new Uri("http://" + onos.onosipadd + ":" + onos.onosport + "/onos/v1/" + getItem);
                var result = client.GetAsync(endpoint).Result;
                var jsonstring = result.Content.ReadAsStringAsync().Result;
                return jsonstring;
            }
        }

        public async Task<object> GetONOSJson(ONOS onos, string getItem)
        {
            HttpClientHandler httpClientHandler = new HttpClientHandler()
            {
                Credentials = new NetworkCredential("onos", "rocks"),
            };

            using (var client = new HttpClient(httpClientHandler))
            {
                var endpoint = new Uri("http://" + onos.onosipadd + ":" + onos.onosport + "/onos/v1/" + getItem);
                var response = client.GetAsync(endpoint).Result;
                if (response != null)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<object>(jsonString);
                }
                else
                {
                    return null;
                }
            }
        }

        public void GetAll(ONOS onos) // for system auto do
        {
            onos.devices_jsonstring = this.GetONOSInformation(onos, "devices");
            onos.hosts_jsonstring = this.GetONOSInformation(onos, "hosts");
            onos.links_jsonstring = this.GetONOSInformation(onos, "links");
            onos.flows_jsonstring = this.GetONOSInformation(onos, "flows");
            onos.networkconfiguration_jsonstring = this.GetONOSInformation(onos, "network/configuration");
        }
    }
}