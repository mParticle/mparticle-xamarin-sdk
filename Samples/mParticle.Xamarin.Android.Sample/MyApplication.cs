using System;
using Android.App;
using Android.Runtime;
using mParticle.Xamarin.Sample.Shared;

namespace mParticle.Xamarin.Android.Sample
{
	[Application]
	public class MyApplication : Application
	{
		public MyApplication(IntPtr handle, JniHandleOwnership ownerShip) : base(handle, ownerShip)
		{
		}

		public override void OnCreate()
		{
            base.OnCreate();
			SampleCalls.Init();
		}
	}
}
