using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace LinuxInstaller.Converters
{
    public class IsNotEmptyConverter : IValueConverter
    {
        public object? Convert(object? value, Type _t, object? _p, CultureInfo _c)
        {
            if (value is string s)
            {
                return !string.IsNullOrEmpty(s);
            }
            return value != null;
        }

        public object? ConvertBack(object? value, Type _t, object? _p, CultureInfo _c)
        {
            throw new NotImplementedException();
        }
    }
}