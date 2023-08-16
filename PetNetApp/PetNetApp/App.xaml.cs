using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PetNetApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
    }

    public class IconSizeConverter : IValueConverter
    {
        private const double numerator = 1.4;
        private const double denominator = 2;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double inputSize;
            double.TryParse(value.ToString(), out inputSize);
            int finalPixelSize = (int)(inputSize * (numerator / denominator));
            return finalPixelSize % 2 == 0 ? finalPixelSize : finalPixelSize + 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((double)value * (denominator / numerator)).ToString();
        }
    }
    public class RadioButtonIconMarginConverter : IValueConverter
    {
        private const double numerator = 1;
        private const double denominator = 10;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double inputSize;
            double.TryParse(value.ToString(), out inputSize);
            return (int)(inputSize * (numerator / denominator));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((double)value * (denominator / numerator)).ToString();
        }
    }
    public class CheckBoxIconMarginConverter : IValueConverter
    {
        private const double numerator = 1;
        private const double denominator = 20;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double inputSize;
            double.TryParse(value.ToString(), out inputSize);
            return (int)(inputSize * (numerator / denominator));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((double)value * (denominator / numerator)).ToString();
        }
    }

    [ValueConversion(typeof(bool), typeof(bool))]
    public class BoolStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolString = value is bool && (bool)value;

            return boolString ? "Yes" : "No";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && value.ToString() == "Yes";
        }
    }

    
}
