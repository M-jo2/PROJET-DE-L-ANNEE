using MyPopuStore.BU;
using MyPopuStore.DAL.DB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyPopuStore.UI.Pages.Sale_Page
{
    class SaleListViewModel : INotifyPropertyChanged
    {
        ObservableCollection<SaleUI> listAllSale;
        public ObservableCollection<SaleUI> ListAllSale
        {
            get
            {
                return listAllSale;
            }
            set
            {
                listAllSale = value;
                OnPropertyChanged();
            }
        }

        public SaleListViewModel()
        {
            
            ListAllSale = new();
            LoadListSale();
        }

        public void LoadListSale()
        {
            List<Sale> sales = SaleServices.getAllSales();
            ListAllSale.Clear();
            foreach (Sale sale in sales)
            {
                listAllSale.Add(new()
                {
                    Sale = sale,
                    QuantityOfProduct = SaleServices.getNumberProductOneSale(sale.SaleId),
                    Total = SaleServices.getTotalOneSale(sale.SaleId)
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
