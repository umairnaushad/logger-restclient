using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;

namespace REST_Client_API
{    class RijksMuseumApi
    {
        private string apiKey="tckFvOqN";
        private string uri= "https://www.rijksmuseum.nl/api/nl/";
        private RestRequest restRequest;
        private RestClient restClient;
        private RestResponse restResponse;
        private JObject jsonObject;

        public void getCollection()
        {
            restClient = new RestClient(uri);
            restRequest = new RestRequest("collection?key="+ apiKey +"&involvedMaker=Rembrandt+van+Rijn", Method.GET);
            restResponse = (RestResponse)restClient.Execute(restRequest);
            Console.WriteLine("restResponse.Content = " + restResponse.Content.ToString());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            RijksMuseumApi obj = new RijksMuseumApi();
            obj.getCollection();
        }
    }
}
