using MyPopuStore.BU;
using MyPopuStore.UI.Pages.Manage;
using MyPopuStore.UI.Pages.Product_Page;
using MyPopuStore.UI.Pages.Sale_Page;
using MyPopuStore.UI.Resource;
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
            if (InfoServices.PopupStoreInfoExist())
            {
                ZoneContent.Content = new ProductPage();
            }
                
            else AlertShopNotCreated();
        }

        private void MenuBar_CashRegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (InfoServices.PopupStoreInfoExist())
            {
                ZoneContent.Content = new SalesPage();
            }
                
            else AlertShopNotCreated();
        }

        private void MenuBar_ManageButton_Click(object sender, RoutedEventArgs e)
        {
            ZoneContent.Content = new ManagePage() ;
        }

        private void MenuBar_ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void AlertShopNotCreated()
        {
            MessageBox.Show("L'accès aux produit et à la caisse  est inaccessible tant qu'un PopupStore n'est pas initialisé.");
        }

        
    }
}
