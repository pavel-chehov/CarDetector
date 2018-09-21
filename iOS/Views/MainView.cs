using Foundation;
using System;
using UIKit;
using CarDetector.iOS.MvxCustomization;
using CarDetector.ViewModels;
using MvvmCross.Binding.BindingContext;
using CarDetector.iOS.Converters;

namespace CarDetector.iOS
{
    public partial class MainView : BaseViewController<MainViewModel>
    {
        private bool _isCameraInitialized = false;

        public MainView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            SetupBinding();
            InitUI();
        }

        private void SetupBinding()
        {
            var set = this.CreateBindingSet<MainView, MainViewModel>();
            set.Bind(closeButton).To(vm => vm.CloseCommand);
            set.Bind(captureButton).To(vm => vm.CaptureCommand);
            set.Bind(overlayView).For(v => v.Hidden).To(vm => vm.IsBusy).WithConversion(IosConverters.InvertBooleanConverter);
            set.Apply();
        }

        private void InitUI()
        {
            HideNavigationBar();
            ViewModel.CameraService.AuthorizeCameraUse();
        }

        public override void ViewWillLayoutSubviews()
        {
            if (_isCameraInitialized)
                return;
            ViewModel.CameraService.SetupLiveCameraStream(cameraView);
            _isCameraInitialized = true;
        }
    }
}