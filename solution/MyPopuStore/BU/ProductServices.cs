using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPopuStore.BU
{
    using DAL.DB;
    using Microsoft.EntityFrameworkCore;

    class ProductServices
    {
        /// <summary>
        /// Crée et ajoute un nouveau produit à la DB
        /// </summary>
        /// <param name="code">Identifiant</param>
        /// <param name="label"> Nom du produit</param>
        /// <param name="catPrice"> <c>CategoryPrice</c> à associer au produit</param>
        /// <param name="quantityStock"> Stock initial </param>
        /// <param name="picturePath"> Représentation picturale du futur produit </param>
        public static void AddProduct(string code,string label, CategoryPrice catPrice, int quantityStock = 0, string picturePath = "") 
        {

            Product product = new()
            {
                QuantityStock = quantityStock,
                Code = code,
                Label = label,
                CategoryPriceId = catPrice.CategoryPriceId,
                Picture = picturePath
            };
            

            using(MyPopupStoreDBContext db = new())
            {
                try
                {
                    db.Add(product);
                    db.SaveChanges();
                }
                catch(Exception e)
                {
                    throw new Exception("Produit invalide", e);
                }
                
            }
        }
        
        /// <summary>
        /// Est-ce qu'un produit est utilisé dans une vente ?
        /// </summary>
        /// <param name="code"> Identifiant du produit en question.</param>
        /// <returns>True si le produit apparait dans une vente</returns>
        public static bool ProductIsUsed(string code)
        {
            using (MyPopupStoreDBContext db = new())
            {
                return db.SaleDetails.Where(e => e.ProductCode == code).Any();
            }
        }

        /// <summary>
        /// Vérifie l'existance dans la DB d'un identifiant.
        /// </summary>
        /// <param name="code">Identifiant du produit en question. </param>
        /// <returns>True si l'identifiant existe dans la DB.</returns>
        public static bool ProductExist(string code)
        {
            using (MyPopupStoreDBContext db = new())
            {
                return db.Products.Where(e => e.Code == code).Any();
            }
        }

        /// <summary>
        /// Supprime un produit de la DB.
        /// </summary>
        /// <param name="code">Identifiant du produit en question.</param>
        public static void RemoveProduct(string code)
        {
            using (MyPopupStoreDBContext db = new())
            {
                if(ProductServices.ProductIsUsed(code))
                    throw new Exception("Impossible de supprimer un produit enregistré dans au moins une vente.");

                db.Products.Remove(GetProduct(code));
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Supprime tout les enregistrement de la table des produits.
        /// </summary>
        public static void RemoveAllProduct()
        {
            using(MyPopupStoreDBContext db = new())
            {
                List<Product> products = GetAllProduct();
                foreach(Product product in products)
                {
                    db.Products.Remove(product);
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Met à jour un produit dans la DB.
        /// </summary>
        /// <param name="product">Produit modifié avec l'identifiant du produit à mettre à jour.</param>
        public static void UpdateProduct(Product product)
        {
            using (MyPopupStoreDBContext db = new())
            {
                db.Products.Update(product);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Recherche d'un produit dans la DB.
        /// </summary>
        /// <param name="code">Identifiant du produit en question.</param>
        /// <returns>Le produit correspondant ou Null.</returns>
        public static Product GetProduct(string code)
        {
            using (MyPopupStoreDBContext db = new MyPopupStoreDBContext())
            {
                return db.Products.SingleOrDefault(e=>e.Code==code);
            }
        }

        /// <summary>
        /// Recherche d'une liste de produit commençant par le paramètre.
        /// </summary>
        /// <param name="codeStartWith"> <c>String</c> représentant le début des codes à retourner.</param>
        /// <returns>La liste des produit correspondant.</returns>
        public static List<Product> GetAllProduct(string codeStartWith="") 
        {
            using (MyPopupStoreDBContext db = new MyPopupStoreDBContext())
            {
                List < Product > products = db.Products.Where(e => e.Code.StartsWith(codeStartWith)).ToList();
                return products;
            }
        }
    }
}
