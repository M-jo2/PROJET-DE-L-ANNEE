using MyPopuStore.BU;
using MyPopuStore.DAL.DB;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using static System.Net.Mime.MediaTypeNames;

namespace MyPopuStore.UI.Resource
{
    public class HexaToSolidColorBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush color;
            try
            {
                color = (SolidColorBrush)(new BrushConverter().ConvertFrom(value.ToString())) ;
            }
            catch
            {
                color = Brushes.White;
            }
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class PriceToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = value != null ? ((decimal)value).ToString() : "null";
            if (result.EndsWith(",0")) result =  result.Split(',')[0]+',';
            if (result == "0") result = "";
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Decimal equivalent;

            string texte = (string)value;
            texte = Regex.Replace(texte, "[^0-9+\\,]", "");

            string[] textes = texte.Split(',');
            if (textes.Length > 1)
            {
                textes[1] += textes[1].Length == 0 ? "0":"";
                textes[1] = textes[1].Length > 2 ? textes[1].Substring(0, 2):textes[1];

                texte = textes[0] +','+ textes[1];
            }
            else texte = textes[0];

            if (Decimal.TryParse(texte, out equivalent))
            {
                return equivalent;
            }
            return 0;
        }
    }

    public class IntToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = value != null ? ((int)value).ToString() : "null";
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int equivalent;
            if (int.TryParse((string)value, out equivalent))
            {
                return equivalent;
            }
            return -1;
        }
    }

    public class PaymentTypeToLogoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string picture;
            if ((bool)value)
            {
                picture = @"\Pictures\Logo\logo_payment_type_card.png";
            }
            else
            {
                picture = @"\Pictures\Logo\logo_payment_type_money.png";
            }
            return picture;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
