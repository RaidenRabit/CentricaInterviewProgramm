using InternalAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using WpfUI.Controllers;

namespace WpfUI.UI
{
    /// <summary>
    /// Interaction logic for ViewAllDistrictsPage.xaml
    /// </summary>
    public partial class ViewAllDistrictsPage : Page
    {
        private DistrictService _districtService;
        private string _originalDistrictCountText;
        private List<District> _districts;
        public ViewAllDistrictsPage()
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
            var districtTotalCount = await _districtService.GetDistrictCount();
            districtCount_lbl.Text = _originalDistrictCountText + districtTotalCount.ToString();

            var districts = await _districtService.GetAllDistricts();
            if (districts != null)
            {
                _districts = districts;
                CreateDistrictButtons(districts);
            }
        }

        private void SpecificDistrictBtn_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            districtCount_lbl.Text = button.Content + " got clicked";    
            // instantiate mainwindow and store it in the window variable
            var window = (MainWindow)Application.Current.MainWindow;
            var pg1 = new DistrictDetailsPage();
            var districtId = new String(button.Name.Where(Char.IsDigit).ToArray());
            pg1.SetDistrict(_districts.FirstOrDefault(e => e.DistrictId.ToString().Equals(districtId)));
            window.Content = pg1;
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
                button.Name = "btn" + district.DistrictId.ToString();
                button.Click += (s, e) => SpecificDistrictBtn_Clicked(s, e);
                string btnContent = $"{district.DistrictName} \nSalesPersons: {district.Spwr.Count} \nStores:{district.Stores.Count}";
                button.Content = btnContent;
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
