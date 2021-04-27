using MyPopuStore.BU;
using MyPopuStore.DAL.DB;
using MyPopuStore.UI.Pages.CategoryPriceManagerPage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MyPopuStore.UI.Pages.Product_Page
{
    class ProductPageViewModel : INotifyPropertyChanged
    {
        public List<ProductUI> GetAllProducts()
        {
            List<ProductUI> productsUI = new();
            List<DAL.DB.Product> products = ProductServices.getAllProduct();

            foreach (DAL.DB.Product product in products)
            {
                int CatId = product.CategoryPriceId != null ? (int)product.CategoryPriceId : 0;

                productsUI.Add(new ProductUI
                {
                    Product = product,
                    CategoryPrice = CategoryPriceServices.GetPrice(CatId)
                });
            }

            return productsUI;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
