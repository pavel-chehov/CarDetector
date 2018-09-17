using System;
using MvvmCross.Platform.Converters;
using UIKit;

namespace CarDetector.iOS.Converters
{
    public class BytesToUIImageConverter : MvxValueConverter<byte[], UIImage>
    {
        protected override UIImage Convert(byte[] value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new UIKit.UIImage(Foundation.NSData.FromArray(value));
        }
    }
}
