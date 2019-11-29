using System;
using System.Net;
using System.IO;
using System.Text;
using Nancy.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace finalLab
{
    class Program
    {
        static void Main(string[] args)
        {
            const string BASE_URL = "https://opendata.vancouver.ca/api/records/1.0/search/?dataset=food-vendors";
            string jsonString = CallRestMethod(BASE_URL);
            // Console.WriteLine(jsonString);
            Rootobject rootObject = JsonConvert.DeserializeObject<Rootobject>(jsonString);
        }
        static string CallRestMethod(string uri)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                StreamReader responseStream = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string stringResponse = responseStream.ReadToEnd();
                return stringResponse;
                response.Close();
                responseStream.Close();
            }
            catch (Exception e)
            {
                return $"{{'Error':'{e.Message}'}}";
            }
        }

    }
}
