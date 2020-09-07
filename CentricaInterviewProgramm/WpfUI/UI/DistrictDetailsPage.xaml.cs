using InternalAPI.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace WpfUI.UI
{
    /// <summary>
    /// Interaction logic for DistrictDetails.xaml
    /// </summary>
    public partial class DistrictDetailsPage : Page
    {
        private District _district;
        public DistrictDetailsPage()
        {
            InitializeComponent();
        }

        public void SetDistrict(District district)
        {
            _district = district;
            PopulatePage();
        }

        private void PopulatePage()
        {
            if (_district == null)
            {
                return;
            }

            districtName_lbl.Content = _district.DistrictName;
            salesPerson_lbl.Content = $"SalesPerson Count: {_district.Spwr.Count}";
            store_lbl.Content = $"Stores Count: {_district.Stores.Count}";

            PopulateGrids(_district.Stores, store_grid);
            PopulateGrids(_district.Spwr, salesPerson_grid);
        }

        private void PopulateGrids<T>(List<T> objects, Grid grid)
        {
            int initialColumn = 0;
            int row = grid.RowDefinitions.Count;
            int column = 0;

            foreach (var obj in objects)
            {
                foreach (var property in obj.GetType().GetProperties())
                {
                    string content = "";
                    var objValue = property.GetValue(obj, null);
                    var type = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                    if (type == typeof (SalesPerson))
                    {
                        foreach (var objValueProperty in objValue.GetType().GetProperties())
                        {
                            content += objValueProperty.GetValue(objValue, null).ToString() + "\n";
                        }
                    }
                    else
                    {
                        content = objValue.ToString();
                    }
                    var label = new Label();
                    label.Content = content;
                    Grid.SetColumn(label, column);
                    Grid.SetRow(label, row);
                    grid.Children.Add(label);
                    column++;
                }
                column = initialColumn;
                grid.RowDefinitions.Add(new RowDefinition());
                row++;
            }
        }


        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            // instantiate mainwindow and store it in the window variable
            var window = (MainWindow)Application.Current.MainWindow;
            var viewAllDistrictsPage = new ViewAllDistrictsPage();
            window.Content = viewAllDistrictsPage;
        }
    }
}
