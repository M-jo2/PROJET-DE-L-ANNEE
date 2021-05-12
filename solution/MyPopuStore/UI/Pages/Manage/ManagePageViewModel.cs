using Microsoft.Win32;
using MyPopuStore.BU;
using MyPopuStore.DAL.DB;
using MyPopuStore.UI.Pages.Manage.ClosePopupStore;
using MyPopuStore.UI.Pages.Manage.ReportExport;
using MyPopuStore.UI.Resource;
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
            RefreshInfo();
        }

        private void RefreshInfo()
        {
            if (InfoServices.PopupStoreInfoExist())
            {
                Info info = InfoServices.GetPopupStoreInfo();
                ExistShopVisibility = "Visible";
                CreationDate = info.CreationDate.ToString();
                NamePopupStore = info.PopupStoreName;

            }
            else
            {
                ExistShopVisibility = "Hidden";
            }
        }

        public void NewInfoPopupStore(string popupStoreName)
        {
            try
            {
                InfoServices.CreatePopupStoreInfo(popupStoreName);
                RefreshInfo();
            }catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
        }

        public void CloturePopupStore()
        {
            ClosePopupValidation closePopupValidation = new();

            if(closePopupValidation.ShowDialog() == true)
            {
                InfoServices.DeletePopupStoreInfo();
                SaleServices.DeleteSaleAndSaleDetails();
                if (closePopupValidation.DeleteProduct) ProductServices.RemoveAllProduct();
            }
            
            RefreshInfo();
        }


        public void SaveReport()
        {
            Export export = new();
            ReportManageView reportManageView = new();
            SaveFileDialog saveFileDialog = new();

            if (reportManageView.ShowDialog() == true)
            {
                export.End = reportManageView.ReportManageViewModel.DateEnd;
                export.Start = reportManageView.ReportManageViewModel.DateStart;
                saveFileDialog.Filter = "Text Files(*.html)|*.html|All files (*.*)|*.*";
                saveFileDialog.FileName = InfoServices.GetPopupStoreInfo().PopupStoreName + ".html";

                if (saveFileDialog.ShowDialog() == true)
                {
                    export.ExportToHtml(saveFileDialog.FileName, true);
                }
            }
        }
    }
}
