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
                var endpoint = new Uri("http://" + onos.ipadd + ":8181/onos/v1/" + getItem);
                var result = client.GetAsync(endpoint).Result;
                var jsonstring = result.Content.ReadAsStringAsync().Result;
                return jsonstring;
            }
        }
    }
}