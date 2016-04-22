using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MeetingClock.Converters
{
    public class IntToCurrencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return $@"${(int)value}.00";
            }
            catch (Exception)
            {
                return null;
            }            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (((string) value).StartsWith("$"))
                {
                    string[] elements = ((string) value).Split('.');
                    return Int32.Parse(elements[0].Substring(1));
                }
                return Int32.Parse(((string) value).Split('.')[0]);
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
