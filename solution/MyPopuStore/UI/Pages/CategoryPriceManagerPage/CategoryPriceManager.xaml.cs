using MyPopuStore.DAL.DB;
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
        public CategoryPrice ChoiceCat; 
        public CategoryPriceManager()
        {
            InitializeComponent();
            PopulateAndBind();
        }

        public void PopulateAndBind()
        {
            categoryPriceManagerViewModel = new CategoryPriceManagerViewModel();
            this.DataContext = categoryPriceManagerViewModel;
            ListOfAllCategories.ItemsSource = categoryPriceManagerViewModel.AllCategories;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void AddCategoryPrice_Click(object sender, RoutedEventArgs e)
        {
            categoryPriceManagerViewModel.CreateCategoryPrice();
            ListOfAllCategories.ItemsSource = categoryPriceManagerViewModel.AllCategories;
        }

        private void SelectPrice_Click(object sender, RoutedEventArgs e)
        {
            ChoiceCat = (CategoryPrice)((Button)sender).DataContext;
            this.DialogResult = true;
        }

        private void DeletePrice_RightClick(object sender, MouseButtonEventArgs e)
        {

            categoryPriceManagerViewModel.DeleteCategoryPrice((CategoryPrice)((Button)sender).DataContext);
            ListOfAllCategories.ItemsSource = categoryPriceManagerViewModel.AllCategories;
        }
    }
}
