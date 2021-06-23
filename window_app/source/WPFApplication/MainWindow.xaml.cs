using RESTClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows;

namespace WPFApplication
{
    public partial class MainWindow : Window
    {
        private readonly int numberOfRecPerPage = 5;
        private ArtworkPagging artworkPagedTable = new ArtworkPagging();
        private IList<ArtCollectionList> artworkList = new List<ArtCollectionList>();
        RijksMuseumApi museumApi = new RijksMuseumApi();

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
                
        private void button_FetchArtwork_Click(object sender, RoutedEventArgs e)
        {
            button_FetchArtwork.IsEnabled = false;
            //await FetchArtworkDetail();
            FetchArtworkDetail();
            SaveImageAsThumbnails();
            button_FetchArtwork.IsEnabled = true;
        }

        private void button_ArtisList_Click(object sender, RoutedEventArgs e)
        {
            FetchArtistList();
        }

        //public async Task<List<Words>> FindWordCountsAsync()
        public string FetchArtworkDetail()
        {
            //List<Artwork> artObjectList = obj.GetArtistWorkByName("Paris-Artiste");
            artworkList = museumApi.GetCollectionsListByArtistName("Vincent van Gogh");
            //DownloadImages();
            dataGridView1.ItemsSource = artworkPagedTable.First(artworkList, numberOfRecPerPage).DefaultView;
            lb_PagrInfo.Content = PageNumberDisplay();
            //This is more like Task-Based Asynchronous Pattern
            return "Hi";// await Task.Run(() => words.ToList());
        }

        public void FetchArtistList()
        {
            museumApi.getArtistsList();
            //dataGridView1.ItemsSource = artObjectList;
        }





        public string PageNumberDisplay()
        {
            int firstNumber = numberOfRecPerPage * artworkPagedTable.PageIndex + 1;
            int lastNumber = numberOfRecPerPage * (artworkPagedTable.PageIndex + 1);
            if (lastNumber >= artworkList.Count)
            {
                lastNumber = artworkList.Count;
            }
            //This dramatically reduced the number of times I had to write this string statement
            //return "Showing " + PagedNstartumber + " of " + artworkList.Count; 
            return "" + firstNumber + " -  " + lastNumber + " (" + artworkList.Count + ")";
        }

        private void btn_Next(object sender, RoutedEventArgs e)
        {
            dataGridView1.ItemsSource = artworkPagedTable.Next(artworkList, numberOfRecPerPage).DefaultView;
            lb_PagrInfo.Content = PageNumberDisplay();
        }

        private void btn_Previous_Click(object sender, RoutedEventArgs e)
        {
            dataGridView1.ItemsSource = artworkPagedTable.Previous(artworkList, numberOfRecPerPage).DefaultView;
            lb_PagrInfo.Content = PageNumberDisplay();
        }


        private void btn_LastPage_Click(object sender, RoutedEventArgs e)
        {
            dataGridView1.ItemsSource = artworkPagedTable.Last(artworkList, numberOfRecPerPage).DefaultView;
            lb_PagrInfo.Content = PageNumberDisplay();
        }

        private void btn_FirstPage_Click(object sender, RoutedEventArgs e)
        {
            dataGridView1.ItemsSource = artworkPagedTable.First(artworkList, numberOfRecPerPage).DefaultView;
            lb_PagrInfo.Content = PageNumberDisplay();
        }



        private void SaveImageAsThumbnails()
        {
            for (int i = 0; i < artworkList.Count; i++)
            {
                Image image = Image.FromFile(artworkList[i].ImageLocalPath);

                System.Drawing.Size thumbnailSize = GetThumbnailSize(image);

                Image thumbnail = image.GetThumbnailImage(thumbnailSize.Width,
                    thumbnailSize.Height, null, IntPtr.Zero);

                thumbnail.Save(artworkList[i].ImageLocalPathThumbnail);
            }
        }



        static System.Drawing.Size GetThumbnailSize(Image original)
        {
            // Maximum size of any dimension.
            const int maxPixels = 80;

            // Width and height.
            int originalWidth = original.Width;
            int originalHeight = original.Height;

            // Compute best factor to scale entire image based on larger dimension.
            double factor;
            if (originalWidth > originalHeight)
            {
                factor = (double)maxPixels / originalWidth;
            }
            else
            {
                factor = (double)maxPixels / originalHeight;
            }

            // Return thumbnail size.
            return new System.Drawing.Size((int)(originalWidth * factor), (int)(originalHeight * factor));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ArtCollectionDetail detail = museumApi.GetCollectionDetailByObjectNumber("RP-P-OB-20.603");
            ArtworkForm win2 = new ArtworkForm(detail);
            win2.Show();
        }
    }
}
