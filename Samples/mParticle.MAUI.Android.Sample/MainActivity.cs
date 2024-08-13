﻿using Android.Views;
using Android.App;
using Android.Widget;
using Android.OS;
using Button = Android.Widget.Button;

namespace mParticle.MAUI.Android.Sample;

[Activity(Label = "@string/app_name", MainLauncher = true)]
public class MainActivity : Activity
{
    //protected override void OnCreate(Bundle? savedInstanceState)
    //{
    //    base.OnCreate(savedInstanceState);

    //    // Set our view from the "main" layout resource
    //    SetContentView(Resource.Layout.activity_main);
    //}

    Button Login, Modify, MakeTestCalls, Initialize, OptedInButton;
    TextView OptedInText;
    bool optedOut = true;

    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);


        // Set our view from the "main" layout resource
        SetContentView(Resource.Layout.activity_main);

        FindViewById<Button>(Resource.Id.add_to_cart_btn).Click += (sender, e) => BuyStuff();
        FindViewById<Button>(Resource.Id.upload_btn).Click += (sender, e) => MParticle.Instance.Upload();
        Initialize = FindViewById<Button>(Resource.Id.initialize_btn);
        Initialize.Click += (sender, e) => Init();
        Login = FindViewById<Button>(Resource.Id.login_btn);
        Login.Click += (sender, e) => SampleCalls.LoginNewUser();
        Login.Visibility = ViewStates.Invisible;
        Modify = FindViewById<Button>(Resource.Id.modify_btn);
        Modify.Click += (sender, e) => SampleCalls.ModifyUser();
        Modify.Visibility = ViewStates.Invisible;
        MakeTestCalls = FindViewById<Button>(Resource.Id.test_calls_btn);
        MakeTestCalls.Click += (sender, e) => SampleCalls.MakeTestCalls();
        MakeTestCalls.Visibility = ViewStates.Invisible;
        OptedInButton = FindViewById<Button>(Resource.Id.opted_in_btn);
        OptedInText = FindViewById<TextView>(Resource.Id.opted_in_tv);
        OptedInButton.Click += (sender, e) =>
        {
            optedOut = !optedOut;
            MParticle.Instance.SetOptOut(optedOut);
            OptedInText.Text = optedOut.ToString();
            OptedInButton.Text = "Opt " + (optedOut ? "Out" : "In");
        };
    }

    public void BuyStuff()
    {
        MParticle.Instance.LogCommerceEvent(new CommerceEvent(ProductAction.AddToCart,
            new Product[]
        {
            new Product("Random Thing", "231123", 99.99, 3)
        }, new TransactionAttributes("999999")));
    }

    public void Init()
    {
        SampleCalls.Init();
        MakeTestCalls.Visibility = ViewStates.Visible;
        Modify.Visibility = ViewStates.Visible;
        Login.Visibility = ViewStates.Visible;
        Initialize.Visibility = ViewStates.Gone;
    }

}
