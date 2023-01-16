using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace SDN.Data
{
    public class ONOSPostService
    {
        public async void PostVlanTest()
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
            var response = await client.PostAsync(url, data);
            string result = response.Content.ReadAsStringAsync().Result;
        }
    }
}