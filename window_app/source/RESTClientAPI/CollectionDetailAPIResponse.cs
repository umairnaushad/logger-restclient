using System;
using System.Collections.Generic;
using System.Text;

namespace RESTClient
{
    public class CollectionDetailAPIResponse
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Links
        {
            public string search { get; set; }
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

        public class Color
        {
            public int percentage { get; set; }
            public string hex { get; set; }
        }

        public class ColorsWithNormalization
        {
            public string originalHex { get; set; }
            public string normalizedHex { get; set; }
        }

        public class NormalizedColor
        {
            public int percentage { get; set; }
            public string hex { get; set; }
        }

        public class Normalized32Colors
        {
            public int percentage { get; set; }
            public string hex { get; set; }
        }

        public class Maker
        {
            public string name { get; set; }
            public string unFixedName { get; set; }
            public string placeOfBirth { get; set; }
            public string dateOfBirth { get; set; }
            public object dateOfBirthPrecision { get; set; }
            public string dateOfDeath { get; set; }
            public object dateOfDeathPrecision { get; set; }
            public string placeOfDeath { get; set; }
            public List<string> occupation { get; set; }
            public List<string> roles { get; set; }
            public object nationality { get; set; }
            public object biography { get; set; }
            public List<object> productionPlaces { get; set; }
            public object qualification { get; set; }
        }

        public class PrincipalMaker
        {
            public string name { get; set; }
            public string unFixedName { get; set; }
            public string placeOfBirth { get; set; }
            public string dateOfBirth { get; set; }
            public object dateOfBirthPrecision { get; set; }
            public string dateOfDeath { get; set; }
            public object dateOfDeathPrecision { get; set; }
            public string placeOfDeath { get; set; }
            public List<string> occupation { get; set; }
            public List<string> roles { get; set; }
            public object nationality { get; set; }
            public object biography { get; set; }
            public List<object> productionPlaces { get; set; }
            public object qualification { get; set; }
        }

        public class Acquisition
        {
            public string method { get; set; }
            public DateTime date { get; set; }
            public string creditLine { get; set; }
        }

        public class Dating
        {
            public string presentingDate { get; set; }
            public int sortingDate { get; set; }
            public int period { get; set; }
            public int yearEarly { get; set; }
            public int yearLate { get; set; }
        }

        public class Classification
        {
            public List<string> iconClassIdentifier { get; set; }
            public List<string> iconClassDescription { get; set; }
            public List<object> motifs { get; set; }
            public List<object> events { get; set; }
            public List<object> periods { get; set; }
            public List<object> places { get; set; }
            public List<string> people { get; set; }
            public List<string> objectNumbers { get; set; }
        }

        public class Dimension
        {
            public string unit { get; set; }
            public string type { get; set; }
            public object part { get; set; }
            public string value { get; set; }
        }

        public class Label
        {
            public object title { get; set; }
            public object makerLine { get; set; }
            public object description { get; set; }
            public object notes { get; set; }
            public object date { get; set; }
        }

        public class ArtObject
        {
            public Links links { get; set; }
            public string id { get; set; }
            public string priref { get; set; }
            public string objectNumber { get; set; }
            public string language { get; set; }
            public string title { get; set; }
            public object copyrightHolder { get; set; }
            public WebImage webImage { get; set; }
            public List<Color> colors { get; set; }
            public List<ColorsWithNormalization> colorsWithNormalization { get; set; }
            public List<NormalizedColor> normalizedColors { get; set; }
            public List<Normalized32Colors> normalized32Colors { get; set; }
            public List<string> titles { get; set; }
            public string description { get; set; }
            public object labelText { get; set; }
            public List<string> objectTypes { get; set; }
            public List<string> objectCollection { get; set; }
            public List<Maker> makers { get; set; }
            public List<PrincipalMaker> principalMakers { get; set; }
            public object plaqueDescriptionDutch { get; set; }
            public object plaqueDescriptionEnglish { get; set; }
            public string principalMaker { get; set; }
            public object artistRole { get; set; }
            public List<object> associations { get; set; }
            public Acquisition acquisition { get; set; }
            public List<object> exhibitions { get; set; }
            public List<string> materials { get; set; }
            public List<string> techniques { get; set; }
            public List<object> productionPlaces { get; set; }
            public Dating dating { get; set; }
            public Classification classification { get; set; }
            public bool hasImage { get; set; }
            public List<string> historicalPersons { get; set; }
            public List<object> inscriptions { get; set; }
            public List<object> documentation { get; set; }
            public List<object> catRefRPK { get; set; }
            public string principalOrFirstMaker { get; set; }
            public List<Dimension> dimensions { get; set; }
            public List<object> physicalProperties { get; set; }
            public string physicalMedium { get; set; }
            public string longTitle { get; set; }
            public string subTitle { get; set; }
            public string scLabelLine { get; set; }
            public Label label { get; set; }
            public bool showImage { get; set; }
            public object location { get; set; }
        }

        public class AdlibOverrides
        {
            public object titel { get; set; }
            public object maker { get; set; }
            public object etiketText { get; set; }
        }

        public class ArtObjectPage
        {
            public string id { get; set; }
            public List<object> similarPages { get; set; }
            public string lang { get; set; }
            public string objectNumber { get; set; }
            public List<object> tags { get; set; }
            public object plaqueDescription { get; set; }
            public object audioFile1 { get; set; }
            public object audioFileLabel1 { get; set; }
            public object audioFileLabel2 { get; set; }
            public DateTime createdOn { get; set; }
            public DateTime updatedOn { get; set; }
            public AdlibOverrides adlibOverrides { get; set; }
        }

        public class Root
        {
            public int elapsedMilliseconds { get; set; }
            public ArtObject artObject { get; set; }
            public ArtObjectPage artObjectPage { get; set; }
        }
    }
}
