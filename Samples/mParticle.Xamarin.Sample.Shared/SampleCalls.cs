using System;
using System.Collections.Generic;
using System.Timers;

namespace mParticle.Xamarin.Sample.Shared
{
    public class SampleCalls
    {
        public static void Init()
        {
            #if __IOS__
                MParticle.Instance.Initialize("<YOURAPIKEY>", "<YOURSECRET>");
            #elif __ANDROID__
                MParticle.Instance.Initialize("<YOURAPIKEY>", "<YOURSECRET>");
            #endif
        }

        public static void MakeTestCalls()
        {

            var mparticle = mParticle.Xamarin.MParticle.Instance;

			Console.WriteLine(mparticle.GetEnvironment());
            mparticle.SetOptOut(false);

            mparticle.LogEvent("AppEvent of type location", EventType.Location, new Dictionary<string, string>() { { "Cool", "Beans" } });
            mparticle.LogEvent("AppEvent of type navigation", EventType.Navigation, new Dictionary<string, string>() { { "Cool", "Beans" } });
            mparticle.LogEvent("AppEvent of type other", EventType.Other, new Dictionary<string, string>() { { "Cool", "Beans" } });
            mparticle.LogEvent("AppEvent of type search", EventType.Search, new Dictionary<string, string>() { { "Cool", "Beans" } });
            mparticle.LogEvent("AppEvent of type social", EventType.Social, new Dictionary<string, string>() { { "Cool", "Beans" } });
            mparticle.LogEvent("AppEvent of type transaction", EventType.Transaction, new Dictionary<string, string>() { { "Cool", "Beans" } });
            mparticle.LogEvent("AppEvent of type usercontent", EventType.UserContent, new Dictionary<string, string>() { { "Cool", "Beans" } });
            mparticle.LogEvent("AppEvent of type userpreference", EventType.UserPreference, new Dictionary<string, string>() { { "Cool", "Beans" } });

            mparticle.SetUserIdentity("SomeCustomer123", UserIdentity.CustomerId);
            mparticle.SetUserIdentity("MyEmail@myemail.xyz", UserIdentity.Email);
            mparticle.SetUserIdentity("OtherVal", UserIdentity.Other);
            mparticle.SetUserIdentity("AliasVal", UserIdentity.Alias);
            mparticle.SetUserIdentity("FB", UserIdentity.Facebook);
            mparticle.SetUserIdentity("FBC", UserIdentity.FacebookCustomAudienceId);
            mparticle.SetUserIdentity("OG", UserIdentity.Google);
            mparticle.SetUserIdentity("M$FT", UserIdentity.Microsoft);
            mparticle.SetUserIdentity("Tweety", UserIdentity.Twitter);
            mparticle.SetUserIdentity("Yippy Kayay", UserIdentity.Yahoo);

            mparticle.SetUserAttributeArray("uattr_array", new string[] { "You get an attribute", "And you get an atrribute" });
            mparticle.SetUserAttribute("Test Attribute Key", "Test Attribute Value");
            mparticle.SetUserTag("Something Completely New");

            var product = new Product("Chicken", "SomeSku", 123.43, 22.14) { Brand = "mybrand", Category = "mycategory", CouponCode = "mycoupon", Variant = "myvariant", Position = 12, customAttributes = new Dictionary<string, string>() { { "Tk1", "Tv1" }, { "Tk2", "Tv2" } } };
            var product2 = new Product("Noodles", "CoolSku", 12.4, 52.4) { Brand = "mybrand2", Category = "mycategory2", CouponCode = "mycoupon2", Variant = "myvariant2", Position = 3, customAttributes = new Dictionary<string, string>() { { "NTk1", "NTv1" }, { "NTk2", "NTv2" } } };
            var products = new Product[] { product, product2 };
            var transactionAttributes = new TransactionAttributes("transactionId123") { Affiliation = "Some Affiliation", CouponCode = "Winner", Revenue = 400.21, Tax = 434.98, Shipping = 5.13 };

            mparticle.LogCommerceEvent(new CommerceEvent(ProductAction.AddToCart, products) { Currency = "AUD" });
            mparticle.LogCommerceEvent(new CommerceEvent(ProductAction.Purchase, products, transactionAttributes) { CustomAttributes = new Dictionary<string, string> { { "TestCommerceAttr", "TestCommerceVal" } } });
            mparticle.LogCommerceEvent(new CommerceEvent(ProductAction.Checkout, products, transactionAttributes) { CheckoutStep = 2 });
            mparticle.LogCommerceEvent(new CommerceEvent(ProductAction.Refund, products, transactionAttributes) { ScreenName = "Goodbye" });
            mparticle.LogCommerceEvent(new CommerceEvent(ProductAction.RemoveFromCart, products) { NonInteractive = true });
            mparticle.LogCommerceEvent(new CommerceEvent(ProductAction.ViewDetail, products) { CheckoutOptions = "-very -good" });
            mparticle.LogCommerceEvent(new CommerceEvent(ProductAction.AddToWishlist, products));
            mparticle.LogCommerceEvent(new CommerceEvent(ProductAction.RemoveFromWishlist, products));

            mparticle.LogCommerceEvent(new CommerceEvent(new Impression[] { new Impression("testIMpression", products), new Impression("testIMpression2", products) }));
            mparticle.LogCommerceEvent(new CommerceEvent(PromotionAction.View, new Promotion[] { new Promotion("someid", "somename", "somecreative", 123) }));

            mparticle.LogScreen("Home Screen", new Dictionary<string, string>() { { "EventAttributeKey", "EventAttributeValue" } });
            mparticle.LogScreen("Another screen");
            mparticle.LeaveBreadcrumb("crumble");
            mparticle.IncrementUserAttribute("Dragon Ball Z", 9001);

            var timer = new Timer(5000);
            timer.Elapsed += (sender, e) =>
            {
                timer.Stop();
                mparticle.RemoveUserAttribute("Test Attribute Key");
                mparticle.Logout();
                mparticle.SetOptOut(true);
            };
            timer.Start();

            #if __ANDROID__
	            // This is highly discouraged and we make no guarantees about this but just to show it is possible.
	            var unsafeNativeSDK = mparticle.GetBindingInstance();
	            unsafeNativeSDK.GetType().GetMethod("RemoveUserIdentity").Invoke(unsafeNativeSDK, new object[] { "SomeCustomer123" });
            #endif
		}
    }
}
