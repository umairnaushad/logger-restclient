using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Drawing;

namespace REST_Client_API
{
    public class RijksMuseumApi
    {
        private readonly string apiKey="tckFvOqN";
        private readonly string uri= "https://www.rijksmuseum.nl/api/en/";
        private RestRequest restRequest;
        private RestClient restClient;
        private RestResponse restResponse;
        private JObject jsonObject;

        public RijksMuseumApi()
        {
            restClient = new RestClient(uri);
        }

        public List<Artwork> GetCollectionByArtistName(string artistName) {
            string imageSourceUrl = "";
            string imageLocalPath = "";

            restRequest = new RestRequest("collection?key="+ apiKey +"&involvedMaker="+artistName, Method.GET);
            restResponse = (RestResponse)restClient.Execute(restRequest);
            jsonObject = JObject.Parse(restResponse.Content);

            var artworkList = JsonConvert.DeserializeObject<ArtistWorkDetail.Root>(jsonObject.ToString());
            List<Artwork> artworkParsedList = new List<Artwork>();
            
            using (WebClient client = new WebClient())
            {
                for (int i = 0; i < artworkList.artObjects.Count; i++)
                {
                    imageSourceUrl = artworkList.artObjects[i].webImage.url;
                    imageLocalPath = Directory.GetCurrentDirectory() + "\\images\\" + artworkList.artObjects[i].objectNumber + ".png";
                    artworkParsedList.Add(new Artwork(artworkList.artObjects[i].id,
                        artworkList.artObjects[i].objectNumber, artworkList.artObjects[i].title,
                        artworkList.artObjects[i].longTitle,
                        artworkList.artObjects[i].principalOrFirstMaker,
                        artworkList.artObjects[i].webImage.width, artworkList.artObjects[i].webImage.height,
                        artworkList.artObjects[i].webImage.url, imageLocalPath
                        ));
                    
                    client.DownloadFile(new Uri(imageSourceUrl), imageLocalPath);
                }                
            }

            return artworkParsedList;
        }

        public void getArtworkDetailByObjectNumber(string objectNumber)
        {
            restRequest = new RestRequest("collection/"+objectNumber+"?key=" + apiKey, Method.GET);
            restResponse = (RestResponse)restClient.Execute(restRequest);
            Console.WriteLine("restResponse.Content = " + restResponse.Content.ToString());
            jsonObject = JObject.Parse(restResponse.Content);
            //var count = jsonObject.SelectToken("count").ToString();
            Console.WriteLine("====== Response ======");
            //Console.WriteLine("count= " + count);
            var artwork = JsonConvert.DeserializeObject<ArtworkDetail.Root>(jsonObject.ToString());
            Console.WriteLine("====== Response ======");
            Console.WriteLine("url= " + artwork.artObject.webImage.url);
            string url = artwork.artObject.webImage.url;
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(new Uri(url), "Image.png");
            }
        }

        public void getArtistsList()
        {
            restRequest = new RestRequest("collection?key=" + apiKey + "&q=artist", Method.GET);
            restResponse = (RestResponse)restClient.Execute(restRequest);
            jsonObject = JObject.Parse(restResponse.Content);
            int count = Int32.Parse(jsonObject.SelectToken("count").ToString());

            //var artwork = JsonConvert.DeserializeObject<ArtworkDetail.Root>(jsonObject.ToString());
            Console.WriteLine("restResponse.Content = " + restResponse.Content.ToString());
        }
    }
}
