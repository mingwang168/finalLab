using System;
using System.Net;
using System.IO;
using System.Text;
using Nancy.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using static finalLab.Models;

namespace finalLab
{
    class Program
    {
        static class SqlHelper
        {
            public static SqlDataReader ExecuteReader(String connString, String commandText)
            {
                SqlConnection conn = new SqlConnection(connString);
                using (SqlCommand cmd = new SqlCommand(commandText, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    return reader;
                }
            }
        }

        static void Main(string[] args)
        {


            const string BASE_URL = "https://opendata.vancouver.ca/api/records/1.0/search/?dataset=food-vendors";
            string jsonString = CallRestMethod(BASE_URL);
            // Console.WriteLine(jsonString);
            Rootobject rootObject = JsonConvert.DeserializeObject<Rootobject>(jsonString);

            FoodCartContext db = new FoodCartContext();
            foreach (Record cart in rootObject.records)
            {
                db.Add(new FoodCart
                {
                    key = cart.fields.key,
                    status = cart.fields.status,
                    description=cart.fields.description,
                    geo_localarea=cart.fields.geo_localarea,
                    longitude=cart.fields.geom.coordinates[0],
                    latitude=cart.fields.geom.coordinates[1],
                    location=cart.fields.location,
                    vendor_type=cart.fields.vendor_type,
                    business_name=cart.fields.business_name

                });
            }

            db.SaveChanges();
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
