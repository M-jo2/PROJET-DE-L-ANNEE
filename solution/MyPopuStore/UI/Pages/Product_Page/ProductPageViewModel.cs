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

namespace MyPopuStore.UI.Pages.Product
{
    class ProductPageViewModel : INotifyPropertyChanged
    {

        public List<DAL.DB.Product> GetAllProducts()
        {
            return ProductServices.getAllProduct();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
