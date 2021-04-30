using Microsoft.Win32;
using MyPopuStore.DAL.DB;
using MyPopuStore.UI.Pages.CategoryPriceManagerPage;
using MyPopuStore.UI.Pages.Product_Page;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace MyPopuStore.UI.Pages.Product_Page
{
    /// <summary>
    /// Logique d'interaction pour ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        ProductPageViewModel productPageViewModel;
        AddProductViewModel addProductViewModel;
        public ProductPage()
        {
            InitializeComponent();
            PopulateAndBind();
        }

        private void PopulateAndBind()
        {
            productPageViewModel = new ProductPageViewModel();
            addProductViewModel = new AddProductViewModel();

            AddProductPart.DataContext = addProductViewModel;
            ListProductPart.DataContext = productPageViewModel;
        }

        private void AddProductButton(object sender, RoutedEventArgs e)
        {
            addProductViewModel.CreateProduct();
            productPageViewModel.RefreshProductList();
        }

        private void CatPrice_Click(object sender, RoutedEventArgs e)
        {
            addProductViewModel.SetCategoriesPrices();
        }

        private void SearchPicture_Click(object sender, RoutedEventArgs e)
        {
            addProductViewModel.SetPictureProduct();
        }

        private void SetPicture_Click(object sender, RoutedEventArgs e)
        {
            ProductUI product = (ProductUI)((Button)sender).DataContext;
            productPageViewModel.SetPictureProduct(product);
        }

        private void SetCatPrice_Click(object sender, RoutedEventArgs e)
        {
            ProductUI product = (((sender as Button).Parent as Grid).DataContext as ProductUI) ;
            productPageViewModel.SetCategoriesPrice(product);
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            productPageViewModel.DeleteProduct(((ProductUI)((Button)sender).DataContext).Product.Code);
        }
    }
}
