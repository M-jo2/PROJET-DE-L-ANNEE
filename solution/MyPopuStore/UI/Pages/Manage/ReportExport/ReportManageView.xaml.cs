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
using System.Windows.Shapes;

namespace MyPopuStore.UI.Pages.Manage.ReportExport
{
    /// <summary>
    /// Logique d'interaction pour ReportManageView.xaml
    /// </summary>
    public partial class ReportManageView : Window
    {
        public ReportManageViewModel ReportManageViewModel { get; set; }
        public ReportManageView()
        {
            InitializeComponent();
            ReportManageViewModel = new();
            this.DataContext = ReportManageViewModel;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
