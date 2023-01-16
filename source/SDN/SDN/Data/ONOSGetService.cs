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

        public void GetAll(ONOS onos) // for system auto do
        {
            onos.devices_json = this.GetONOSInformation(onos, "devices");
            onos.hosts_json = this.GetONOSInformation(onos, "hosts");
            onos.links_json = this.GetONOSInformation(onos, "links");
            onos.flows_json = this.GetONOSInformation(onos, "flows");
            onos.networkconfiguration_json = this.GetONOSInformation(onos, "network/configuration");
        }
    }
}