using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace MyPopuStore.UI.Pages.Product_Page.Image_Manager
{
    /// <summary>
    /// Logique d'interaction pour ImageManagerView.xaml
    /// </summary>
    public partial class ImageManagerView : Window , INotifyPropertyChanged
    {
        private string pathFile = "";
        public string PathFile {
            get
            {
                return pathFile;
            }
            set
            {
                pathFile = value;
                OnPropertyChanged();
            }
        }
        public ImageManagerView()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void OpenFileDialogClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
                PathFile = openFileDialog.FileName;
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
