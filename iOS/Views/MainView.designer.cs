// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace CarDetector.iOS
{
	[Register ("MainView")]
	partial class MainView
	{
		[Outlet]
		UIKit.UIView cameraView { get; set; }

		[Outlet]
		UIKit.UIButton captureButton { get; set; }

		[Outlet]
		UIKit.UIButton closeButton { get; set; }

		[Outlet]
		UIKit.UIView overlayView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (cameraView != null) {
				cameraView.Dispose ();
				cameraView = null;
			}

			if (captureButton != null) {
				captureButton.Dispose ();
				captureButton = null;
			}

			if (closeButton != null) {
				closeButton.Dispose ();
				closeButton = null;
			}

			if (overlayView != null) {
				overlayView.Dispose ();
				overlayView = null;
			}
		}
	}
}
