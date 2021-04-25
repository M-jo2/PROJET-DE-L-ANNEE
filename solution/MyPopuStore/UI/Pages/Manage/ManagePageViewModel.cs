using MyPopuStore.BU;
using MyPopuStore.DAL.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyPopuStore.UI.Pages.Manage
{
    class ManagePageViewModel : INotifyPropertyChanged
    {
        private string newShopVisibility = "Hidden";
        private string existShopVisibility = "Hidden";

        private string namePopupStore;
        private string creationDate;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string NamePopupStore { 
            get{return namePopupStore;}
            set { 
                namePopupStore = value;
                OnPropertyChanged();
            }
        }
        public string CreationDate { 
            get
            {
                return creationDate;
            }
            set {
                creationDate = value;
                OnPropertyChanged();
            }
        }

        public string NewShopVisibility {
            get
            {
                return newShopVisibility;
            }
            set
            {
                newShopVisibility = value;

                if(newShopVisibility == existShopVisibility)
                    ExistShopVisibility = value =="Hidden" ? "Visible" : "Hidden";
                OnPropertyChanged();
            }
        }

        public string ExistShopVisibility
        {
            get
            {
                return existShopVisibility;
            }
            set
            {
                existShopVisibility = value;

                if (newShopVisibility == existShopVisibility)
                    NewShopVisibility = value == "Hidden" ? "Visible" : "Hidden";
                OnPropertyChanged();
            }
        }


        public ManagePageViewModel()
        {
            refreshInfo();
        }

        private void refreshInfo()
        {
            InfoServices infoServices = new InfoServices();
            if (infoServices.popupStoreInfoExist())
            {
                Info info = infoServices.getPopupStoreInfo();
                ExistShopVisibility = "Visible";
                CreationDate = info.CreationDate.ToString();
                NamePopupStore = info.PopupStoreName;

            }
            else
            {
                ExistShopVisibility = "Hidden";
            }
        }

        public void newInfoPopupStore(string popupStoreName)
        {
            try
            {
                InfoServices infoServices = new InfoServices();
                infoServices.createPopupStoreInfo(popupStoreName);
                refreshInfo();
            }catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
        }

        public void deleteInfoPopupStore()
        {
            InfoServices infoServices = new InfoServices();
            infoServices.deletePopupStoreInfo();
            refreshInfo();
        }
    }
}
