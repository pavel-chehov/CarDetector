using System;
using System.Globalization;
using Foundation;
using MvvmCross.Platform.Converters;
using UIKit;

namespace CarDetector.iOS.Converters
{
    public class PathToUIImageConverter : MvxValueConverter<string, UIImage>
    {
        protected override UIImage Convert(string value, Type targetType, object parameter, CultureInfo culture)
        {
            return new UIImage(NSData.FromFile(value));
        }
    }
}
