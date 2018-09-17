using System;
using CarDetector.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.IoC;

namespace CarDetector
{
    public class CarDetectorApp : MvxApplication
    {
        public override void Initialize()
        {
            this.CreatableTypes()
               .EndingWith("Service")
               .AsInterfaces()
               .RegisterAsLazySingleton();

            CreatableTypes()
               .EndingWith("Messenger")
               .AsInterfaces()
               .RegisterAsSingleton();

            this.RegisterAppStart<MainViewModel>();
        }
    }
}
