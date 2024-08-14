using System;
using Android.App;
using Android.Runtime;

namespace mParticle.Xamarin.Android.Sample
{
	[Application]
	public class MyApplication : Application
	{
		public MyApplication(IntPtr handle, JniHandleOwnership ownerShip) : base(handle, ownerShip)
		{
		}
	}
}
