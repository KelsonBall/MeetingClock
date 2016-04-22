using System;
using System.Globalization;
using System.Windows.Data;

namespace MeetingClock.Converters
{
    public class DoubleToCurrencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string digits = Math.Round((double) value, 2).ToString();
                if (!digits.Contains("."))
                    digits += ".";
                int centDigits = digits.Split('.')[1].Length;
                if (centDigits < 1)
                    digits += "0";
                if (centDigits < 2)
                    digits += "0";
                return $@"${digits}";
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (((string) value).StartsWith("$"))
                    return double.Parse(((string) value).Substring(1));
                return double.Parse((string) value);
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
