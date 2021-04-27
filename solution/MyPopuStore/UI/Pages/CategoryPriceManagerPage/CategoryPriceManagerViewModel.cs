using MyPopuStore.BU;
using MyPopuStore.DAL.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace MyPopuStore.UI.Pages.CategoryPriceManagerPage
{
    class CategoryPriceManagerViewModel :INotifyPropertyChanged
    {

        private string colorChoice ;
        private decimal priceChoice;


        private CategoryPrice categoryPriceOfProduct;

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
        public string ColorChoice
        {
            get
            {
                return colorChoice;
            }
            set
            {
                colorChoice = value;
                OnPropertyChanged();
            }
        }

        public string PriceChoice
        {
            get
            {
                return priceChoice.ToString();
            }
            set
            {
                if(value.Count() > 0)
                {
                    if (value.Last() == ',') value = value + "0";
                    if (decimal.TryParse(value, out priceChoice))
                    {
                        priceChoice = Math.Round(priceChoice, 2);
                        OnPropertyChanged();
                    }
                }
            }
        }

        public List<CategoryPrice> AllCategories
        {
            get { return CategoryPriceServices.GetAllPrice(); }
        }

        public void CreateCategoryPrice()
        {
            CategoryPriceServices cat = new CategoryPriceServices();
            CategoryPriceServices.add(colorChoice,priceChoice);
        }

        public void DeleteCategoryPrice(CategoryPrice categoryPrice)
        {
            CategoryPriceServices.delete(categoryPrice);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
