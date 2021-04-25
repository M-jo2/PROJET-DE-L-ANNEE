using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyPopuStore.UI.Pages.Manage
{
    /// <summary>
    /// Logique d'interaction pour ManagePage.xaml
    /// </summary>
    public partial class ManagePage : Page
    {
        ManagePageViewModel managePageModel;
        public ManagePage()
        {
            
            InitializeComponent();
            PopulateAndBind();


        }

        public void PopulateAndBind()
        {
            managePageModel = new ManagePageViewModel();

            this.DataContext = managePageModel;
        }
        private void CreatePopupStoreButton(object sender, RoutedEventArgs e)
        {
            string popupname = this.PopupStoreNameEntry.Text;
            if (popupname != "")
                managePageModel.newInfoPopupStore(popupname);
            else
                this.PopupStoreNameEntry.BorderBrush = Brushes.IndianRed;
        }

        private void ClosePopupStoreButton(object sender, RoutedEventArgs e)
        {
            managePageModel.deleteInfoPopupStore();
            
        }
    }
}
