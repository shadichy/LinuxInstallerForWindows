using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace LinuxInstaller.Converters;

public class FileSizeConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is long sizeInBytes)
        {
            const long KiloByte = 1024;
            const long MegaByte = KiloByte * 1024;
            const long GigaByte = MegaByte * 1024;

            if (sizeInBytes >= GigaByte)
            {
                return $"{sizeInBytes / (double)GigaByte:F1} GB";
            }
            if (sizeInBytes >= MegaByte)
            {
                return $"{sizeInBytes / (double)MegaByte:F1} MB";
            }
            if (sizeInBytes >= KiloByte)
            {
                return $"{sizeInBytes / (double)KiloByte:F1} KB";
            }
            return $"{sizeInBytes} Bytes";
        }
        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
