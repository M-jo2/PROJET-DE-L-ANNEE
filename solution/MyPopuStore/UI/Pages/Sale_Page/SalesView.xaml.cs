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

namespace MyPopuStore.UI.Pages.Sale_Page
{
    /// <summary>
    /// Logique d'interaction pour SalesView.xaml
    /// </summary>
    public partial class SalesView : Page
    {
        private SaleListViewModel saleListViewModel;
        private SaleDetailViewModel saleDetailViewModel;
        private NewSaleViewModel newSaleViewModel;

        public SalesView()
        {
            InitializeComponent();
            PopulateAndBind();
        }

        private void PopulateAndBind()
        {
            saleDetailViewModel = new();
            saleListViewModel = new();
            newSaleViewModel = new();

            RightView.DataContext = newSaleViewModel;
            LeftView.DataContext = saleListViewModel;
        }

        private void NewSale_Click(object sender, RoutedEventArgs e)
        {
            if (RightView.DataContext == newSaleViewModel)
            {
                newSaleViewModel.SaveSale();
                saleListViewModel.LoadListSale();
            }
            else 
            { 
                RightView.DataContext = newSaleViewModel;
            }
        }

        private void ASaleClick(object sender, RoutedEventArgs e)
        {
            if (RightView.DataContext != saleDetailViewModel)
            {
                RightView.DataContext = saleDetailViewModel;
            }
            int saleId = ((sender as Button).DataContext as SaleUI).Sale.SaleId;
            saleDetailViewModel.SetSaleDetailUIs(saleId);
        }

        private void AddProductToSale(object sender, RoutedEventArgs e)
        {
            newSaleViewModel.AddProductToSale();
        }
    }
}
