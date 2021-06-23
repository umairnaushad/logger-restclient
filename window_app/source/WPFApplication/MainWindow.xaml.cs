﻿using RESTClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows;

namespace WPFApplication
{
    public partial class MainWindow : Window
    {
        private readonly int numberOfRecPerPage = 5;
        private ArtworkPagging artworkPagedTable = new ArtworkPagging();
        private IList<ArtCollectionList> artworkList = new List<ArtCollectionList>();
        private IList<ArtCollectionList> artworkListNull = new List<ArtCollectionList>();
        private RijksMuseumApi museumApi = new RijksMuseumApi();

        public MainWindow()
        {
            InitializeComponent();
            PopulateArtistListFromFile();
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            
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

        #region UI Controls

        private void button_FetchCollectionList_Click(object sender, RoutedEventArgs e)
        {
            button_FetchCollectionList.IsEnabled = false;
            //ClearImages();
            //await FetchArtworkDetail();
            FetchCollectionList();
            SaveImageAsThumbnails();
            button_FetchCollectionList.IsEnabled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            ClearImages();
        }

        private void ClearImages()
        {
            dataGridView1.ItemsSource = artworkListNull;
            dataGridView1.ItemsSource = null;
            dataGridView1.Items.Refresh();
            dataGridView1.Items.Refresh();
        }

        #endregion

        #region REST API Calls

        //public async Task<List<Words>> FindWordCountsAsync()
        public void FetchCollectionList()
        {
            artworkList = museumApi.GetCollectionsListByArtistName(cb_ArtistName.SelectedItem.ToString());
            dataGridView1.ItemsSource = artworkPagedTable.First(artworkList, numberOfRecPerPage).DefaultView;
            lb_PagrInfo.Content = PageNumberDisplay();
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

        private void SaveImageAsThumbnails()
        {
            Stream stream;
            Image image;
            Image thumbnail;
            using WebClient client = new WebClient();
            for (int i = 0; i < artworkList.Count; i++)
            {
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








        private void SaveImageAsThumbnails_2()
        {
            using (WebClient client = new WebClient())
            {
                for (int i = 0; i < artworkList.Count; i++)
                {
                    Stream stream = client.OpenRead(artworkList[i].ImageURL);
                    Bitmap original;
                    original = new Bitmap(stream);
                    Bitmap resized = new Bitmap(original, new System.Drawing.Size(original.Width / 4, original.Height / 4));
                    resized.Save(artworkList[i].ImageLocalPathThumbnail);
                }
            }
        }
                
        private Image GetCopyImage(string path)
        {
            using (Image im = Image.FromFile(path))
            {
                Bitmap bm = new Bitmap(im);
                return bm;
            }
        }

        private void dataGridView1_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int selectedIndex = dataGridView1.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex < artworkList.Count)
            {
                string objecNumber = artworkList[selectedIndex].ObjectNumber;
                MessageBox.Show(selectedIndex + " - " + objecNumber);
                //ArtCollectionDetail detail = museumApi.GetCollectionDetailByObjectNumber(objecNumber);
                //ArtworkForm win2 = new ArtworkForm(detail);
                //win2.ShowDialog();
            }
        }
    }
}
