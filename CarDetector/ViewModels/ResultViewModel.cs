using System;
using System.Linq;
using System.Windows.Input;
using CarDetector.DataModels;
using CarDetector.Interfaces;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Newtonsoft.Json;
using static CarDetector.Constants;

namespace CarDetector.ViewModels
{
    public class ResultViewModel : BaseViewModel
    {
        private DetectedModel _detectedModel;
        public DetectedModel DetectedModel
        {
            get => _detectedModel;
            set
            {
                SetProperty(ref _detectedModel, value);

                RaisePropertyChanged(() => BrandName);
                RaisePropertyChanged(() => Model);
                RaisePropertyChanged(() => Probability);
            }
        }

        private byte[] _image;
        public byte[] Image
        {
            get => Image;
            set => SetProperty(ref _image, value);
        }

        public string BrandName => DetectedModel.MakeName;

        public string Model => DetectedModel.ModelName;

        public double Probability => DetectedModel.GenProb;

        private string _fullPath;
        public string FullPath
        {
            get => _fullPath;
            set => SetProperty(ref _fullPath, value);
        }

        private Bbox _bbox;
        public Bbox Bbox
        {
            get => _bbox;
            set => SetProperty(ref _bbox, value);
        }

        public ICommand CloseCommand { get; private set; }

        public ResultViewModel()
        {
            CloseCommand = new MvxCommand(OnClose);
        }

        public void Init(string aiResponseString)
        {
            try
            {
                var aiResponse = JsonConvert.DeserializeObject<AiResponse>(aiResponseString);
                DetectedModel = aiResponse.DetectedModels?.OrderByDescending(s => s.ModelProb).First();
                FullPath = Mvx.Resolve<IFileService>().GetFullPathIfExists("image.png");
                var detectedObject = aiResponse.DetectedObjects?.First();
                Bbox = detectedObject.GetBbox();
            }
            catch(Exception ex)
            {
                DialogService.ShowAsync(Text.Error, ex.Message);
            }
        }
        
        private void OnClose()
        {
            Close(this);
        }
    }
}
