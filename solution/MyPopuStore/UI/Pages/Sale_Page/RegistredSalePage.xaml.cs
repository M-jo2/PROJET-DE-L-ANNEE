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
    /// Logique d'interaction pour RegistredSalePage.xaml
    /// </summary>
    public partial class RegistredSalePage : Page
    {
        private SaleListViewModel saleListViewModel;
        private SaleDetailViewModel saleDetailViewModel;

        public RegistredSalePage()
        {
            InitializeComponent();
            PopulateAndBind();
        }

        private void PopulateAndBind()
        {
            saleDetailViewModel = new();
            saleListViewModel = new();

            RightView.DataContext = saleListViewModel;
            LeftView.DataContext = saleListViewModel;
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

        public void RefreshList()
        {
            saleListViewModel.LoadListSale();
        }

    }
}
