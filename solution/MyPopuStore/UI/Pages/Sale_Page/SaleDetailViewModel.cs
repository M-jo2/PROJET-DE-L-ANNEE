using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPopuStore.UI.Pages.Sale_Page
{
    class SaleDetailViewModel
    {
        public List<SaleUI> SaleDetailUIs
        {
            get
            {
                List<SaleDetailUI> saleDetailUIs = new();
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
