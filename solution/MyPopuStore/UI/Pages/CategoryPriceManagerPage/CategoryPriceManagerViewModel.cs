using MyPopuStore.BU;
using MyPopuStore.DAL.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace MyPopuStore.UI.Pages.CategoryPriceManagerPage
{
    class CategoryPriceManagerViewModel : INotifyPropertyChanged
    {
        private CategoryPrice categoryPriceOfProduct;
        public ObservableCollection<CategoryPrice> ListCategories { get; set; }

        public CategoryPrice CategoryPriceOfProduct
        {
            get
            {
                return categoryPriceOfProduct;
            }
            set
            {
                categoryPriceOfProduct = value;
            }
        }

        public CategoryPriceManagerViewModel()
        {
            categoryPriceOfProduct = new();
            ListCategories = new();
            LoadCategories();
        }

        public void LoadCategories()
        {
            List<CategoryPrice> categoryPrices = CategoryPriceServices.GetAllPrice();
            ListCategories.Clear();
            foreach (CategoryPrice categoryPrice in categoryPrices)
            {
                ListCategories.Add(categoryPrice);
            }
        }

        public void CreateCategoryPrice()
        {
            CategoryPriceServices.Add(CategoryPriceOfProduct.Color, CategoryPriceOfProduct.Price);
            LoadCategories();
        }

        public void DeleteCategoryPrice(CategoryPrice categoryPrice)
        {
            try
            {
                CategoryPriceServices.Delete(categoryPrice);
                LoadCategories();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
