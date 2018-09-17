using System;
using System.Threading.Tasks;
using static CarDetector.Constants;

namespace CarDetector.Interfaces
{
    public interface IDialogService
    {
        Task ShowAsync(string title, string message, string ok = Text.Ok);
        Task<bool> ConfirmAsync(string title, string message, string constructive = Text.Accept, string destructive = Text.Decline);
    }
}
