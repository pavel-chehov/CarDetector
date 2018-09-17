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
	[Register ("ResultView")]
	partial class ResultView
	{
		[Outlet]
		UIKit.UILabel brandLabel { get; set; }

		[Outlet]
		UIKit.UIButton doneButton { get; set; }

		[Outlet]
		UIKit.UIImageView imageView { get; set; }

		[Outlet]
		UIKit.UILabel modelLabel { get; set; }

		[Outlet]
		UIKit.UILabel probLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (brandLabel != null) {
				brandLabel.Dispose ();
				brandLabel = null;
			}

			if (imageView != null) {
				imageView.Dispose ();
				imageView = null;
			}

			if (modelLabel != null) {
				modelLabel.Dispose ();
				modelLabel = null;
			}

			if (probLabel != null) {
				probLabel.Dispose ();
				probLabel = null;
			}

			if (doneButton != null) {
				doneButton.Dispose ();
				doneButton = null;
			}
		}
	}
}
