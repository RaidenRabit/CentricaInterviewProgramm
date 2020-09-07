using System.Windows;
using WpfUI.UI;

namespace WpfUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            var viewAllDistrictsPage = new ViewAllDistrictsPage();
            this.Content = viewAllDistrictsPage;
        }

    }
        
}