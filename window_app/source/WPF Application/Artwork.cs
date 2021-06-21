using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace WPFApplication
{
    public class Artwork2
    {
        public string Id { get; set; }
        public string ObjectNumber { get; set; }
        public string Title { get; set; }
        public string LongTitle { get; set; }
        public string ProductionPlaces { get; set; }
        public string PresentingDate { get; set; }
        public string Width { get; set; }
        public string Height{ get; set; }
        public ImageSource Picture { get; set; }
    }
}
