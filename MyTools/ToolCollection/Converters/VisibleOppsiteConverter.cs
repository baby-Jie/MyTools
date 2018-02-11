using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ToolCollection.Converters
{
    public class VisibleOppsiteConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                bool visibility = (bool)value;
                if (!visibility)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("not support visibility convertback");
        }
    }
}
