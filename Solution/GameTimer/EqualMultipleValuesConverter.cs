using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace GameTimer
{
    public class EqualMultipleValuesConverter : IMultiValueConverter
    {
        public Object Convert(Object[] values, Type targetType, Object parameter, CultureInfo culture)
        {
            for (int i = 1; i < values.Length; i++)
            {
                if (!Equals(values[i - 1], values[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public Object[] ConvertBack(Object value, Type[] targetTypes, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}