using System.Threading.Tasks;
using MvvmCross.Platform.Core;
using UIKit;
using MvvmCross.Platform;
using CarDetector.Interfaces;
using static CarDetector.Constants;

namespace CarDetector.iOS.Services
{
    public class DialogService : IDialogService
    {
        IMvxMainThreadDispatcher dispathcher = Mvx.Resolve<IMvxMainThreadDispatcher>();

        public Task ShowAsync(string title, string message, string ok = Text.Ok)
        {
            var tcs = new TaskCompletionSource<bool>();
            dispathcher.RequestMainThreadAction(() =>
            {
                var rootController = UIApplication.SharedApplication.KeyWindow.RootViewController;
                if (rootController == null)
                {
                    tcs.TrySetResult(false);
                    return;
                }

                UIAlertController alert = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
                var okAction = UIAlertAction.Create(ok, UIAlertActionStyle.Default, act => tcs.TrySetResult(true));
                alert.AddAction(okAction);
                rootController.PresentViewController(alert, true, null);
            });

            return tcs.Task;
        }

        public async Task<bool> ConfirmAsync(string title, string message, string accept = Text.Accept, string decline = Text.Decline)
        {
            var tcs = new TaskCompletionSource<bool>();
            dispathcher.RequestMainThreadAction(() =>
            {
                var rootController = UIApplication.SharedApplication.KeyWindow.RootViewController;
                if (rootController == null)
                {
                    tcs.TrySetResult(false);
                    return;
                }

                UIAlertController actionSheet = UIAlertController.Create(title, message, UIAlertControllerStyle.ActionSheet);
                var acceptAction = UIAlertAction.Create(accept, UIAlertActionStyle.Default, action => tcs.TrySetResult(true));
                var declineAction = UIAlertAction.Create(decline, UIAlertActionStyle.Destructive, action => tcs.TrySetResult(false));
                actionSheet.AddAction(acceptAction);
                actionSheet.AddAction(declineAction);
                rootController.PresentViewController(actionSheet, true, null);
            });

            return await tcs.Task;
        }
    }
}
