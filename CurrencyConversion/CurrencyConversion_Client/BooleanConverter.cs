using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace CurrencyConversion_Client
{
    public class BooleanConverter<T> : IValueConverter
    {
        public BooleanConverter(T trueValue, T falseValue)
        {
            True = trueValue;
            False = falseValue;
        }

        public T True { get; set; }
        public T False { get; set; }

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool && ((bool)value) ? True : False;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is T && EqualityComparer<T>.Default.Equals((T)value, True);
        }
    }
    public sealed class TrueToVisibleConverter : BooleanConverter<Visibility>
    {
        public TrueToVisibleConverter() :
            base(Visibility.Visible, Visibility.Collapsed)
        { }
    }
    public sealed class TrueToCollapsedConverter : BooleanConverter<Visibility>
    {
        public TrueToCollapsedConverter() :
            base(Visibility.Collapsed, Visibility.Visible)
        { }
    }
}
