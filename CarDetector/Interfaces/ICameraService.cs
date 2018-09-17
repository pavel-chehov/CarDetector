using System;
using System.Threading.Tasks;

namespace CarDetector.Interfaces
{
    public interface ICameraService
    {
        Task AuthorizeCameraUse();
        void SetupLiveCameraStream(object cameraView);
        Task<byte[]> GetPhotoBytesAsync();
    }
}
