using MyPopuStore.BU;
using MyPopuStore.DAL.DB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPopuStore.UI.Pages.Sale_Page
{
    class SaleListViewModel
    {
        public List<SaleUI> SaleUIs
        {
            get
            {
                List<SaleUI> saleUIs = new();
                List<Sale> sales = SaleServices.getAllSales();

                foreach (Sale sale in sales)
                {
                    saleUIs.Add(new()
                    {
                        Sale = sale,
                        QuantityOfProduct = SaleServices.getNumberProductOneSale(sale.SaleId),
                        Total = SaleServices.getTotalOneSale(sale.SaleId)
                    });
                }
                return saleUIs;
            }
        }
    }
}
