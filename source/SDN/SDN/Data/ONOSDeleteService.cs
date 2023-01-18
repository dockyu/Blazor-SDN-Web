using Newtonsoft.Json;
using System.Net;

namespace SDN.Data
{
    public class ONOSDeleteService
    {
        
        public void DeleteONOSInformation(ONOS onos, string getItem)
        {
            HttpClientHandler httpClientHandler = new HttpClientHandler()
            {
                Credentials = new NetworkCredential("onos", "rocks"),
            };

            using (var client = new HttpClient(httpClientHandler))
            {
                var endpoint = new Uri("http://" + onos.onosipadd + ":" + onos.onosport + "/onos/v1/" + getItem);
                var result = client.DeleteAsync(endpoint).Result;
                var jsonstring = result.Content.ReadAsStringAsync().Result;
                return;
            }
        }
    }
}