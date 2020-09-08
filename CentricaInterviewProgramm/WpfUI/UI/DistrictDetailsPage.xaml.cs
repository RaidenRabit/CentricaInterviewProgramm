using InternalAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfUI.Controllers;

namespace WpfUI.UI
{
    /// <summary>
    /// Interaction logic for DistrictDetails.xaml
    /// </summary>
    public partial class DistrictDetailsPage : Page
    {
        private District _district;
        private DistrictService _districtService;
        public DistrictDetailsPage()
        {
            InitializeComponent();
            _districtService = new DistrictService();
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

            PopulateGrids(_district.Stores, store_grid, false);
            _district.Spwr.ForEach(sp => sp.SalesPerson.Stores = null);
            PopulateGrids(_district.Spwr, salesPerson_grid, true);
        }

        private void PopulateGrids<T>(List<T> objects, Grid grid, bool makeCheckBoxes)
        {
            int initialColumn = 0;
            int row = grid.RowDefinitions.Count;
            int column = 0;
            int id = 0;

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
                            var objValuePropertyValue = objValueProperty.GetValue(objValue, null);
                            if (!CheckForDefaultOrEmpty(objValuePropertyValue))
                            {
                                content += objValuePropertyValue.ToString() + "\n";
                                if (makeCheckBoxes && id == 0)
                                {
                                    Int32.TryParse(objValuePropertyValue.ToString(), out id);
                                }
                            }
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
                if (makeCheckBoxes)
                {
                    var checkBox = new CheckBox();
                    checkBox.IsChecked = false;
                    checkBox.Name = "chckBox" + id.ToString();
                    Grid.SetColumn(checkBox, column);
                    Grid.SetRow(checkBox, row);
                    grid.Children.Add(checkBox);
                }
                column = initialColumn;
                grid.RowDefinitions.Add(new RowDefinition());
                row++;
                id = 0;
            }
        }

        private bool CheckForDefaultOrEmpty<T>(T myObject)
        {
            if (EqualityComparer<T>.Default.Equals(myObject, default(T)))
            {
                return true;
            }
            return false;
        }


        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            // instantiate mainwindow and store it in the window variable
            var window = (MainWindow)Application.Current.MainWindow;
            var viewAllDistrictsPage = new ViewAllDistrictsPage();
            window.Content = viewAllDistrictsPage;
        }

        private void addSalesPersonToDistrict_btn_Click(object sender, RoutedEventArgs e)
        {
            if (addSalesPersonToDistrict_grid.Visibility == Visibility.Visible)
            {
                addSalesPersonToDistrict_grid.Visibility = Visibility.Hidden;
            }
            else
            {
                addSalesPersonToDistrict_grid.Visibility = Visibility.Visible;
            }
        }

        private async void FinalizeAdd_btn_Click(object sender, RoutedEventArgs e)
        {

            addSalesPersonError_lbl.Content = "";
            if (!Int32.TryParse(salesPersonId_txtblk.Text, out int salesPersonId))
            {
                addSalesPersonError_lbl.Content = "Only Integer SalesPersonId's Please";
                return;
            }
            if (!Int32.TryParse(relationTypeId_txtblk.Text, out int relationTypeId))
            {
                addSalesPersonError_lbl.Content = "Only integer RelationTypeId's, please";
                return;
            }
            var response = await _districtService.CreateSalesPersonToDistrict(_district.DistrictId, salesPersonId, relationTypeId);
            if (response)
            {
                addSalesPersonToDistrict_grid.Visibility = Visibility.Hidden;
            }
            addSalesPersonError_lbl.Content = "Something went wrong";
        }

        private async void removeSalesPerson_btn_Click(object sender, RoutedEventArgs e)
        {
            var salesPersonIdList = new List<int>();
            var checkBoxList = this.salesPerson_grid.Children.OfType<CheckBox>().Where(x => x.IsChecked == true);
            foreach (var checkBox in checkBoxList)
            {
                var id = new String(checkBox.Name.Where(Char.IsDigit).ToArray());
                int salesPersonId = Int32.Parse(id);
                salesPersonIdList.Add(salesPersonId);
            }
            var response = await _districtService.RemoveSalesPersonsFromDistrict(salesPersonIdList, _district.DistrictId);
            if (response)
            {
                removeSalesPerson_lbl.Foreground = Brushes.Green;
                removeSalesPerson_lbl.Content = "Deleted";
            }
            else
            {
                removeSalesPerson_lbl.Foreground = Brushes.Red;
                removeSalesPerson_lbl.Content = "Something went wrong";
            }
        }
    }
}
