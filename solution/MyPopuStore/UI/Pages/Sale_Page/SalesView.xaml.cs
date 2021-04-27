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
        public SalesView()
        {
            InitializeComponent();
            PopulateAndBind();
        }

        private void PopulateAndBind()
        {
            saleListViewModel = new();
            GridSale.ItemsSource = saleListViewModel.GetAllSaleUI();
        }
    }
}
