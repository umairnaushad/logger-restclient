using RESTClient;
using System.Windows;

namespace WPFApplication
{
    /// <summary>
    /// Interaction logic for ArtworkForm.xaml
    /// </summary>
    public partial class ArtworkForm : Window
    {
        public ArtworkForm()
        {
            InitializeComponent();
        }
        
        public ArtworkForm(ArtCollectionDetail artwork):this()
        {
            DataContext = artwork;
            //label_ElapsedMilliseconds.Content = artwork.ElapsedMilliseconds;
            label_ObjectNumber.Content = artwork.ObjectNumber;
            label_LongTitle.Content = artwork.LongTitle;
            label_PrincipalMaker.Content = artwork.PrincipalMaker;
            label_AcquisitionDate.Content = artwork.AcquisitionDate;
            label_AcquisitionMethod.Content = artwork.AcquisitionMethod;
            label_Guid.Content = artwork.Guid;
            label_Width.Content = artwork.Width;
            label_Height.Content = artwork.Height;
            label_Description.Content = artwork.Description;
        }
    }
}
