using CarDetector.DataModels;
using CarDetector.iOS.Converters;
using CarDetector.iOS.Extensions;
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

        public ResultView(IntPtr handle) : base(handle)
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
            var widthScale = imageView.Bounds.Size.Width / imageView.Image.Size.Width;
            var heightScale = imageView.Bounds.Size.Height / imageView.Image.Size.Height;

            var brX = bbox.BrX * imageView.Image.Size.Width * widthScale;
            var brY = bbox.BrY * imageView.Image.Size.Height * heightScale;
            var tlX = bbox.TlX * imageView.Image.Size.Width * widthScale;
            var tlY = bbox.TlY * imageView.Image.Size.Height * heightScale;

            var width = brX - tlX;
            var height = brY - tlY;

            const int borderWidth = 3;
            var frame = new CGRect(tlX-borderWidth,tlY-borderWidth,width+borderWidth*2, height+borderWidth*2);
            var bboxView = new UIView(frame)
            {
                BackgroundColor = UIColor.Clear
            };
            bboxView.Layer.BorderWidth = borderWidth;
            bboxView.Layer.BorderColor = UIColor.Green.CGColor;
            imageView.AddSubview(bboxView);
        }
    }
}