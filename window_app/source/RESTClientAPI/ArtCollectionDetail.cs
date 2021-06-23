using System;

namespace RESTClient
{
    public class ArtCollectionDetail
    {
        public string Priref { get; set; }
        public string ElapsedMilliseconds { get; set; }
        public string ObjectNumber { get; set; }
        public string Guid { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string Description { get; set; }
        public string PrincipalMaker { get; set; }
        public string AcquisitionMethod { get; set; }
        public string AcquisitionDate { get; set; }
        public string LongTitle { get; set; }
        public ArtCollectionDetail(string priref, int elapsedMilliseconds, string objectNumber, string guid, int width,
            int height, string description, string principalMaker, string acquisitionMethod,
            DateTime acquisitionDate, string longTitle)
        {
            this.Priref = priref;
            this.ElapsedMilliseconds = elapsedMilliseconds.ToString();
            this.ObjectNumber = objectNumber;
            this.Guid = guid;
            this.Width = width.ToString();
            this.Height = height.ToString();
            this.Description = description;
            this.PrincipalMaker = principalMaker;
            this.AcquisitionMethod = acquisitionMethod;
            this.AcquisitionDate = acquisitionDate.ToString();
            this.LongTitle = longTitle;
        }
    }
}
