using System;
using CoreGraphics;
using Foundation;
using UIKit;


namespace CarDetector.iOS.Helpers
{
    public static class TableSeparatorHelper
    {
        public static UIImageView AddSeparatorImageAtYPosition(UITableViewCell cell, nfloat yPosition)
        {
            var image = UIImage.FromBundle("line_separator");
            var endLine = new UIImageView(new CGRect(0, yPosition - 2f, UIScreen.MainScreen.Bounds.Width, image.Size.Height));
            endLine.Image = image;
            cell.ContentView.AddSubview(endLine);
            return endLine;
        }

        public static void AddSeparatorAndSelectionIfNeeded(UITableView tableView, NSIndexPath indexPath, UITableViewCell cell, bool addSeparator, bool addSelection, nfloat height)
        {
            if (addSeparator)
            {
                AddSeparatorImageAtYPosition(cell, 2f);
            }

            if (addSelection)
            {
                AddSelection(tableView, indexPath, cell, height);
            }
        }

        public static void AddSelection(UITableView tableView, NSIndexPath indexPath, UITableViewCell cell, nfloat height)
        {
            var selectedView = new UIView(new CGRect(0, 2f, UIScreen.MainScreen.Bounds.Width, height));
            cell.SelectedBackgroundView = selectedView;
        }
    }
}
