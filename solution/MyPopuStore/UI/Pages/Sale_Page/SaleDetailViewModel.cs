using MyPopuStore.BU;
using MyPopuStore.DAL.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyPopuStore.UI.Pages.Sale_Page
{
    class SaleDetailViewModel : INotifyPropertyChanged
    {
        private List<SaleDetailUI> listSaleDetails;
        public int NewSalePannelHeigh
        {
            get
            {
                return 0;
            }
        }
        public List<SaleDetailUI> ListSaleDetails
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

        public void SetSaleDetailUIs(int SaleId)
        {
            List<SaleDetailUI> saleDetailUIs = new();
            List<SaleDetail> saleDetails = SaleServices.getSaleDetailsOneSale(SaleId);

            foreach (SaleDetail saleDetail in saleDetails)
            {
                saleDetailUIs.Add(new()
                {
                    Product = ProductServices.GetProduct(saleDetail.ProductCode),
                    SaleDetail = saleDetail
                }) ;
            }
            ListSaleDetails =  saleDetailUIs;
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
