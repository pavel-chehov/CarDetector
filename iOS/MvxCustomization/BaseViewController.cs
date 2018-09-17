using System;
using MvvmCross.Core.Views;
using MvvmCross.iOS.Views;
using MvvmCross.Core.ViewModels;
using UIKit;
using Foundation;
using CarDetector.ViewModels;
using CarDetector.iOS.Helpers;

namespace CarDetector.iOS.MvxCustomization
{
    public class BaseViewController<TViewModel> : MvxViewController<TViewModel>
        where TViewModel : BaseViewModel
    {
        NSObject _viewWillAppearNotification;
        NSObject _viewDidDissapearNotification;

        void ViewWillAppearCallback(object sender, Foundation.NSNotificationEventArgs args)
        {
            // Access strongly typed args
            this.OnCoinBalancedViewWillAppear();
        }

        void ViewDidDissapearCallback(object sender, Foundation.NSNotificationEventArgs args)
        {
            // Access strongly typed args
            this.OnCoinBalancedViewDidDisappear();
        }

        void SetupListeners()
        {
            _viewWillAppearNotification = UIApplication.Notifications.ObserveWillEnterForeground(ViewWillAppearCallback);
            _viewDidDissapearNotification = UIApplication.Notifications.ObserveDidEnterBackground(ViewDidDissapearCallback);
        }

        void TeardownListeners()
        {
            _viewWillAppearNotification.Dispose();
            _viewDidDissapearNotification.Dispose();
        }

        public TViewModel Context
        {
            get { return (TViewModel)ViewModel; }
        }

        public BaseViewController(IntPtr ptr) : base(ptr) { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.OnCoinBalancedViewDidLoad();
        }

        public override void ViewWillAppear(bool animated)
        {
            this.OnCoinBalancedViewWillAppear();
            SetupListeners();
            base.ViewWillAppear(animated);
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            this.OnCoinBalancedViewDidDisappear();
            TeardownListeners();
        }

        protected virtual void HideNavigationBar()
        {
            NavigationController.SetNavigationBarHidden(true, false);
            UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.Default, true);
        }

        protected virtual void ShowNavigationBar()
        {
            NavigationController.SetNavigationBarHidden(false, true);
        }
    }
}
