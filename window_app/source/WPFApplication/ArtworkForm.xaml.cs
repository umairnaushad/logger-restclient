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
        }
    }
}
