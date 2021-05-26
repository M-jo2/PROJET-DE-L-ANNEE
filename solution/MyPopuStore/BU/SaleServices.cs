using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPopuStore.BU
{
    using DAL.DB;
    using System.Runtime.InteropServices;

    class SaleServices
    {
        /// <summary>
        /// Retourne toute les ventes
        /// </summary>
        /// <returns> <c>List<Sale></c> contenant toute les ventes réalisée.</returns>
        public static List<Sale> getAllSales()
        {
            
            using (MyPopupStoreDBContext db = new())
            {
                return db.Sales.ToList();
            }
        }

        /// <summary>
        /// Nombre de produit contenu dans une seule vente.
        /// </summary>
        /// <param name="SaleID">Identifiant de la vente.</param>
        /// <returns><c>Int</c> représentant le nombre de produit.</returns>
        public static int getNumberProductOneSale(int SaleID)
        {
            using (MyPopupStoreDBContext db = new())
            {
                int? quantity = 0;
                quantity = db.SaleDetails.Where(e => e.SaleId == SaleID).Sum(e=>e.NbProduct);
                if (quantity == null) throw new Exception("Quantité de produit null");
                return (int)quantity;
            }
        }

        /// <summary>
        /// Recette d'une vente.
        /// </summary>
        /// <param name="SaleID">Identifiant de la vente.</param>
        /// <returns><c>decimal</c> représentant le prix de la vente.</returns>
        public static decimal getTotalOneSale(int SaleID)
        {
            using (MyPopupStoreDBContext db = new())
            {
                decimal? total = 0;
                total = db.SaleDetails.Where(e => e.SaleId == SaleID).Sum(e => e.Price*e.NbProduct);
                return (decimal)total;
            }
        }

        /// <summary>
        /// Permet d'obtenir des informations sur les produits d'une ventes
        /// </summary>
        /// <param name="SaleID">Identifiant de la vente.</param>
        /// <returns>Liste des détails de la ventes</returns>
        public static List<SaleDetail> getSaleDetailsOneSale(int SaleID)
        {
            using (MyPopupStoreDBContext db = new())
            {
                List<SaleDetail> saleDetails = db.SaleDetails.Where(e => e.SaleId == SaleID).ToList();
                return saleDetails;
            }
        }

        /// <summary>
        /// Détermine si le stock est suffisant pour une <c>List<SaleDetail></c> donnée
        /// </summary>
        /// <param name="saleDetails"></param>
        /// <returns>True si le stock est suffisant pour TOUT les produits de <c>List<SaleDetail></c></returns>
        public static bool EnoughProductInStock(List<SaleDetail> saleDetails)
        {
            Dictionary<string,int> codesQuantities = new Dictionary<string, int>();
            foreach (SaleDetail saleDetail in saleDetails)
            {
                if (codesQuantities.ContainsKey(saleDetail.ProductCode)) codesQuantities[saleDetail.ProductCode] += (int)saleDetail.NbProduct;
                else codesQuantities.Add(saleDetail.ProductCode, (int)saleDetail.NbProduct);
            }
            foreach (KeyValuePair<string,int> codeQuantity in codesQuantities)
            {
                Product product = ProductServices.GetProduct(codeQuantity.Key);
                if (product.QuantityStock - codeQuantity.Value < 0)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Crée et stocke une vente dans la DB.
        /// </summary>
        /// <param name="saleDetails"></param>
        /// <param name="paymentType">True = Carte bancaire   false = monnaie</param>
        /// <param name="decrementQuantityStock"> est ce que le stock doit être débité à l'issue de la vente. </param>
        public static void NewSale(List<SaleDetail> saleDetails,bool paymentType,bool decrementQuantityStock=true)
        {
            if(decrementQuantityStock && !EnoughProductInStock(saleDetails))
                throw new Exception("Produit manquant dans le stock.");

            using (MyPopupStoreDBContext db = new())
            {
                
                Sale sale = new()
                {
                    PaymentType = paymentType,
                    Date = DateTime.Now
                };
                sale.SaleDetails = saleDetails;
                
                db.Sales.Add(sale);
                db.SaveChanges();
            }

            if (decrementQuantityStock)
            {
                foreach (SaleDetail saleDetail in saleDetails)
                {
                    Product product = ProductServices.GetProduct(saleDetail.ProductCode);
                    product.QuantityStock -= saleDetail.NbProduct;
                    ProductServices.UpdateProduct(product);
                }
            }
        }

        /// <summary>
        /// nombre de pièce vendue d'un seul produit entre deux dates donnée.
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns>Nombre de pièce vendue.</returns>
        public static int QuantitySoldOfAProduct(string Code,DateTime start, DateTime end)
        {
            using(MyPopupStoreDBContext db= new())
            {
                return db.SaleDetails.Where(e=> e.ProductCode==Code && e.Sale.Date>start && e.Sale.Date<end).Sum(e => e.NbProduct) ?? default(int);
            }
        }

        /// <summary>
        /// Recette du magasin dans un intervalle de temps. 
        /// </summary>
        /// <param name="start">Date de début</param>
        /// <param name="end">Date de fin</param>
        /// <returns></returns>
        public static decimal GetTotal(DateTime start, DateTime end)
        {
            using (MyPopupStoreDBContext db = new())
            {
                return db.SaleDetails.Where(e => e.Sale.Date > start && e.Sale.Date < end).Sum(e => e.Price*e.NbProduct)??default(decimal);
            }
        }

        /// <summary>
        /// Efface toute les ventes et leurs détails de la DB
        /// </summary>
        public static void DeleteSaleAndSaleDetails()
        {
            using (MyPopupStoreDBContext db = new())
            {
                List<Sale> sales = getAllSales();
                foreach(Sale sale in sales)
                {
                    List<SaleDetail> saleDetails = getSaleDetailsOneSale(sale.SaleId);
                    foreach(SaleDetail saleDetail in saleDetails)
                    {
                        db.SaleDetails.Remove(saleDetail);
                    }
                    db.Sales.Remove(sale);
                }
                db.SaveChanges();
            }
        }
    }
}
