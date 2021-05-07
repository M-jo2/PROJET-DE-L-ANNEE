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
using System.Windows;

namespace MyPopuStore.UI.Pages.Sale_Page
{

    class NewSaleViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<SaleDetailUI> SaleDetailUIs { get; set; }
        public ObservableCollection<Product> ComboBoxPropositionProducts { get; set; }

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
                if (value != null)
                {
                    ComboBoxPropositionProducts.Clear();
                    foreach (Product product in ProductServices.GetAllProduct(value)) ComboBoxPropositionProducts.Add(product);
                
                
                    if (ComboBoxPropositionProducts.Any())
                    {
                        if (ProductServices.ProductExist(value))
                        {
                            Product product = ComboBoxPropositionProducts.First();
                            NewSaleDetailPrice = CategoryPriceServices.GetPrice((int)product.CategoryPriceId).Price;
                        }
                    }
                

                    newSaleDetailCode = value;
                }
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
        public int NewSalePannelHeigh
        {
            get
            {
                return 70;
            }
        }

        public bool PaymentType { get; set; }


        public NewSaleViewModel()
        {
            SaleDetailUIs = new();
            ComboBoxPropositionProducts = new(ProductServices.GetAllProduct());
            newSaleDetailCode = "";
            newSaleDetailQuantity = 0;
            newSaleDetailPrice = 0;
        }


        public void SaveSale()
        {
            List<SaleDetail> saleDetails = new();
            foreach (SaleDetailUI saleDetailUi in SaleDetailUIs)
            {
                saleDetails.Add(saleDetailUi.SaleDetail);
            }
            try
            {
                SaleServices.NewSale(saleDetails, PaymentType);
                SaleDetailUIs.Clear();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void AddProductToSale()
        {
            Product product = ProductServices.GetProduct(NewSaleDetailCode);
            SaleDetail saleDetail = new()
            {
                NbProduct = newSaleDetailQuantity,
                Price = newSaleDetailPrice,
                ProductCode = NewSaleDetailCode
            };
            if (NewProductIsValid(saleDetail))
            {
                SaleDetailUIs.Add(new SaleDetailUI()
                {
                    Product = product,
                    SaleDetail = saleDetail
                });
            }
            else
            {
                MessageBox.Show($"Impossible de réaliser cette vente. {product.QuantityStock} {product.Label} restant en stock. ");
            }
            
        }

        private bool NewProductIsValid(SaleDetail saleDetail)
        {
            List<SaleDetail> saleDetails = new();
            foreach(SaleDetailUI saleDetailUI in SaleDetailUIs)
            {
                saleDetails.Add(saleDetailUI.SaleDetail);
            }
            saleDetails.Add(saleDetail);
            return SaleServices.EnoughProductInStock(saleDetails);
        }

        public void DeleteProductToSale(SaleDetailUI index)
        {
            SaleDetailUIs.Remove(index);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
