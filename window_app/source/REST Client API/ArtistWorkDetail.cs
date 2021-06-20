using System;
using System.Collections.Generic;
using System.Text;

namespace REST_Client_API
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class ArtistWorkDetail
    {
        public class Facet
        {
            public int key { get; set; }
            public int value { get; set; }
        }

        public class CountFacets
        {
            public int hasimage { get; set; }
            public int ondisplay { get; set; }
        }

        public class Links
        {
            public string self { get; set; }
            public string web { get; set; }
        }

        public class WebImage
        {
            public string guid { get; set; }
            public int offsetPercentageX { get; set; }
            public int offsetPercentageY { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public string url { get; set; }
        }

        public class HeaderImage
        {
            public string guid { get; set; }
            public int offsetPercentageX { get; set; }
            public int offsetPercentageY { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public string url { get; set; }
        }

        public class ArtObject
        {
            public Links links { get; set; }
            public string id { get; set; }
            public string objectNumber { get; set; }
            public string title { get; set; }
            public bool hasImage { get; set; }
            public string principalOrFirstMaker { get; set; }
            public string longTitle { get; set; }
            public bool showImage { get; set; }
            public bool permitDownload { get; set; }
            public WebImage webImage { get; set; }
            public HeaderImage headerImage { get; set; }
            public List<string> productionPlaces { get; set; }
        }

        public class Facet2
        {
            public string key { get; set; }
            public int value { get; set; }
            public List<Facet> facets { get; set; }
            public string name { get; set; }
            public int otherTerms { get; set; }
            public int prettyName { get; set; }
        }

        public class Root
        {
            public int elapsedMilliseconds { get; set; }
            public int count { get; set; }
            public CountFacets countFacets { get; set; }
            public List<ArtObject> artObjects { get; set; }
            public List<Facet> facets { get; set; }
        }
    }
}
