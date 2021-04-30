using MyPopuStore.BU;
using MyPopuStore.DAL.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyPopuStore.UI.Pages.Sale_Page
{
    
    class NewSaleViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<SaleDetailUI> listSaleDetails;
        private string newSaleDetailCode;
        private int newSaleDetailQuantity;
        private decimal newSaleDetailPrice;
        public String NewSaleDetailCode
        {
            get
            {

                return newSaleDetailCode;
            }
            set
            {
                if (ProductServices.ProductIsUsed(value))
                {
                    Product product = ProductServices.GetProduct(value);
                    NewSaleDetailPrice = CategoryPriceServices.GetPrice((int)product.CategoryPriceId).Price;
                }
                newSaleDetailCode = value;
                OnPropertyChanged();
            }
        }
        public int NewSaleDetailQuantity
        {
            get
            {
                return newSaleDetailQuantity;
            }
            set
            {
                newSaleDetailQuantity = value;
                OnPropertyChanged();
            }
        }
        public decimal NewSaleDetailPrice
        {
            get
            {
                return newSaleDetailPrice;
            }
            set
            {
                newSaleDetailPrice = value;
                OnPropertyChanged();
            }
        }
        public int NewSaleInputHeight{get => 70;}
        public ObservableCollection<SaleDetailUI> ListSaleDetails
        {
            get
            {
                return listSaleDetails;
            }
            set
            {
                listSaleDetails = value;
                OnPropertyChanged();
            }
        }
        public bool PaymentType { get; set; }


        public NewSaleViewModel()
        {
            ListSaleDetails = new();
        }


        public void SaveSale()
        {
            List<SaleDetail> saleDetails = new();
            foreach(SaleDetailUI saleDetailUi in ListSaleDetails)
            {
                saleDetails.Add(saleDetailUi.SaleDetail);
            }
            SaleServices.NewSale(saleDetails, PaymentType);
            ListSaleDetails.Clear();
        }

        public void AddProductToSale()
        {
            Product product = ProductServices.GetProduct(NewSaleDetailCode);
            SaleDetail saleDetail = new SaleDetail()
            {
                NbProduct = newSaleDetailQuantity,
                Price = newSaleDetailPrice,
                ProductCode = NewSaleDetailCode
            };
            ListSaleDetails.Add(new SaleDetailUI()
                {
                    Product = product,
                    SaleDetail = saleDetail
                }
            );
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}
