using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CarDetector.Interfaces;
using CarDetector.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Newtonsoft.Json;
using static CarDetector.Constants;

namespace CarDetector.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private DataProvider _dataProvider = new DataProvider();

        public ICameraService CameraService { get; } = Mvx.Resolve<ICameraService>();

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }
        

        public ICommand CaptureCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }
        
        public MainViewModel()
        {
            CaptureCommand = new MvxAsyncCommand(OnCapture);
            CloseCommand = new MvxCommand(OnClose);
        }
        
        private async Task OnCapture()
        {
            try
            {
                var photoBytes = await CameraService.GetPhotoBytesAsync();
                FileService.WriteAllBytes(photoBytes, Constants.ImageFileName);
                IsBusy = true;
                var aiResponse = await _dataProvider.GetAiResponse(photoBytes);
                IsBusy = false;
                if (!aiResponse.IsSuccess)
                {
                    await DialogService.ShowAsync(Text.Error, aiResponse.Error);
                    return;
                }
                ShowViewModel<ResultViewModel>(new {aiResponseString = JsonConvert.SerializeObject(aiResponse)});
            }
            catch (Exception ex)
            {
                await DialogService.ShowAsync(Text.Error, ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        private void OnClose()
        {
            PlatfromService.QuitApplication();
        }
    }
}
