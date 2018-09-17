using System;
using CarDetector.Interfaces;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace CarDetector.ViewModels
{
    public class BaseViewModel : MvxViewModel
    {
        #region propeties

        public IDialogService DialogService { get; } = Mvx.Resolve<IDialogService>();
        public IFileService FileService { get; } = Mvx.Resolve<IFileService>();
        
        private bool _isActivated;
        protected bool IsActivated
        {
            get { return _isActivated; }
            set { SetProperty(ref _isActivated, value); }
        }

        private void OnGoBack()
        {
            Close(this);
        }

        #endregion

        #region methods
        public BaseViewModel()
        {
        }

        /// <summary>
        /// Raised ONLY ONCE when related UI is ready to be displayed to end user
        /// </summary>
        public virtual void OnLoaded()
        {
        }

        /// <summary>
        /// Raise every time when UI is about to be shown to end user
        /// </summary>
        public virtual void OnActivated()
        {
            IsActivated = true;
        }

        /// <summary>
        /// Raise every time when UI was hidden from user
        /// </summary>
        public virtual void OnDeactivated()
        {

            IsActivated = false;
        }

        #endregion
    }
}
