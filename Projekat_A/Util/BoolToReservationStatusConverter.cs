using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Projekat_A.Util
{
    public class BoolToReservationStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isReserved)
            {
                try
                {
                    string reservedText = Application.Current.TryFindResource("Reserved")?.ToString() ?? "Reserved";
                    string availableText = Application.Current.TryFindResource("Available")?.ToString() ?? "Available";

                    return isReserved ? reservedText : availableText;
                }
                catch
                {
                    return isReserved ? "Reserved" : "Available";
                }
            }
            return Application.Current.TryFindResource("Unknown")?.ToString() ?? "Unknown";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
