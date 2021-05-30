using Microsoft.Win32;
using MyPopuStore.BU;
using MyPopuStore.DAL.DB;
using MyPopuStore.UI.Pages.CategoryPriceManagerPage;
using MyPopuStore.UI.Pages.Product_Page.Image_Manager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MyPopuStore.UI.Pages.Product_Page
{
    class ProductPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ProductUI> listProduct;

        public ObservableCollection<ProductUI> ListProduct {
            get { return listProduct; }
        }
        public ProductPageViewModel()
        {
            listProduct = new();
            RefreshProductList();
        }
        public void RefreshProductList()
        {
            List<Product> products = ProductServices.GetAllProduct();
            ListProduct.Clear();

            foreach (Product product in products)
            {
                int CatId = product.CategoryPriceId != null ? (int)product.CategoryPriceId : 0;

                ListProduct.Add(new ProductUI
                {
                    Product = product,
                    CategoryPrice = CategoryPriceServices.GetPrice(CatId)
                });
            }
            
        }

        public void DeleteProduct(string code)
        {
            try
            {
                ProductServices.RemoveProduct(code);
                RefreshProductList();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void SetCategoriesPrice(Product product)
        {
            CategoryPriceManager cat = new();
            if (cat.ShowDialog() == true)
            {
                product.CategoryPriceId = cat.ChoiceCat.CategoryPriceId;
            }
            UpdateProduct(product);
        }
        public void SetPictureProduct(Product product)
        {
            ImageManagerView imageManagerView = new();
            if(imageManagerView.ShowDialog() == true)
            {
                product.Picture = imageManagerView.PathFile;
            }
            UpdateProduct(product);
        }

        public void UpdateProduct(Product product)
        {
            try
            {
                ProductServices.UpdateProduct(product);
            }
            catch(Exception e)
            {
                MessageBox.Show("Update impossible : \n"+e.Message);
            }
            RefreshProductList();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
