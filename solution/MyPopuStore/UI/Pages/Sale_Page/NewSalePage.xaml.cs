using MyPopuStore.UI.Resource.User_Control;
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
    /// Logique d'interaction pour NewSalePage.xaml
    /// </summary>
    public partial class NewSalePage : Page
    {
        private NewSaleViewModel newSaleViewModel;

        public event EventHandler SaleSaved;

        public NewSalePage()
        {
            InitializeComponent();
            PopulateAndBind();
        }

        private void PopulateAndBind()
        {
            newSaleViewModel = new();

            this.DataContext = newSaleViewModel;
        }


        private void AddProductToSale(object sender, RoutedEventArgs e)
        {
            newSaleViewModel.AddProductToSale();
        }

        private void SaveSale(object sender, RoutedEventArgs e)
        {
            newSaleViewModel.SaveSale();
            SaleSaved(this, new EventArgs());
        }

        private void DeleteSaleDetailButtonClick(object sender, RoutedEventArgs e)
        {
            SaleDetailUI index = ((sender as SaleDetailControl).DataContext as SaleDetailUI);
            newSaleViewModel.DeleteProductToSale(index);
        }
    }
}
