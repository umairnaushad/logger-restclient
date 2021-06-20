using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class Author
        {
            public int Id { get; set; }
            public string ObjectNumber { get; set; }
            public string Title { get; set; }
            public string LongTitle { get; set; }
            public ImageSource Picture { get; set; }
        }
        private List<Author> LoadCollectionData()
        {
            List<Author> authors = new List<Author>();
            authors.Add(new Author()
            {
                Id = 101,
                ObjectNumber = "Umair",
                Title = "Graphics Programming ",
                LongTitle = "Graphics Programming with GDI+",
                Picture = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory+ "images\\image1.png"))
            });
            return authors;
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            dataGridView1.ItemsSource = LoadCollectionData();
        }
    }
}
