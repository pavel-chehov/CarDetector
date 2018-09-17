using System;
using UIKit;
using Foundation;
using MvvmCross.iOS.Views;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Platform;

namespace CarDetector.iOS.MvxCustomization
{
    public class StoryboardViewContainer : MvxIosViewsContainer
    {
        public StoryboardViewContainer(MvxApplicationDelegate applicationDelegate, UIWindow window)
        {
        }

        protected override IMvxIosView CreateViewOfType(Type viewType, MvxViewModelRequest request)
        {
            IMvxIosView viewController = null;
            try
            {
                var viewStoryboard = UIStoryboard.FromName(viewType.Name, null);
                viewController = (IMvxIosView)viewStoryboard.InstantiateViewController(viewType.Name);
            }
            catch (MonoTouchException)
            {
                //failed to load controller from storyboard.
                //Continue with the default approach
            }

            if (viewController == null)
                viewController = base.CreateViewOfType(viewType, request);

            return viewController;
        }
    }
}
