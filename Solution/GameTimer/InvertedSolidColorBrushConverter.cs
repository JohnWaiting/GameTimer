using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace GameTimer
{
    public class InvertedSolidColorBrushConverter : IValueConverter
    {
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            if (value == DependencyProperty.UnsetValue)
            {
                return value;
            }

            var brush = (SolidColorBrush) value;
            return new SolidColorBrush(Color.FromRgb((Byte)(Byte.MaxValue - brush.Color.R), (Byte)(Byte.MaxValue - brush.Color.G), (Byte)(Byte.MaxValue - brush.Color.B)));
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}