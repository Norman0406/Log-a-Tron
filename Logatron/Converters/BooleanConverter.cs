using System;
using System.Globalization;
using System.Windows.Data;

namespace Logatron.Converters
{
    public class BooleanConverter<T> : IValueConverter
    {
        public BooleanConverter(T trueValue, T falseValue)
        {
            True = trueValue;
            False = falseValue;
        }

        enum Parameters
        {
            Normal, Inverted
        }

        public T True { get; set; }
        public T False { get; set; }

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var direction = Parameters.Normal;
            if (parameter != null)
            {
                direction = (Parameters)Enum.Parse(typeof(Parameters), (string)parameter);
            }

            var trueValue = direction == Parameters.Normal ? True : False;
            var falseValue = direction == Parameters.Normal ? False : True;

            return value is bool && ((bool)value) ? trueValue : falseValue;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
