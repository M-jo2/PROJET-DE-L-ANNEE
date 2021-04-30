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

        public static void createPopupStoreInfo(string popupStoreName)
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

        public static Info getPopupStoreInfo()
        {
            Info info = null;

            using (MyPopupStoreDBContext db = new())
            {
                info = db.Infos.FirstOrDefault();
            }

            return info;
        }

        public static bool popupStoreInfoExist()
        {
            bool exist = false;

            using (MyPopupStoreDBContext db = new())
            {
                exist = db.Infos.Count() > 0;
            }

            return exist;
        }

        public static void deletePopupStoreInfo()
        {
            using (MyPopupStoreDBContext db = new())
            {
                if (popupStoreInfoExist())
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
