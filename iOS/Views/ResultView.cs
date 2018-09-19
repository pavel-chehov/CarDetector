using CarDetector.DataModels;
using CarDetector.iOS.Converters;
using CarDetector.iOS.MvxCustomization;
using CarDetector.ViewModels;
using CoreGraphics;
using Foundation;
using MvvmCross.Binding.BindingContext;
using System;
using UIKit;

namespace CarDetector.iOS
{
    public partial class ResultView : BaseViewController<ResultViewModel>
    {
        public Bbox Bbox
        {
            get => null;
            set => CreateBbox(value);
        }
        
        public ResultView (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            SetupBinding();
            ShowNavigationBar();
        }

        public override void ViewWillDisappear(bool animated)
        {
            HideNavigationBar();
            base.ViewWillDisappear(animated);
        }

        private void SetupBinding()
        {
            var set = this.CreateBindingSet<ResultView, ResultViewModel>();
            set.Bind(modelLabel).To(vm => vm.Model);
            set.Bind(brandLabel).To(vm => vm.BrandName);
            set.Bind(probLabel).To(vm => vm.Probability);
            set.Bind(doneButton).To(vm => vm.CloseCommand);
            set.Bind(imageView).For(v => v.Image).To(vm => vm.FullPath).WithConversion(IosConverters.PathToUIImageConverter);
            set.Bind(this).For(v => v.Bbox).To(vm => vm.Bbox);
            set.Apply();
        }
        
        private void CreateBbox(Bbox bbox)
        {
            var brX = bbox.BrX * imageView.Frame.Width;
            var brY = bbox.BrY * imageView.Frame.Height;
            var tlX = bbox.TlX * imageView.Frame.Width;
            var tlY = bbox.TlY * imageView.Frame.Height;

            var width = brX - tlX;
            var height = brY - tlY;
            
            var frame = new CGRect(tlX, tlY, width, height);
            var bboxView = new UIView(frame)
            {
                BackgroundColor = UIColor.Clear
            };
            bboxView.Layer.BorderWidth = 3;
            bboxView.Layer.BorderColor = UIColor.Green.CGColor;
            imageView.AddSubview(bboxView);
        }
    }
}