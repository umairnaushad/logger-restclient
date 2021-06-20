using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WPFApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Artwork> artworkList = new List<Artwork>();

        private List<Artwork> LoadCollectionData()
        {            
            artworkList.Add(new Artwork()
            {
                Id = "nl-BI-H-4615",
                ObjectNumber = "BI-H-4615",
                Title = "Paris-Artiste",
                LongTitle = "Paris-artiste, Paris-Artiste, ca. 1883",
                ProductionPlaces = "Parijs",
                PresentingDate = "ca. 1883",
                Width = "1957",
                Height = "2500",
                Picture = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory+ "images\\image1.png"))
            });
            artworkList.Add(new Artwork()
            {
                Id = "102",
                ObjectNumber = "BI-H-4615",
                Title = "Paris-Artiste",
                LongTitle = "Paris-artiste, Paris-Artiste, ca. 1883",
                ProductionPlaces = "Parijs",
                PresentingDate = "ca. 1883",
                Width = "1957",
                Height = "2500",
                Picture = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images\\image1.png"))
            });
            return artworkList;
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dataGridView1.ItemsSource = LoadCollectionData();
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            int selected_index = dataGridView1.SelectedIndex + 1;
            // this is used for debugging and testing.
            //MessageBox.Show("The index of the row for the clicked cell is " + selected_index);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ArtworkForm win2 = new ArtworkForm(artworkList[0]);
            win2.Show();
        }
    }
}
