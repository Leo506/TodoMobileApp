using System;
using System.Globalization;
using Xamarin.Forms;

namespace GoalApp.Convertors;

public class EnumToIntConvertor : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is Enum ? (int)value : 0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is int ? Enum.ToObject(targetType, value) : 0;
    }
}