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

        public void createPopupStoreInfo(string popupStoreName)
        {
            Info info = new Info(popupStoreName, DateTime.Now.Date);
            using (DAL.DB.MyPopupStoreDBContext db = new DAL.DB.MyPopupStoreDBContext())
            {
                

                if (db.Infos.Count() != 0) throw new Exception("Supprimer le PopupStore actuel avant d'insérer de nouvelles informations.");
                if (popupStoreName.Length > 50) throw new Exception("Le nom du PopupStore dois faire moins de 50 caractères.");
                if (popupStoreName.Length == 0) throw new Exception("Un nom pour le PopupStore est requis.");


                db.Infos.Add(info);
                db.SaveChanges();
            }
        }

        public Info getPopupStoreInfo()
        {
            Info info = null;

            using (DAL.DB.MyPopupStoreDBContext db = new DAL.DB.MyPopupStoreDBContext())
            {
                info = db.Infos.FirstOrDefault();
            }

            return info;
        }

        public bool popupStoreInfoExist()
        {
            bool exist = false;

            using (DAL.DB.MyPopupStoreDBContext db = new DAL.DB.MyPopupStoreDBContext())
            {
                exist = db.Infos.Count() > 0;
            }

            return exist;
        }

        public void deletePopupStoreInfo()
        {
            using (DAL.DB.MyPopupStoreDBContext db = new DAL.DB.MyPopupStoreDBContext())
            {
                if (this.popupStoreInfoExist())
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
