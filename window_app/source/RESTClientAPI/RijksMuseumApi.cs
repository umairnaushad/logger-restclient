using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Drawing;

namespace RESTClient
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

        public List<ArtCollectionList> GetCollectionsListByArtistName(string artistName) {
            string imageLocalPath = "";
            string imageLocalPathThumbnail = "";

            restRequest = new RestRequest("collection?key="+ apiKey +"&involvedMaker="+artistName, Method.GET);
            restResponse = (RestResponse)restClient.Execute(restRequest);
            jsonObject = JObject.Parse(restResponse.Content);

            var artworkList = JsonConvert.DeserializeObject<CollectionListAPIResponse.Root>(jsonObject.ToString());
            List<ArtCollectionList> artworkParsedList = new List<ArtCollectionList>();
            for (int i = 0; i < artworkList.artObjects.Count; i++)
            {
                imageLocalPath = Directory.GetCurrentDirectory() + "\\images\\" + artworkList.artObjects[i].objectNumber + ".png";
                imageLocalPathThumbnail = Directory.GetCurrentDirectory() + "\\images\\" + artworkList.artObjects[i].objectNumber + "-thumbnail.png";
                artworkParsedList.Add(new ArtCollectionList(i+1+"",
                    artworkList.artObjects[i].objectNumber, artworkList.artObjects[i].title,
                    artworkList.artObjects[i].longTitle,
                    artworkList.artObjects[i].principalOrFirstMaker,
                    artworkList.artObjects[i].webImage.width, artworkList.artObjects[i].webImage.height,
                    artworkList.artObjects[i].webImage.url, imageLocalPath, imageLocalPathThumbnail
                    ));
            }

            return artworkParsedList;
        }

        public ArtCollectionDetail GetCollectionDetailByObjectNumber(string objectNumber)
        {
            restRequest = new RestRequest("collection/"+objectNumber+"?key=" + apiKey, Method.GET);
            restResponse = (RestResponse)restClient.Execute(restRequest);
            jsonObject = JObject.Parse(restResponse.Content);
            CollectionDetailAPIResponse.Root detail = JsonConvert.DeserializeObject<CollectionDetailAPIResponse.Root>(jsonObject.ToString());
            return new ArtCollectionDetail(detail.artObject.priref,
                detail.elapsedMilliseconds, detail.artObject.objectNumber, detail.artObject.webImage.guid,
                detail.artObject.webImage.width, detail.artObject.webImage.height,
                detail.artObject.description, detail.artObject.principalMaker,
                detail.artObject.acquisition.method, detail.artObject.acquisition.date,
                detail.artObject.longTitle
                );
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
