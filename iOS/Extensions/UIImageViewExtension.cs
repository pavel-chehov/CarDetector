//copied from here: https://gist.github.com/andrey-str/60f8297239f8827a979e
using System;
using CoreGraphics;
using UIKit;

namespace CarDetector.iOS.Extensions
{
    public static class UIImageViewExtension
    {
        public static CGPoint PixelPointFromViewPoint(this UIImageView self, CGPoint touch)
        {
            // Sanity check to see whether the touch is actually in the view
            if (touch.X >= 0.0 && touch.X <= self.Frame.Size.Width && touch.Y >= 0.0 && touch.Y <= self.Frame.Size.Height)
            {
                // http://developer.apple.com/library/ios/#DOCUMENTATION/UIKit/Reference/UIView_Class/UIView/UIView.html#//apple_ref/occ/cl/UIView
                switch (self.ContentMode)
                {
                    // Simply scale the Image Size by the Size of the Frame
                    case UIViewContentMode.ScaleToFill:
                    // Redraw is basically the same as scale to fill but redraws itself in the drawRect call (so when bounds change)
                    case UIViewContentMode.Redraw:
                        return new CGPoint(Math.Floor(touch.X / (self.Frame.Size.Width / self.Image.Size.Width)), Math.Floor(touch.Y / (self.Frame.Size.Height / self.Image.Size.Height)));
                    // Although the documentation doesn't state it, we will assume a centered Image. This mode makes the Image fit into the view with its aspect ratio
                    case UIViewContentMode.ScaleAspectFit:
                        {
                            // If the aspect ratio favours Width over Height in relation to the images aspect ratio
                            if (self.Frame.Size.Width / self.Frame.Size.Height > self.Image.Size.Width / self.Image.Size.Height)
                            {
                                // Checking whether the touch coordinate is not in a 'blank' spot on the view
                                if (touch.X >= (self.Frame.Size.Width / 2.0) - (((self.Frame.Size.Height / self.Image.Size.Height) * self.Image.Size.Width) / 2.0)
                                        && touch.X <= (self.Frame.Size.Width / 2.0) + (((self.Frame.Size.Height / self.Image.Size.Height) * self.Image.Size.Width) / 2.0))
                                {
                                    // Scaling by using the Height ratio as a reference, and minusing the blank X coordiantes on the view
                                    return new CGPoint(Math.Floor((touch.X - ((self.Frame.Size.Width / 2.0) - (((self.Frame.Size.Height / self.Image.Size.Height) * self.Image.Size.Width) / 2.0))) / (self.Frame.Size.Height / self.Image.Size.Height)), Math.Floor(touch.Y / (self.Frame.Size.Height / self.Image.Size.Height)));
                                }
                                break;
                            }
                            // Or if the aspect ratio favours Height over Width in relation to the images aspect ratio
                            else if (self.Frame.Size.Width / self.Frame.Size.Height < self.Image.Size.Width / self.Image.Size.Height)
                            {
                                // Obtaining half of the view that is taken up by the aspect ratio
                                var halfAspectFit = ((self.Frame.Size.Width / self.Image.Size.Width) * self.Image.Size.Height) / 2.0;
                                // Checking whether the touch coordinate is not in a 'blank' spot on the view
                                if (touch.Y >= (self.Frame.Size.Height / 2.0) - halfAspectFit
                                        && touch.Y <= (self.Frame.Size.Height / 2.0) + halfAspectFit)
                                {
                                    // Scaling by using the Width ratio as a reference, and minusing the blank Y coordinates on the view
                                    return new CGPoint(Math.Floor(touch.X / (self.Frame.Size.Width / self.Image.Size.Width)), Math.Floor((touch.Y - ((self.Frame.Size.Width / 2.0) - halfAspectFit)) / (self.Frame.Size.Height / self.Image.Size.Height)));
                                }
                            }
                            // This is just the same as a scale to fill mode if the aspect ratios from the view and the Image are the same
                            else return new CGPoint(Math.Floor(touch.X / (self.Frame.Size.Width / self.Image.Size.Width)), Math.Floor(touch.Y / (self.Frame.Size.Width / self.Image.Size.Height)));
                            break;
                        }
                    // This fills the view with the Image in its aspect ratio, meaning that it could get cut off in either axis
                    case UIViewContentMode.ScaleAspectFill:
                        {
                            // If the aspect ratio favours Width over Height in relation to the images aspect ratio
                            if (self.Frame.Size.Width / self.Frame.Size.Height > self.Image.Size.Width / self.Image.Size.Height)
                            {
                                // Scaling by using the Width ratio, this will cut off some Height
                                return new CGPoint(Math.Floor(touch.X / (self.Frame.Size.Width / self.Image.Size.Width)), Math.Floor(touch.Y / (self.Frame.Size.Width / self.Image.Size.Width)));
                            }
                            // If the aspect ratio favours Height over Width in relation to the images aspect ratio
                            else if (self.Frame.Size.Width / self.Frame.Size.Height < self.Image.Size.Width / self.Image.Size.Height)
                            {
                                // Scaling by using the Height ratio, this will cut off some Width
                                return new CGPoint(Math.Floor(touch.X / (self.Frame.Size.Height / self.Image.Size.Height)), Math.Floor(touch.Y / (self.Frame.Size.Height / self.Image.Size.Height)));
                            }
                            // Again if the aspect ratios are the same, then it will just be another copy of scale to fill mode
                            else return new CGPoint(Math.Floor(touch.X / (self.Frame.Size.Width / self.Image.Size.Width)), Math.Floor(touch.Y / (self.Frame.Size.Width / self.Image.Size.Height)));
                        }
                    // This centers the Image in the view both vertically and horizontally
                    case UIViewContentMode.Center:
                        {
                            // Check whether our touch is on the Image centered vertically and horizontally
                            if (touch.X >= (self.Frame.Size.Width / 2.0) - (self.Image.Size.Width / 2.0)
                                    && touch.X <= (self.Frame.Size.Width / 2.0) + (self.Image.Size.Width / 2.0)
                                    && touch.Y >= (self.Frame.Size.Height / 2.0) - (self.Image.Size.Height / 2.0)
                                    && touch.Y <= (self.Frame.Size.Height / 2.0) + (self.Image.Size.Height / 2.0))
                                // Just return the touch coordinates and minus the offset
                                return new CGPoint(Math.Floor(touch.X - ((self.Frame.Size.Width / 2.0) - (self.Image.Size.Width / 2.0))), Math.Floor(touch.Y - ((self.Frame.Size.Height / 2.0) - (self.Image.Size.Height / 2.0))));
                            break;
                        }
                    // This centers the Image horizontally and moves it up to the top
                    case UIViewContentMode.Top:
                        {
                            // Check whether our touch is on the Image centered horizontally and put at the vertical start
                            if (touch.X >= (self.Frame.Size.Width / 2.0) - (self.Image.Size.Width / 2.0)
                                    && touch.X <= (self.Frame.Size.Width / 2.0) + (self.Image.Size.Width / 2.0)
                                    && touch.Y <= self.Image.Size.Height)
                                // Just return the touch coordinates and minus the offset
                                return new CGPoint(Math.Floor(touch.X - ((self.Frame.Size.Width / 2.0) - (self.Image.Size.Width / 2.0))), Math.Floor(touch.Y));
                            break;
                        }
                    // This centers the Image horizontally and moves it down to the bottom
                    case UIViewContentMode.Bottom:
                        {
                            // Check whether our touch is on the Image centered horizontally and put at the vertical end
                            if (touch.X >= (self.Frame.Size.Width / 2.0) - (self.Image.Size.Width / 2.0)
                                    && touch.X <= (self.Frame.Size.Width / 2.0) + (self.Image.Size.Width / 2.0)
                                    && touch.Y >= self.Frame.Size.Height - self.Image.Size.Height)
                                // Just return the touch coordinates and minus the offset
                                return new CGPoint(Math.Floor(touch.X - ((self.Frame.Size.Width / 2.0) - (self.Image.Size.Width / 2.0))), Math.Floor(touch.Y - (self.Frame.Size.Height - self.Image.Size.Height)));
                            break;
                        }
                    // This moves the Image to the horizontal start and centers it vertically
                    case UIViewContentMode.Left:
                        {
                            // Check whether our touch is on the Image at the horizontal start and centered vertically
                            if (touch.X <= self.Image.Size.Width
                                    && touch.Y >= (self.Frame.Size.Height / 2.0) - (self.Image.Size.Height / 2.0)
                                    && touch.Y <= (self.Frame.Size.Height / 2.0) + (self.Image.Size.Height / 2.0))
                                return new CGPoint(Math.Floor(touch.X), Math.Floor(touch.Y - ((self.Frame.Size.Height / 2.0) - (self.Image.Size.Height / 2.0))));
                            break;
                        }
                    // This moves the Image to the horizontal end and centers it vertically
                    case UIViewContentMode.Right:
                        {
                            if (touch.X >= self.Frame.Size.Width - self.Image.Size.Width
                                    && touch.Y >= (self.Frame.Size.Height / 2.0) - (self.Image.Size.Height / 2.0)
                                    && touch.Y <= (self.Frame.Size.Height / 2.0) + (self.Image.Size.Height / 2.0))
                                return new CGPoint(Math.Floor(touch.X - (self.Frame.Size.Width - self.Image.Size.Width)), Math.Floor(touch.Y - ((self.Frame.Size.Height / 2.0) - (self.Image.Size.Height / 2.0))));
                            break;
                        }
                    // This simply moves the Image to the horizontal and vertical start
                    case UIViewContentMode.TopLeft:
                        {
                            if (touch.X <= self.Image.Size.Width
                                    && touch.X <= self.Image.Size.Height)
                                // My favourite
                                return new CGPoint(Math.Floor(touch.X), Math.Floor(touch.Y));
                            break;
                        }
                    // This moves the Image to the horizontal end and vertical start
                    case UIViewContentMode.TopRight:
                        {
                            if (touch.X >= self.Frame.Size.Width - self.Image.Size.Width
                                    && touch.Y <= self.Image.Size.Height)
                                return new CGPoint(Math.Floor(touch.X - (self.Frame.Size.Width - self.Image.Size.Width)), Math.Floor(touch.Y));
                            break;
                        }
                    // This moves the Image to the horizontal start and vertical end
                    case UIViewContentMode.BottomLeft:
                        {
                            if (touch.X <= self.Image.Size.Width
                                    && touch.Y <= self.Frame.Size.Height - self.Image.Size.Height)
                                return new CGPoint(Math.Floor(touch.X), Math.Floor(touch.Y - (self.Frame.Size.Height - self.Image.Size.Height)));
                            break;
                        }
                    // This moves the Image to the horizontal and vertical end
                    case UIViewContentMode.BottomRight:
                        {
                            if (touch.X <= self.Frame.Size.Width - self.Image.Size.Width
                                    && touch.Y <= self.Frame.Size.Height - self.Image.Size.Height)
                                return new CGPoint(Math.Floor(touch.X - (self.Frame.Size.Width - self.Image.Size.Width)), Math.Floor(touch.Y - (self.Frame.Size.Height - self.Image.Size.Height)));
                            break;
                        }
                    default: break;
                }
            }
            return CGPoint.Empty;
        }

        public static CGPoint ViewPointFromPixelPoint(this UIImageView self, CGPoint pixelPoint)
        {
            // Sanity check to see whether the pixel point is actually in the Image
            if (pixelPoint.X >= 0.0 && pixelPoint.X <= self.Image.Size.Width && pixelPoint.Y >= 0.0 && pixelPoint.Y <= self.Image.Size.Height)
            {
                switch (self.ContentMode)
                {
                    case UIViewContentMode.ScaleToFill:
                    case UIViewContentMode.Redraw:
                        return new CGPoint(Math.Floor(pixelPoint.X * (self.Frame.Size.Width / self.Image.Size.Width)), Math.Floor(pixelPoint.Y * (self.Frame.Size.Height / self.Image.Size.Height)));
                    case UIViewContentMode.ScaleAspectFit:
                        {
                            if (self.Frame.Size.Width / self.Frame.Size.Height > self.Image.Size.Width / self.Image.Size.Height)
                                return new CGPoint(Math.Floor(((self.Frame.Size.Width / 2.0) - ((self.Image.Size.Width / 2.0) * (self.Frame.Size.Height / self.Image.Size.Height))) + pixelPoint.X * (self.Frame.Size.Height / self.Image.Size.Height)), Math.Floor(pixelPoint.Y * (self.Frame.Size.Height / self.Image.Size.Height)));
                            else if (self.Frame.Size.Width / self.Frame.Size.Height < self.Image.Size.Width / self.Image.Size.Height)
                                return new CGPoint(Math.Floor(pixelPoint.X * (self.Frame.Size.Width / self.Image.Size.Width)), Math.Floor(((self.Frame.Size.Height / 2.0) - ((self.Image.Size.Height / 2.0) * (self.Frame.Size.Width / self.Image.Size.Width))) + pixelPoint.Y * (self.Frame.Size.Width / self.Image.Size.Width)));
                            return new CGPoint(Math.Floor(pixelPoint.X * (self.Frame.Size.Width / self.Image.Size.Width)), Math.Floor(pixelPoint.Y * (self.Frame.Size.Height / self.Image.Size.Height)));
                        }
                    case UIViewContentMode.ScaleAspectFill:
                        {
                            if (self.Frame.Size.Width / self.Frame.Size.Height > self.Image.Size.Width / self.Image.Size.Height)
                                return new CGPoint(Math.Floor(pixelPoint.X * (self.Frame.Size.Width / self.Image.Size.Width)), Math.Floor(pixelPoint.Y * (self.Frame.Size.Width / self.Image.Size.Width)));
                            else if (self.Frame.Size.Width / self.Frame.Size.Height < self.Image.Size.Width / self.Image.Size.Height)
                                return new CGPoint(Math.Floor(pixelPoint.X * (self.Frame.Size.Height / self.Image.Size.Height)), Math.Floor(pixelPoint.Y * (self.Frame.Size.Height / self.Image.Size.Height)));
                            return new CGPoint(Math.Floor(pixelPoint.X * (self.Frame.Size.Width / self.Image.Size.Width)), Math.Floor(pixelPoint.Y * (self.Frame.Size.Height / self.Image.Size.Height)));
                        }
                    case UIViewContentMode.Center:
                        return new CGPoint(Math.Floor(pixelPoint.X + (self.Frame.Size.Width / 2.0) - (self.Image.Size.Width / 2.0)), Math.Floor(pixelPoint.Y + (self.Frame.Size.Height / 2.0) - (self.Image.Size.Height / 2.0)));
                    case UIViewContentMode.Top:
                        return new CGPoint(Math.Floor(pixelPoint.X + (self.Frame.Size.Width / 2.0) - (self.Image.Size.Width / 2.0)), Math.Floor(pixelPoint.Y));
                    case UIViewContentMode.Bottom:
                        return new CGPoint(Math.Floor(pixelPoint.X + (self.Frame.Size.Width / 2.0) - (self.Image.Size.Width / 2.0)), Math.Floor(pixelPoint.Y - (self.Frame.Size.Height - self.Image.Size.Height)));
                    case UIViewContentMode.Left:
                        return new CGPoint(Math.Floor(pixelPoint.X), Math.Floor(pixelPoint.Y + (self.Frame.Size.Height / 2.0) - (self.Image.Size.Height / 2.0)));
                    case UIViewContentMode.Right:
                        return new CGPoint(Math.Floor(pixelPoint.X - (self.Frame.Size.Width - self.Image.Size.Width)), Math.Floor(pixelPoint.Y + (self.Frame.Size.Height / 2.0) - (self.Image.Size.Height / 2.0)));
                    case UIViewContentMode.TopLeft:
                        return new CGPoint(Math.Floor(pixelPoint.X), Math.Floor(pixelPoint.Y));
                    case UIViewContentMode.TopRight:
                        return new CGPoint(Math.Floor(pixelPoint.X - (self.Frame.Size.Width - self.Image.Size.Width)), Math.Floor(pixelPoint.Y));
                    case UIViewContentMode.BottomLeft:
                        return new CGPoint(Math.Floor(pixelPoint.X), Math.Floor(pixelPoint.Y - (self.Frame.Size.Height - self.Image.Size.Height)));
                    case UIViewContentMode.BottomRight:
                        return new CGPoint(Math.Floor(pixelPoint.X - (self.Frame.Size.Width - self.Image.Size.Width)), Math.Floor(pixelPoint.Y - (self.Frame.Size.Height - self.Image.Size.Height)));
                    default: break;
                }
            }
            return CGPoint.Empty;
        }

        public static CGSize PixelSizeFromViewSize(this UIImageView self, CGSize viewSize)
        {
            if (viewSize.Width >= 0.0 && viewSize.Width <= self.Frame.Size.Width && viewSize.Height >= 0.0 && viewSize.Height <= self.Frame.Size.Height)
            {
                switch (self.ContentMode)
                {
                    case UIViewContentMode.ScaleToFill:
                    case UIViewContentMode.Redraw:
                        return new CGSize(Math.Floor(viewSize.Width / (self.Frame.Size.Width / self.Image.Size.Width)), Math.Floor(viewSize.Height / (self.Frame.Size.Height / self.Image.Size.Height)));
                    case UIViewContentMode.ScaleAspectFit:
                        {
                            if (self.Frame.Size.Width / self.Frame.Size.Height > self.Image.Size.Width / self.Image.Size.Height)
                                return new CGSize(Math.Floor(viewSize.Width / (self.Frame.Size.Height / self.Image.Size.Height)), Math.Floor(viewSize.Height / (self.Frame.Size.Height / self.Image.Size.Height)));
                            else if (self.Frame.Size.Width / self.Frame.Size.Height < self.Image.Size.Width / self.Image.Size.Height)
                                return new CGSize(Math.Floor(viewSize.Width / (self.Frame.Size.Width / self.Image.Size.Width)), Math.Floor(viewSize.Height / (self.Frame.Size.Height / self.Image.Size.Height)));
                            return new CGSize(Math.Floor(viewSize.Width / (self.Frame.Size.Width / self.Image.Size.Width)), Math.Floor(viewSize.Height / (self.Frame.Size.Height / self.Image.Size.Height)));
                        }
                    case UIViewContentMode.ScaleAspectFill:
                        {
                            if (self.Frame.Size.Width / self.Frame.Size.Height > self.Image.Size.Width / self.Image.Size.Height)
                                return new CGSize(Math.Floor(viewSize.Width / (self.Frame.Size.Width / self.Image.Size.Width)), Math.Floor(viewSize.Height / (self.Frame.Size.Width / self.Image.Size.Width)));
                            else if (self.Frame.Size.Width / self.Frame.Size.Height < self.Image.Size.Width / self.Image.Size.Height)
                                return new CGSize(Math.Floor(viewSize.Width / (self.Frame.Size.Height / self.Image.Size.Height)), Math.Floor(viewSize.Height / (self.Frame.Size.Height / self.Image.Size.Height)));
                            return new CGSize(Math.Floor(viewSize.Width / (self.Frame.Size.Width / self.Image.Size.Width)), Math.Floor(viewSize.Height / (self.Frame.Size.Height / self.Image.Size.Height)));
                        }
                    case UIViewContentMode.Center:
                    case UIViewContentMode.Top:
                    case UIViewContentMode.Bottom:
                    case UIViewContentMode.Left:
                    case UIViewContentMode.Right:
                    case UIViewContentMode.TopLeft:
                    case UIViewContentMode.TopRight:
                    case UIViewContentMode.BottomLeft:
                    case UIViewContentMode.BottomRight:
                        return new CGSize(Math.Floor(viewSize.Width), Math.Floor(viewSize.Height));
                    default: break;
                }
            }
            return CGSize.Empty;
        }

        public static CGSize ViewSizeFromPixelPixel(this UIImageView self, CGSize pixelSize)
        {
            if (pixelSize.Width >= 0.0 && pixelSize.Width <= self.Image.Size.Width && pixelSize.Height >= 0.0 && pixelSize.Height <= self.Image.Size.Height)
            {
                switch (self.ContentMode)
                {
                    case UIViewContentMode.ScaleToFill:
                    case UIViewContentMode.Redraw:
                        return new CGSize(Math.Floor(pixelSize.Width * (self.Frame.Size.Width / self.Image.Size.Width)), Math.Floor(pixelSize.Height * (self.Frame.Size.Height / self.Image.Size.Height)));
                    case UIViewContentMode.ScaleAspectFit:
                        {
                            if (self.Frame.Size.Width / self.Frame.Size.Height > self.Image.Size.Width / self.Image.Size.Height)
                                return new CGSize(Math.Floor(pixelSize.Width * (self.Frame.Size.Height / self.Image.Size.Height)), Math.Floor(pixelSize.Height * (self.Frame.Size.Height / self.Image.Size.Height)));
                            else if (self.Frame.Size.Width / self.Frame.Size.Height < self.Image.Size.Width / self.Image.Size.Height)
                                return new CGSize(Math.Floor(pixelSize.Width * (self.Frame.Size.Width / self.Image.Size.Width)), Math.Floor(pixelSize.Height * (self.Frame.Size.Height / self.Image.Size.Height)));
                            return new CGSize(Math.Floor(pixelSize.Width * (self.Frame.Size.Width / self.Image.Size.Width)), Math.Floor(pixelSize.Height * (self.Frame.Size.Height / self.Image.Size.Height)));
                        }
                    case UIViewContentMode.ScaleAspectFill:
                        {
                            if (self.Frame.Size.Width / self.Frame.Size.Height > self.Image.Size.Width / self.Image.Size.Height)
                                return new CGSize(Math.Floor(pixelSize.Width * (self.Frame.Size.Width / self.Image.Size.Width)), Math.Floor(pixelSize.Height * (self.Frame.Size.Width / self.Image.Size.Width)));
                            else if (self.Frame.Size.Width / self.Frame.Size.Height < self.Image.Size.Width / self.Image.Size.Height)
                                return new CGSize(Math.Floor(pixelSize.Width * (self.Frame.Size.Height / self.Image.Size.Height)), Math.Floor(pixelSize.Height * (self.Frame.Size.Height / self.Image.Size.Height)));
                            return new CGSize(Math.Floor(pixelSize.Width * (self.Frame.Size.Width / self.Image.Size.Width)), Math.Floor(pixelSize.Height * (self.Frame.Size.Height / self.Image.Size.Height)));
                        }
                    case UIViewContentMode.Center:
                    case UIViewContentMode.Top:
                    case UIViewContentMode.Bottom:
                    case UIViewContentMode.Left:
                    case UIViewContentMode.Right:
                    case UIViewContentMode.TopLeft:
                    case UIViewContentMode.TopRight:
                    case UIViewContentMode.BottomLeft:
                    case UIViewContentMode.BottomRight:
                        return new CGSize(Math.Floor(pixelSize.Width), Math.Floor(pixelSize.Height));
                    default: break;
                }
            }
            return CGSize.Empty;
        }
    }
}
