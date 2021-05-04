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
    public partial class CategoryPriceControl : UserControl
    {
        public string ColorPrice
        {
            get { return (string)this.GetValue(ColorPriceProperty); }
            set { this.SetValue(ColorPriceProperty, value); }
        }
        public static readonly DependencyProperty ColorPriceProperty = DependencyProperty.Register(
          "ColorPrice", typeof(string), typeof(CategoryPriceControl));

        public string Price
        {
            get { return (string)this.GetValue(PriceProperty); }
            set { this.SetValue(PriceProperty, value); }
        }
        public static readonly DependencyProperty PriceProperty = DependencyProperty.Register(
          "Price", typeof(string), typeof(CategoryPriceControl));

        public Visibility DeleteButtonVisible
        {
            get {
                Visibility isIt = (Visibility)this.GetValue(IsDeletableProperty);
                return isIt; }
            set {
                
                this.SetValue(IsDeletableProperty, value); 
            }
        }
        public static readonly DependencyProperty IsDeletableProperty = DependencyProperty.Register(
          "DeleteButtonVisible", typeof(Visibility), typeof(CategoryPriceControl), new PropertyMetadata(Visibility.Hidden));



        public CategoryPriceControl()
        {
            InitializeComponent();
            
        }


        public static readonly RoutedEvent DeleteButtonClickEvent = EventManager.RegisterRoutedEvent(
                                    "DeleteButtonClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CategoryPriceControl));
        public event RoutedEventHandler DeleteButtonClick
        {
            add { AddHandler(DeleteButtonClickEvent, value); }
            remove { RemoveHandler(DeleteButtonClickEvent, value); }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(CategoryPriceControl.DeleteButtonClickEvent);
            RaiseEvent(newEventArgs);
        }

        public event RoutedEventHandler Click;
        private void MainButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Click != null)
            {
                this.Click(this, e);
            }
        }
    }
}
