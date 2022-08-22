using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using GoalApp.Models;
using Xamarin.Forms;

namespace GoalApp.Convertors;

public class UrgencyToColorConvertor : IValueConverter
{
    private static readonly Dictionary<Urgency, Color> UrgencyToColor = new Dictionary<Urgency, Color>()
    {
        { Urgency.Urgently, Color.Red },
        { Urgency.Middle, Color.Yellow },
        { Urgency.NoMatter, Color.Aqua }
    };

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var urgency = (Urgency)value;
        return UrgencyToColor[urgency];
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var color = (Color)value;
        return UrgencyToColor.FirstOrDefault(uc => uc.Value.Equals(color)).Key;
    }
}