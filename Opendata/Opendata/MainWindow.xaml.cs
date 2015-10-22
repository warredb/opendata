using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
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
using Microsoft.Maps.MapControl.WPF;
using Newtonsoft.Json;
using HtmlAgilityPack;

namespace Opendata
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SearchListBox.Items.Add("Straat");
            SearchListBox.Items.Add("Postcode");
            SearchListBox.Items.Add("Type");
            SearchListBox.Items.Add("ID");
        }

        ObservableCollection<JSON.Datum> Glascontainers = new ObservableCollection<JSON.Datum>();
        private IEnumerable<JSON.Datum> _linqRes;
        private JSON.Rootobject data;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string url = "http://datasets.antwerpen.be/v4/gis/glascontainer.json";

            WebClient wc = new WebClient();
            string jsondata = wc.DownloadString(url);

            data = JsonConvert.DeserializeObject<JSON.Rootobject>(jsondata);
            MessageBox.Show(data.data.Count() + " gevonden");
            bool unique;

            foreach (var datum in data.data)
            {
                unique = true;
                for (int i = 0; i < Glascontainers.Count; i++)
                {
                    if (Glascontainers[i].id == datum.id && unique)
                        unique = false;
                }
                if (unique)
                {
                    datum.locatie = new Location(Convert.ToDouble(datum.point_lat.Replace(".", ",")), Convert.ToDouble(datum.point_lng.Replace(".", ",")));
                    Glascontainers.Add(datum);
                }
            }
            GlascontainerListBox.ItemsSource = data.data;

        }

        private void GlascontainerListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GlascontainerListBox.SelectedItem != null)
            {
                MapGrid.DataContext = GlascontainerListBox.SelectedItem;
                JSON.Datum t = new JSON.Datum();
                t = (JSON.Datum) GlascontainerListBox.SelectedItem;
                leMap.Center = t.locatie;
            }
        }
        
        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            
            if (SearchListBox.SelectedItem == "Straat")
            {
                var glascontainers = from glascontainer in data.data
                                     where glascontainer.straatnaam.Contains(SearchTxB.Text.ToUpper())
                                     orderby glascontainer.straatnaam
                                     select glascontainer;
                MessageBox.Show(glascontainers.Count() + " gevonden");
                GlascontainerListBox.ItemsSource = glascontainers;
            }
            if (SearchListBox.SelectedItem == "Postcode")
            {
                var glascontainers = from glascontainer in data.data
                                     where glascontainer.postcode.Contains(SearchTxB.Text)
                                     orderby glascontainer.straatnaam
                                     select glascontainer;
                MessageBox.Show(glascontainers.Count() + " gevonden");
                GlascontainerListBox.ItemsSource = glascontainers;
            }
            if (SearchListBox.SelectedItem == "Type")
            {
                var glascontainers = from glascontainer in data.data
                                     where glascontainer.type.Contains(SearchTxB.Text)
                                     orderby glascontainer.straatnaam
                                     select glascontainer;
                MessageBox.Show(glascontainers.Count() + " gevonden");
                GlascontainerListBox.ItemsSource = glascontainers;
            }
            }
            catch (Exception Exc)
            {
                MessageBox.Show(Exc.Message);
            }
        }

    }
}