using System;
using System.Collections.Generic;
using UIKit;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Platform;
using MvvmCross.iOS.Views.Presenters;
using MvvmCross.Platform;
using CarDetector.iOS.MvxCustomization;
using CarDetector.iOS.Services;
using CarDetector.Interfaces;

namespace CarDetector.iOS
{
    public class Setup : MvxIosSetup
    {
        private readonly MvxApplicationDelegate _applicationDelegate;
        private readonly UIWindow _window;

        public Setup(MvxApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
        {
            _applicationDelegate = applicationDelegate;
            _window = window;
        }

        public Setup(MvxApplicationDelegate applicationDelegate, IMvxIosViewPresenter presenter)
            : base(applicationDelegate, presenter)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new CarDetectorApp();
        }

        protected override void InitializePlatformServices()
        {
            Mvx.LazyConstructAndRegisterSingleton<ICameraService, CameraService>();
            Mvx.LazyConstructAndRegisterSingleton<IDialogService, DialogService>();
            Mvx.LazyConstructAndRegisterSingleton<IFileService, FileService>();
            Mvx.LazyConstructAndRegisterSingleton<IPlatformService, PlatformService>();
            base.InitializePlatformServices();
        }

        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();
        }

        protected override IMvxIosViewPresenter CreatePresenter()
        {
            var viewPresenter = new CarDetectorViewPresenter(_applicationDelegate, _window);
            Mvx.RegisterSingleton<IMvxIosViewPresenter>(viewPresenter);
            return viewPresenter;
        }

        protected override MvvmCross.iOS.Views.IMvxIosViewsContainer CreateIosViewsContainer()
        {
            return new StoryboardViewContainer(_applicationDelegate, _window);
        }

        protected override void FillTargetFactories(MvvmCross.Binding.Bindings.Target.Construction.IMvxTargetBindingFactoryRegistry registry)
        {
            base.FillTargetFactories(registry);
        }

        protected override List<Type> ValueConverterHolders
        {
            get
            {
                var holders = base.ValueConverterHolders;
                if (holders == null)
                    holders = new List<Type>();
                holders.Add(typeof(MvvmCross.Platform.Converters.MvxValueConverter));
                return holders;
            }
        }
    }
}
