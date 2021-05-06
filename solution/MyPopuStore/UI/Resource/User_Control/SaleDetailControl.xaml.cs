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

namespace MyPopuStore.UI.Resource.User_Control
{
    public partial class SaleDetailControl : UserControl
    {
        public string ProductPicture
        {
            get { return (string)this.GetValue(ProductPictureProperty); }
            set { this.SetValue(ProductPictureProperty, value); }
        }
        public static readonly DependencyProperty ProductPictureProperty = DependencyProperty.Register(
          "ProductPicture", typeof(string), typeof(SaleDetailControl));


        public string ProductCode
        {
            get { return (string)this.GetValue(ProductCodeProperty); }
            set { this.SetValue(ProductCodeProperty, value); }
        }
        public static readonly DependencyProperty ProductCodeProperty = DependencyProperty.Register(
          "ProductCode", typeof(string), typeof(SaleDetailControl));



        public string ProductLabel
        {
            get { return (string)this.GetValue(ProductLabelProperty); }
            set { this.SetValue(ProductLabelProperty, value); }
        }
        public static readonly DependencyProperty ProductLabelProperty = DependencyProperty.Register(
          "ProductLabel", typeof(string), typeof(SaleDetailControl));


        public string SaleDetailPrice
        {
            get { return (string)this.GetValue(SaleDetailPriceProperty); }
            set { this.SetValue(SaleDetailPriceProperty, value); }
        }
        public static readonly DependencyProperty SaleDetailPriceProperty = DependencyProperty.Register(
          "SaleDetailPrice", typeof(string), typeof(SaleDetailControl));


        public string SaleDetailNbProduct
        {
            get { return (string)this.GetValue(SaleDetailNbProductProperty); }
            set { this.SetValue(SaleDetailNbProductProperty, value); }
        }
        public static readonly DependencyProperty SaleDetailNbProductProperty = DependencyProperty.Register(
          "SaleDetailNbProduct", typeof(string), typeof(SaleDetailControl));


        private bool isDeletable;
        public bool IsDeletable
        {
            get { return isDeletable; }
            set { 
                isDeletable = value;
                if (isDeletable) DeleteButtonVisible = Visibility.Visible;
                else DeleteButtonVisible = Visibility.Collapsed;
            }
        }

        public Visibility DeleteButtonVisible
        {
            get { return (Visibility)this.GetValue(DeleteButtonVisibleProperty); }
            set { this.SetValue(DeleteButtonVisibleProperty, value); }
        }
        public static readonly DependencyProperty DeleteButtonVisibleProperty = DependencyProperty.Register(
          "DeleteButtonVisible", typeof(Visibility), typeof(SaleDetailControl));

        public SaleDetailControl()
        {
            InitializeComponent();
            IsDeletable = false;
        }

        public static readonly RoutedEvent DeleteButtonClickEvent = EventManager.RegisterRoutedEvent(
                                    "DeleteSaleDetailButtonClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SaleDetailControl));
        public event RoutedEventHandler DeleteButtonClick
        {
            add { AddHandler(DeleteButtonClickEvent, value); }
            remove { RemoveHandler(DeleteButtonClickEvent, value); }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(SaleDetailControl.DeleteButtonClickEvent);
            RaiseEvent(newEventArgs);
        }
    }
}
