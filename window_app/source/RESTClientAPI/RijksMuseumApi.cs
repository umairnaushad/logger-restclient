﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace RESTClient
{
    public class RijksMuseumApi
    {
        private readonly string apiKey="tckFvOqN";
        private readonly string uri= "https://www.rijksmuseum.nl/api/en/";
        private RestRequest restRequest;
        private RestClient restClient;
        private IRestResponse restResponse;
        private JObject jsonObject;

        public RijksMuseumApi()
        {
            restClient = new RestClient(uri);
        }

        public List<ArtCollectionList> GetCollectionsListByArtistName(string artistName, int resultsPerPage) {

            // Read data from REST endpoint and jsonify the response
            restRequest = new RestRequest("collection?key="+ apiKey +"&involvedMaker="+artistName+"&ps="+ resultsPerPage, Method.GET);
            restResponse = (RestResponse)restClient.Execute(restRequest);
            jsonObject = JObject.Parse(restResponse.Content);
            CollectionListAPIResponse .Root artworkList = JsonConvert.DeserializeObject<CollectionListAPIResponse.Root>(jsonObject.ToString());
            
            // Get only selected fields from JSON response and store them in a saparete list
            // This list will be ised to display data to UI
            List<ArtCollectionList> artworkParsedList = new List<ArtCollectionList>();
            string objectNumber = "", title = "", longTitle = "", principalOrFirstMaker = "";
            string url="", imageLocalPath="", imageLocalPathThumbnail="";
            int width = 0, height = 0;

            string folderName = Directory.GetCurrentDirectory() + "\\images\\";
            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
            }

            for (int i = 0; i < artworkList.artObjects.Count; i++)
            {
                imageLocalPath = folderName + artworkList.artObjects[i].objectNumber + ".png";
                imageLocalPathThumbnail = folderName + artworkList.artObjects[i].objectNumber + "-thumbnail.png";

                #region Check for null response form API

                if (artworkList.artObjects[i].objectNumber != null)
                    objectNumber = artworkList.artObjects[i].objectNumber;
                else
                    objectNumber = "";
                if (artworkList.artObjects[i].title != null)
                    title = artworkList.artObjects[i].title;
                else
                    title = "";
                if (artworkList.artObjects[i].longTitle != null)
                    longTitle = artworkList.artObjects[i].longTitle;
                else
                    longTitle = "";
                if (artworkList.artObjects[i].principalOrFirstMaker != null)
                    principalOrFirstMaker = artworkList.artObjects[i].principalOrFirstMaker;
                else
                    principalOrFirstMaker = "";
                if (artworkList.artObjects[i].webImage != null)
                {
                    url = artworkList.artObjects[i].webImage.url;
                    width = artworkList.artObjects[i].webImage.width;
                    height = artworkList.artObjects[i].webImage.height;
                }
                else
                {
                    url = "";
                    width = 0;
                    height = 0;
                }

                #endregion

                artworkParsedList.Add(new ArtCollectionList(i+1+"",
                    objectNumber, title, longTitle, principalOrFirstMaker, width, height,
                    url, imageLocalPath, imageLocalPathThumbnail
                    ));
            }

            return artworkParsedList;
        }

        public ArtCollectionDetail GetCollectionDetailByObjectNumber(string objectNumber)
        {
            restRequest = new RestRequest("collection/"+objectNumber+"?key=" + apiKey, Method.GET);
            restResponse = restClient.Execute(restRequest);
            CollectionDetailAPIResponse.Root detail = new CollectionDetailAPIResponse.Root();
            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                jsonObject = JObject.Parse(restResponse.Content);
                detail = JsonConvert.DeserializeObject<CollectionDetailAPIResponse.Root>(jsonObject.ToString());
                string folderName = Directory.GetCurrentDirectory() + "\\images\\";
                if (!Directory.Exists(folderName))
                {
                    Directory.CreateDirectory(folderName);
                }
                string imageLocalPath = folderName + detail.artObject.objectNumber + ".png";
                ArtCollectionDetail collectionDetail = new ArtCollectionDetail();
                using (WebClient client = new WebClient())
                {
                    #region Check for null response form API

                    if(detail.artObject != null)
                    {
                        collectionDetail.Priref = detail.artObject.priref;
                        collectionDetail.ElapsedMilliseconds = detail.elapsedMilliseconds.ToString();
                        collectionDetail.ObjectNumber = detail.artObject.objectNumber;
                        collectionDetail.Description = detail.artObject.description;
                        collectionDetail.PrincipalMaker = detail.artObject.principalMaker;
                        collectionDetail.AcquisitionDate = detail.artObject.acquisition.date.ToString();
                        collectionDetail.AcquisitionMethod = detail.artObject.acquisition.method;
                        collectionDetail.LongTitle = detail.artObject.longTitle;
                    }

                    if (detail.artObject.webImage != null)
                    {
                        collectionDetail.ImageURL = detail.artObject.webImage.url;
                        collectionDetail.Guid = detail.artObject.webImage.guid;
                        collectionDetail.Width = detail.artObject.webImage.width.ToString();
                        collectionDetail.Height = detail.artObject.webImage.height.ToString();
                        collectionDetail.ImageLocalPath = imageLocalPath;
                        if (!File.Exists(imageLocalPath))
                            client.DownloadFile(new Uri(detail.artObject.webImage.url), imageLocalPath);
                    }

                    #endregion                    
                }
                return collectionDetail;
            }

            return new ArtCollectionDetail("N/A",
                0, "N/A", "N/A", 0, 0,
                restResponse.StatusDescription, objectNumber,
                "N/A", new DateTime(), "N/A", "N/A", "N/A"
                );
        }

        public int GetArtistsCount()
        {
            restRequest = new RestRequest("collection?key=" + apiKey + "&q=artist", Method.GET);
            restResponse = (RestResponse)restClient.Execute(restRequest);
            jsonObject = JObject.Parse(restResponse.Content);
            CollectionDetailAPIResponse.Root detail = JsonConvert.DeserializeObject<CollectionDetailAPIResponse.Root>(jsonObject.ToString());
            ArtistListAPIResponse.Root artistList = JsonConvert.DeserializeObject<ArtistListAPIResponse.Root>(jsonObject.ToString());
            string[] artistName = new string[artistList.artObjects.Count];
            for (int i = 0; i < artistList.artObjects.Count; i++)
            {
                artistName[i] = artistList.artObjects[i].principalOrFirstMaker.ToString();
            }
            //File.WriteAllLines("artistsList.txt", artistName);
            Console.WriteLine("restResponse.Content = " + restResponse.Content.ToString());
            return artistList.count;
        }
    }
}
