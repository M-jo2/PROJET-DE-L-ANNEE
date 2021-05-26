using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
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
using System.Windows;

namespace MyPopuStore.UI.Pages.Product_Page
{
    class AddProductViewModel : INotifyPropertyChanged
    {
        private const int MaxCharInCode = 3;

        private string pictureProduct = "https://png.pngtree.com/element_our/20190601/ourmid/pngtree-gray-cross-icon-free-illustration-image_1338616.jpg";
        private CategoryPrice categoryPriceOfProduct = new CategoryPrice() { Color = "Gray", Price = 0 };
        private string code;
        private string label;
        private int quantityStock;


        public CategoryPrice CategoryPriceOfProduct
        {
            get
            {
                return categoryPriceOfProduct;
            }
            set
            {
                categoryPriceOfProduct = value;
                OnPropertyChanged();
            }
        }

        public string Code
        {
            get
            {
                return code;
            }
            set
            {
                if (value.Length > MaxCharInCode)
                {
                    value = code.Substring(0, MaxCharInCode);
                }
                code = value;
                OnPropertyChanged();
            }
        }
        public string Label
        {
            get
            {
                return label;
            }
            set
            {
                label = value;
                OnPropertyChanged();
            }
        }

        public string PictureProduct
        {
            get { return pictureProduct; }
            set
            {
                pictureProduct = value;
                OnPropertyChanged();
            }
        }

        public int QuantityStock
        {
            get { return quantityStock; }
            set
            {
                quantityStock = value;
                OnPropertyChanged();
            }
        }

        public AddProductViewModel()
        {
            
        }

        public void SetPictureProduct()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
                PictureProduct = openFileDialog.FileName;
        }
        public void CreateProduct()
        {
            try
            {
                ProductServices.AddProduct(Code, Label, CategoryPriceOfProduct, QuantityStock, PictureProduct);
            }catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void SetCategoriesPrices()
        {
            CategoryPriceManager cat = new();
            if (cat.ShowDialog() == true)
            {
                CategoryPriceOfProduct = cat.ChoiceCat;
            }
        }



















        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
