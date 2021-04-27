using Microsoft.Win32;
using MyPopuStore.UI.Pages.CategoryPriceManagerPage;
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

namespace MyPopuStore.UI.Pages.Product
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
            ListOfAllProducts.ItemsSource = productPageViewModel.GetAllProducts();
        }

        private void AddProductButton(object sender, RoutedEventArgs e)
        {
            addProductViewModel.createProduct();
        }

        private void UICatPrice_Click(object sender, RoutedEventArgs e)
        {
            addProductViewModel.setCategoryPrice();
        }

        private void UISearchPicture_Click(object sender, RoutedEventArgs e)
        {
            addProductViewModel.SetPictureProduct();
        }
    }
}
