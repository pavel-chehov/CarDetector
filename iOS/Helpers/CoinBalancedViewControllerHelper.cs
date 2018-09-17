using System;
using MvvmCross.Core;
using MvvmCross.Core.ViewModels;
using UIKit;
using CarDetector.iOS.MvxCustomization;
using CarDetector.ViewModels;

namespace CarDetector.iOS.Helpers
{
    public static class CoinBalancedViewControllerHelper
    {
        public static void OnCoinBalancedViewDidLoad<TViewModel>(this BaseViewController<TViewModel> controller)
            where TViewModel : BaseViewModel
        {
            if (controller != null && controller.Context != null)
                OnViewDidLoad(controller.Context, controller.View, controller.GetType().Name);
        }

        public static void OnCoinBalancedViewDidLoad<TViewModel>(this BaseTableViewController<TViewModel> controller)
            where TViewModel : BaseViewModel
        {
            if (controller != null && controller.Context != null)
                OnViewDidLoad(controller.Context, controller.View, controller.GetType().Name);
        }

        private static void OnViewDidLoad(BaseViewModel context, UIView view, string viewName)
        {
            context.OnLoaded();
        }

        public static void OnCoinBalancedViewWillAppear<TViewModel>(this BaseViewController<TViewModel> controller)
            where TViewModel : BaseViewModel
        {
            if (controller != null && controller.Context != null)
                controller.Context.OnActivated();
        }

        public static void OnCoinBalancedViewWillAppear<TViewModel>(this BaseTableViewController<TViewModel> controller)
            where TViewModel : BaseViewModel
        {
            if (controller != null && controller.Context != null)
                controller.Context.OnActivated();
        }

        public static void OnCoinBalancedViewDidDisappear<TViewModel>(this BaseViewController<TViewModel> controller)
            where TViewModel : BaseViewModel
        {
            if (controller != null && controller.Context != null)
                controller.Context.OnDeactivated();
        }

        public static void OnCoinBalancedViewDidDisappear<TViewModel>(this BaseTableViewController<TViewModel> controller)
            where TViewModel : BaseViewModel
        {
            if (controller != null && controller.Context != null)
                controller.Context.OnDeactivated();
        }
    }
}
