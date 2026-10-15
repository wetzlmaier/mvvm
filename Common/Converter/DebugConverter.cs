using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;

namespace Common.Converter
{
    public class DebugConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            Debug.WriteLine("Convert: Value '{0}', TargetType '{1}', Parameter '{2}'", value, targetType, parameter);
            Debugger.Break();
            return value;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
