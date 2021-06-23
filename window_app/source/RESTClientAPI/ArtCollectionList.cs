using System.Drawing;

namespace RESTClient
{
    public class ArtCollectionList
    {
        public string Id { get; set; }
        public string ObjectNumber { get; set; }
        public string Title { get; set; }
        public string LongTitle { get; set; }
        public string PrincipalMakers { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string ImageURL { get; set; }
        public string ImageLocalPath { get; set; }
        public string ImageLocalPathThumbnail { get; set; }
        public Image ThumbnailImage { get; set; }
        public ArtCollectionList(string id, string objectNumber, string title, string longTitle,
            string principalMakers, int width, int height, string imageURL,
            string imageLocalPath, string imageLocalPathThumbnail)
        {
            this.Id = id;
            this.ObjectNumber = objectNumber;
            this.Title = title;
            this.LongTitle = longTitle;
            this.PrincipalMakers = principalMakers;
            this.Width = width;
            this.Height = height;
            this.ImageURL = imageURL;
            this.ImageLocalPath = imageLocalPath;
            this.ImageLocalPathThumbnail = imageLocalPathThumbnail;
        }
    }
}
