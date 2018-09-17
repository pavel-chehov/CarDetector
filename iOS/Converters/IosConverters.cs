using System;
namespace CarDetector.iOS.Converters
{
    public static class IosConverters
    {
        public static readonly InvertBooleanConverter InvertBooleanConverter = new InvertBooleanConverter();
        public static readonly BytesToUIImageConverter BytesToUIImageConverter = new BytesToUIImageConverter();
        public static readonly PathToUIImageConverter PathToUIImageConverter = new PathToUIImageConverter();
    }
}
