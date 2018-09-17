using System;
using CarDetector.iOS.Helpers;
using CarDetector.ViewModels;
using Foundation;
using MvvmCross.iOS.Views;
using UIKit;

namespace CarDetector.iOS.MvxCustomization
{
    public class BaseTableViewController<TViewModel> : MvxTableViewController<TViewModel>
    where TViewModel : BaseViewModel
    {
        public TViewModel Context
        {
            get { return (TViewModel)ViewModel; }
        }

        public BaseTableViewController(IntPtr ptr)
            : base(ptr)
        {

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.OnCoinBalancedViewDidLoad();
            EnableTapToDismiss();
        }

        public override void ViewWillAppear(bool animated)
        {
            this.OnCoinBalancedViewWillAppear();
            base.ViewWillAppear(animated);
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            this.OnCoinBalancedViewDidDisappear();
        }

        protected void EnableTapToDismiss()
        {
            var gestureRecognizer = new UITapGestureRecognizer(() => View.EndEditing(true))
            {
                CancelsTouchesInView = false,
            };
            TableView.AddGestureRecognizer(gestureRecognizer);
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = base.GetCell(tableView, indexPath);
            //add separator
            var addSeparator = SeparatorForCellIsRequired(tableView, indexPath);
            var height = GetHeightForRow(tableView, indexPath);
            TableSeparatorHelper.AddSeparatorAndSelectionIfNeeded(tableView, indexPath, cell, addSeparator, true, height);
            if (cell.Accessory == UITableViewCellAccessory.DisclosureIndicator)
                cell.AccessoryView = new UIImageView(UIImage.FromBundle("disclosure"));

            return cell;
        }

        public override void DidRotate(UIInterfaceOrientation fromInterfaceOrientation)
        {
            base.DidRotate(fromInterfaceOrientation);
            //to refresh dynamically added controls (separators, backgrounds, etc)
            TableView.ReloadData();
        }

        protected virtual bool SeparatorForCellIsRequired(UITableView tableView, NSIndexPath indexPath)
        {
            return true;
        }

        protected virtual void ChangeRowSelectionForViewWithTag(UIView view, bool clearSelection = false)
        {
            var rowIndex = view.Tag;
            if (!clearSelection)
            {
                TableView.SelectRow(NSIndexPath.FromItemSection(rowIndex, 0), true, UITableViewScrollPosition.None);
            }
            else
            {
                TableView.DeselectRow(NSIndexPath.FromItemSection(rowIndex, 0), true);
            }
        }

        protected virtual bool DeactivateRowForView(UITextField textField)
        {
            textField.ResignFirstResponder();
            ChangeRowSelectionForViewWithTag(textField, true);
            return true;
        }

        private bool IsEmpty(object sender)
        {
            var textInput = sender as UITextField;
            return string.IsNullOrEmpty(textInput.Text);
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            tableView.DeselectRow(indexPath, true);
        }
    }
}
