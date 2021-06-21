using REST_Client_API;
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

        public MainWindow()
        {
            InitializeComponent();
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            int selected_index = dataGridView1.SelectedIndex + 1;
            // this is used for debugging and testing.
            //MessageBox.Show("The index of the row for the clicked cell is " + selected_index);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //ArtworkForm win2 = new ArtworkForm(artworkList[0]);
            //win2.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //dataGridView1.ItemsSource = LoadCollectionData();
            button_FetchArtists.IsEnabled = false;
            //var mode= await FindWordCountsAsync(quantity);
            var mode = FindWordCountsAsync();
            button_FetchArtists.IsEnabled = true;
        }

        //public async Task<List<Words>> FindWordCountsAsync()
        public string FindWordCountsAsync()
        {
            RijksMuseumApi obj = new RijksMuseumApi();
            List<Artwork> artObjectList =  obj.GetArtistWorkByName("Paris-Artiste");
            dataGridView1.ItemsSource = artObjectList;
            //This is more like Task-Based Asynchronous Pattern
            return "Hi";// await Task.Run(() => words.ToList());
        }
    }
}
