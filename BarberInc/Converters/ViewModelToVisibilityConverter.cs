using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace BarberInc.Converters
{
    public class CurrentViewModelToVisibilityConverter : IValueConverter
    {
        public Visibility TrueValue { get; set; }
        public Visibility FalseValue { get; set; }

        public CurrentViewModelToVisibilityConverter()
        {
            FalseValue = Visibility.Collapsed;
            TrueValue = Visibility.Visible;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return TrueValue;
            }
            else if (value.ToString() == typeof(LoginViewModel).ToString()|| value.ToString() == typeof(SignUpViewModel).ToString())
            {
                return FalseValue;
            }
            else
            {
                return TrueValue;

            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
