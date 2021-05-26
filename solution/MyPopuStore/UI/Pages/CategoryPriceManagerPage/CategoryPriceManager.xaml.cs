using MyPopuStore.DAL.DB;
using MyPopuStore.UI.Resource.User_Control;
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

namespace MyPopuStore.UI.Pages.CategoryPriceManagerPage
{
    /// <summary>
    /// Logique d'interaction pour CategoryPriceManager.xaml
    /// </summary>
    public partial class CategoryPriceManager : Window
    {
        CategoryPriceManagerViewModel categoryPriceManagerViewModel;
        public CategoryPrice ChoiceCat { get; set; } 

        public CategoryPriceManager()
        {
            InitializeComponent();
            PopulateAndBind();
        }

        public void PopulateAndBind()
        {
            categoryPriceManagerViewModel = new CategoryPriceManagerViewModel();
            this.DataContext = categoryPriceManagerViewModel;
        }


        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void AddCategoryPrice_Click(object sender, RoutedEventArgs e)
        {
            categoryPriceManagerViewModel.CreateCategoryPrice();
        }

        private void SelectPrice_Click(object sender, RoutedEventArgs e)
        {
            ChoiceCat = (CategoryPrice)((CategoryPriceControl)sender).DataContext;
            this.DialogResult = true;
        }

        private void DeletePrice_Click(object sender, RoutedEventArgs e)
        {
            categoryPriceManagerViewModel.DeleteCategoryPrice((CategoryPrice)((CategoryPriceControl)sender).DataContext);
        }
    }
}
