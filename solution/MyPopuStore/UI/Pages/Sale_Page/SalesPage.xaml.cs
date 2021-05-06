using MyPopuStore.DAL.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace MyPopuStore.UI.Pages.Sale_Page
{
    public partial class SalesPage : Page
    {
        public SalesPage()
        {
            InitializeComponent();
            PopulateAndBind();
        }
        private void PopulateAndBind()
        {
            RegistredSaleFrame.Content = new RegistredSalePage();
            NewSaleFrame.Content = new NewSalePage();
        }
    }
}
