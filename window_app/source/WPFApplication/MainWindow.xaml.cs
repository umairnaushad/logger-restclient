using RESTClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace WPFApplication
{
    public partial class MainWindow : Window
    {
        private readonly int numberOfRecPerPage = 5;
        private ArtworkPagging artworkPagedTable = new ArtworkPagging();
        private IList<ArtCollectionList> artworkList = new List<ArtCollectionList>();
        private IList<ArtCollectionList> artworkListNull = new List<ArtCollectionList>();
        private RijksMuseumApi museumApi = new RijksMuseumApi();
        private int counter = 30;
        private readonly int cacheRefreshCounter = 30;
        private DispatcherTimer timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            PopulateArtistListFromFile();
            
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            counter--;
            if (counter <= 0)
            {
                counter = cacheRefreshCounter;
                timer.Stop();
                UpdateCollectionData();
                timer.Start();
            }

            int minutes = counter / 60;
            int seconds = counter % 60;
            lb_Timer.Content = "Remaining Cache Refresh Time: " + minutes + ":" + seconds;
        }


        #region Pagging

        public string PageNumberDisplay()
        {
            int firstNumber = numberOfRecPerPage * artworkPagedTable.PageIndex + 1;
            int lastNumber = numberOfRecPerPage * (artworkPagedTable.PageIndex + 1);
            if (lastNumber >= artworkList.Count)
            {
                lastNumber = artworkList.Count;
            }
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

        #endregion

        #region UI Control Events

        private async void UpdateCollectionData()
        {
            button_FetchCollectionList.IsEnabled = false;
            lb_RESTStatus.Visibility = Visibility.Visible;
            lb_RESTStatus.Foreground = new SolidColorBrush(Colors.Red);
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            string artisName = cb_ArtistName.SelectedItem.ToString();
            var items = await Task.Run(() => FetchCollectionList(artisName));
            var returnStatus = await Task.Run(() => SaveImageAsThumbnails());
            UpdateDataGridItemSource();
            button_FetchCollectionList.IsEnabled = true;
            lb_RESTStatus.Visibility = Visibility.Hidden;
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
        }

        private void button_FetchCollectionList_Click(object sender, RoutedEventArgs e)
        {
            UpdateCollectionData();
        }

        private async void dataGridView1_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int selectedIndex = dataGridView1.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex < artworkList.Count)
            {
                int index = numberOfRecPerPage * artworkPagedTable.PageIndex + selectedIndex;
                string objecNumber = artworkList[index].ObjectNumber;
                //MessageBox.Show(index + " - " + objecNumber);
                ArtCollectionDetail detail = await Task.Run(() => FetchCollectionDetail(objecNumber));
                CollectionDetailForm detailForm = new CollectionDetailForm(detail);
                detailForm.ShowDialog();
                detailForm.Close();
            }
        }

        #endregion

        #region REST API Calls

        public void UpdateDataGridItemSource()
        {
            dataGridView1.ItemsSource = artworkPagedTable.First(artworkList, numberOfRecPerPage).DefaultView;
            lb_PagrInfo.Content = PageNumberDisplay();
        }

        public IList<ArtCollectionList> FetchCollectionList(string artisName)
        {
            artworkList = museumApi.GetCollectionsListByArtistName(artisName);
            return artworkList;
        }

        public ArtCollectionDetail FetchCollectionDetail(string objectNumber)
        {
            return museumApi.GetCollectionDetailByObjectNumber(objectNumber);
        }

        #endregion

        #region Utility Functions

        private void PopulateArtistListFromFile()
        {
            string[] lineOfContents = File.ReadAllLines("artistsList.txt");
            foreach (var line in lineOfContents)
            {
                cb_ArtistName.Items.Add(line.Trim());
            }
            cb_ArtistName.SelectedIndex = 0;
        }

        private string SaveImageAsThumbnails()
        {
            Stream stream;
            Image image;
            Image thumbnail;
            using WebClient client = new WebClient();
            for (int i = 0; i < artworkList.Count; i++)
            {
                if ( String.IsNullOrEmpty(artworkList[i].ImageURL) )
                    continue;
                stream = client.OpenRead(artworkList[i].ImageURL);
                image = Image.FromStream(stream);
                System.Drawing.Size thumbnailSize = GetThumbnailSize(image);

                thumbnail = image.GetThumbnailImage(thumbnailSize.Width,
                    thumbnailSize.Height, null, IntPtr.Zero);

                if (!File.Exists(artworkList[i].ImageLocalPathThumbnail))
                {
                    thumbnail.Save(artworkList[i].ImageLocalPathThumbnail);
                }
                artworkList[i].ThumbnailImage = thumbnail;
            }

            return "Success";
        }

        private System.Drawing.Size GetThumbnailSize(Image original)
        {
            const int maxPixels = 80;
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

            return new System.Drawing.Size((int)(originalWidth * factor), (int)(originalHeight * factor));
        }

        #endregion
                
    }
}
