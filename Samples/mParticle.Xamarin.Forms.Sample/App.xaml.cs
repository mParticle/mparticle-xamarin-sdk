using mParticle.Xamarin.Sample.Shared;
using Xamarin.Forms;

namespace mParticle.Xamarin.Forms.Sample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

			SampleCalls.Init();

			MainPage = new mParticle_Xamarin_Forms_SamplePage();
        }

        protected override void OnStart()
        {
            SampleCalls.MakeTestCalls();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
