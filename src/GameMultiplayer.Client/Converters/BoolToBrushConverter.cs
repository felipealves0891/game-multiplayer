using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace GameMultiplayer.Client.Converters
{
    public class BoolToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
                return false;

            if((bool)value == true)
                return new SolidColorBrush(Color.FromRgb(255, 255, 0));
            else
                return new SolidColorBrush(Color.FromRgb(170, 170, 170));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
