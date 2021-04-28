using MyPopuStore.BU;
using MyPopuStore.DAL.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPopuStore.UI.Pages.Sale_Page
{
    class SaleDetailViewModel
    {
        public List<SaleDetailUI> GetSaleDetailUIs(int SaleId)
        {
            
            List<SaleDetailUI> saleDetailUIs = new();
            List<SaleDetail> saleDetails = SaleServices.getSaleDetailsOneSale(SaleId);

            foreach (SaleDetail saleDetail in saleDetails)
            {
                saleDetailUIs.Add(new()
                {
                    Product = ProductServices.getProduct(saleDetail.ProductCode),
                    SaleDetail = saleDetail
                }) ;
            }
            return saleDetailUIs;
            
        }
    }
}
