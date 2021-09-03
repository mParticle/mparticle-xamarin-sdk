using System;
using mParticle.Xamarin.Sample.Shared;
using UIKit;
using CoreGraphics;

namespace mParticle.Xamarin.iOS.Sample
{
	public partial class ViewController : UIViewController
	{
		UIButton initButton = new UIButton(UIButtonType.System);
		UIButton testButton = new UIButton(UIButtonType.System);

		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			initButton.Frame = new CGRect(25, 125, 300, 50);
			initButton.SetTitle("Initialize", UIControlState.Normal);
			Add(initButton);
			initButton.TouchUpInside += (sender, e) => SampleCalls.Init();

			testButton.Frame = new CGRect(25, 200, 300, 50);
			testButton.SetTitle("Make Test Calls", UIControlState.Normal);
			Add(testButton);
			testButton.TouchUpInside += (sender, e) => SampleCalls.MakeTestCalls();
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}


	}
}