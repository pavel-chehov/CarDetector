using System;
using System.Linq;
using MvvmCross.Core.Views;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.iOS.Views.Presenters;
using MvvmCross.iOS.Views;
using MvvmCross.Platform.Exceptions;
using UIKit;

namespace CarDetector.iOS.MvxCustomization
{
    public class CarDetectorViewPresenter : MvxIosViewPresenter
    {
        private IMvxViewsContainer _viewsContainer;

        protected IMvxViewsContainer ViewsContainer
        {
            get
            {
                if (_viewsContainer == null)
                {
                    _viewsContainer = Mvx.Resolve<IMvxViewsContainer>();
                }
                return _viewsContainer;
            }
        }

        public CarDetectorViewPresenter(UIApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
        {
        }

        public override void Show(MvxViewModelRequest request)
        {
            //all backstack manipulations should be done BEFORE creating and displaying new view/viewmodel
            var clearBackStack = request.ParameterValues != null && request.ParameterValues.ContainsKey("clearBack");
            var goBackToView = request.ParameterValues != null && request.ParameterValues.ContainsKey("goBack");
            if (goBackToView)
            {
                var requestedViewType = ViewsContainer.GetViewType(request.ViewModelType);
                var currentBackStack = MasterNavigationController.ViewControllers;
                var viewControllerToJump = currentBackStack.FirstOrDefault(v => v.GetType() == requestedViewType);
                if (viewControllerToJump != null)
                {
                    MasterNavigationController.PopToViewController(viewControllerToJump, true);
                    return;
                }
            }
            var controller = this.CreateViewControllerFor(request);
            var viewController = (IMvxIosView)controller;
            if (viewController == null)
                throw new MvxException("Passed in IMvxIosView is not a UIViewController");

            base.Show(viewController);

            //prevent animations fail - delete after showing new page
            if (clearBackStack)
            {
                MasterNavigationController.SetViewControllers(new UIViewController[] { controller as UIViewController }, false);
            }

        }

        protected override UINavigationController CreateNavigationController(UIViewController viewController)
        {
            var controller = base.CreateNavigationController(viewController);
            controller.NavigationBar.Translucent = false;
            controller.Toolbar.Translucent = false;
            return controller;
        }
    }
}
