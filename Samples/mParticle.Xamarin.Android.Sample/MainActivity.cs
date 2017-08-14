using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using mParticle.Xamarin.Sample.Shared;

namespace mParticle.Xamarin.Android.Sample
{
    [Activity(Label = "SampleAndroid", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            SampleCalls.MakeTestCalls();
		}
    }
}

