using RESTClient;
using System.Windows;

namespace WPFApplication
{
    /// <summary>
    /// Interaction logic for ArtworkForm.xaml
    /// </summary>
    public partial class CollectionDetailForm : Window
    {
        public CollectionDetailForm()
        {
            InitializeComponent();
        }
        
        public CollectionDetailForm(ArtCollectionDetail artwork):this()
        {
            DataContext = artwork;
        }
    }
}
