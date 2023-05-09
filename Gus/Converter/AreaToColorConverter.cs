using Gus.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Gus.Converter
{
    public class AreaToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Area area))
                return null;

            switch (area.nazwa_poziom)
            {
                case "Temat":
                    return new SolidColorBrush(Colors.Green);
                case "Zakres informacyjny":
                    return new SolidColorBrush(Colors.Red);
                case "Dziedzina":
                    return new SolidColorBrush(Colors.Yellow);
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
