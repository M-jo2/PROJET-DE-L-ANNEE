using MyPopuStore.UI.Pages.Manage;
using MyPopuStore.UI.Pages.Product;
using MyPopuStore.UI.Pages.Sale;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace MyPopuStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
                InitializeComponent();
                ZoneContent.Content = new ManagePage();
        }

        private void MenuBar_ProductButton_Click(object sender, RoutedEventArgs e)
        {
            ZoneContent.Content = new ProductPage();
        }

        private void MenuBar_CashRegisterButton_Click(object sender, RoutedEventArgs e)
        {
            ZoneContent.Content = new SalePage();
        }

        private void MenuBar_ManageButton_Click(object sender, RoutedEventArgs e)
        {
            ZoneContent.Content = new ManagePage();
        }

        private void MenuBar_ExitButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        
    }
}
