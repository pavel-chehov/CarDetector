using System;
using System.Threading;
using CarDetector.Interfaces;

namespace CarDetector.iOS.Services
{
    public class PlatformService : IPlatformService
    {
        public void QuitApplication()
        {
            Thread.CurrentThread.Abort();
        }
    }
}
