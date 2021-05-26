using MyPopuStore.DAL.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPopuStore.BU
{
    class InfoServices
    {
        /// <summary>
        /// Initialise le nouveau magasin dans la DB
        /// </summary>
        /// <param name="popupStoreName"> Nom du magasin </param>
        public static void CreatePopupStoreInfo(string popupStoreName)
        {
            Info info = new Info() { PopupStoreName =popupStoreName, CreationDate= DateTime.Now.Date };
            using (MyPopupStoreDBContext db = new())
            {
                

                if (db.Infos.Count() != 0) throw new Exception("Supprimer le PopupStore actuel avant d'insérer de nouvelles informations.");
                if (popupStoreName.Length > 50) throw new Exception("Le nom du PopupStore dois faire moins de 50 caractères.");
                if (popupStoreName.Length == 0) throw new Exception("Un nom pour le PopupStore est requis.");


                db.Infos.Add(info);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Null ou <c>Info</c>. Contiens les information relative au magasin</c></returns>
        public static Info GetPopupStoreInfo()
        {
            Info info = null;

            using (MyPopupStoreDBContext db = new())
            {
                info = db.Infos.FirstOrDefault();
            }

            return info;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns> True si un magasin a déjà été initialisé</returns>
        public static bool PopupStoreInfoExist()
        {
            bool exist = false;

            using (MyPopupStoreDBContext db = new())
            {
                exist = db.Infos.Count() > 0;
            }

            return exist;
        }

        /// <summary>
        /// Supprime uniquement les information relative au magasin.
        /// </summary>
        public static void DeletePopupStoreInfo()
        {
            using (MyPopupStoreDBContext db = new())
            {
                if (PopupStoreInfoExist())
                {
                    List<Info> infos = db.Infos.ToList();
                    foreach (Info info in infos)
                    {
                        db.Remove(info);
                    }
                    db.SaveChanges();
                }
            }
        }
        
    }
}
