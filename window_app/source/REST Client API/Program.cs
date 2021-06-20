using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;

namespace REST_Client_API
{
    class RijksMuseumApi
    {
        private string apiKey="tckFvOqN";
        private string uri= "https://www.rijksmuseum.nl/api/nl/";
        private RestRequest restRequest;
        private RestClient restClient;
        private RestResponse restResponse;
        private JObject jsonObject;

        public RijksMuseumApi()
        {
            restClient = new RestClient(uri);
        }

        public void getCollectionByArtistName(string artistName)
        {            
            restRequest = new RestRequest("collection?key="+ apiKey +"&involvedMaker="+artistName, Method.GET);
            restResponse = (RestResponse)restClient.Execute(restRequest);
            Console.WriteLine("restResponse.Content = " + restResponse.Content.ToString());
            jsonObject = JObject.Parse(restResponse.Content);
            var count = jsonObject.SelectToken("count").ToString();
            Console.WriteLine("====== Response ======");
            Console.WriteLine("count= " + count);
            var artistWork = JsonConvert.DeserializeObject<ArtistWork.Root>(jsonObject.ToString());
            Console.WriteLine("====== Response ======");
            Console.WriteLine("count= " + artistWork.count);
            //Product deserializedProduct = JsonConvert.DeserializeObject<Product>(output);
        }

        public void getArtistsList()
        {
            restRequest = new RestRequest("collection?key=" + apiKey + "&q=artist", Method.GET);
            restResponse = (RestResponse)restClient.Execute(restRequest);
            Console.WriteLine("restResponse.Content = " + restResponse.Content.ToString());

        }

        public void ParseResponseArtistList()
        {
            Console.WriteLine("====== ParseResponse ======");
            jsonObject = JObject.Parse(restResponse.Content);
            var count = jsonObject.SelectToken("count").ToString();
            Console.WriteLine("====== Response ======");
            Console.WriteLine("count= " + count);
            
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            RijksMuseumApi obj = new RijksMuseumApi();
            //obj.getArtistsList();
            //obj.ParseResponseArtistList();
            obj.getCollectionByArtistName("Paris-Artiste");
        }
    }
}
