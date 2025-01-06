using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace TaskOrganizer.Converters;
public class MultiBooleanToVisibilityConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length == 2 && values.All(v => v is bool))
        {
            bool isLoginAttempted = (bool)values[0];
            bool isPasswordIncorrect = (bool)values[1];
            return (isLoginAttempted && isPasswordIncorrect) ? Visibility.Visible : Visibility.Collapsed;
        }
        return Visibility.Collapsed;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
