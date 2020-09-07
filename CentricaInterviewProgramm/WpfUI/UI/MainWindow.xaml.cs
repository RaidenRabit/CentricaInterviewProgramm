using InternalAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfUI.Controllers;

namespace WpfUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DistrictService _districtService;
        private string _originalDistrictCountText;

        public MainWindow()
        {
            InitializeComponent();
            _districtService = new DistrictService();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(_originalDistrictCountText))
            {
                _originalDistrictCountText = districtCount_lbl.Text;
            }
            var districtTotalCount =  await _districtService.GetDistrictCount();
            districtCount_lbl.Text = _originalDistrictCountText + districtTotalCount.ToString();

            var districts = await _districtService.GetAllDistricts();
            if (districts != null)
            {
                CreateDistrictButtons(districts);
            }
        }

        private void SpecificDistrictBtn_Clicked(object sender, EventArgs e)
        {
            var button = (Button) sender;
            districtCount_lbl.Text = button.Content + " got clicked";
            this.NavigationService.Navigate(new Uri("Pages/Page2.xaml", UriKind.Relative));
        }

        private void CreateDistrictButtons(List<District> districts)
        {
            int row = gridMain.ColumnDefinitions.Count;
            int column = gridMain.RowDefinitions.Count;

            gridMain.ColumnDefinitions.Add(new ColumnDefinition());
            gridMain.ColumnDefinitions.Add(new ColumnDefinition());
            gridMain.ColumnDefinitions.Add(new ColumnDefinition());
            gridMain.ColumnDefinitions.Add(new ColumnDefinition());

            foreach (var district in districts)
            {
                Button button = new Button();
                button.Click += (s, e) => SpecificDistrictBtn_Clicked(s, e);
                string btnContent = $"{district.DistrictName} \nSalesPersons: {district.Spwr.Count} \nStores:{district.Stores.Count}";
                button.Content = district.DistrictName;
                button.Background = Brushes.LightGray;
                if (!district.Spwr.Any())
                {
                    button.Background = Brushes.Gray;
                }
                Grid.SetColumn(button, column);
                Grid.SetRow(button, row);
                gridMain.Children.Add(button);
                

                column++;
                if (column == 5)
                {
                    column = 1;
                    gridMain.RowDefinitions.Add(new RowDefinition());
                    row++;
                }
            }
        }
    }
        
}