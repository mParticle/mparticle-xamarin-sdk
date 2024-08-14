using UIKit;
using Foundation;

namespace mParticle.MAUI.iOS.Sample;

[Register ("AppDelegate")]
public class AppDelegate : UIApplicationDelegate {
	public override UIWindow? Window {
		get;
		set;
	}

	public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
	{
		// create a new window instance based on the screen size
		Window = new UIWindow (UIScreen.MainScreen.Bounds);

		// set root view controller
		Window.RootViewController = new ViewController();

		// make the window visible
		Window.MakeKeyAndVisible ();

		return true;
	}
}

