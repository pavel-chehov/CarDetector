using System;
using MvvmCross.Platform.Converters;

namespace CarDetector.iOS.Converters
{
    public class InvertBooleanConverter : MvxValueConverter<bool, bool>
    {
        protected override bool Convert(bool value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !value;
        }
    }
}
