using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Timers;

namespace mParticle.Xamarin.Sample.Shared
{
    public class SampleCalls
    {
        private const string ConstantUserAttribute = "Test Attribute Key";
        public static void Init()
        {
            string key = "";
            string secret = "";
#if __IOS__
            key = "b757fcf46cba0149bcf73628d440e6a7";
            secret = "3ucgOsSxomouHiQ1ZjAFRjraJ5E4Wf007bJfO7B5qxHccRtdvdPFqE3vesg2N3Hj";
#elif __ANDROID__
            key = "da3b3e96cc9bad469f536fd39b1fc987";
            secret = "dGV0TOQ7FsrCMjIlMe252cestFrJ6vAYiqnP3Mosbeo3d1NBgZMcDqx6wEG9OgxZ";
#endif
            OnUserIdentified _identityStateListener = null;
            _identityStateListener = newUser =>
            {
                MParticle.Instance.Identity.RemoveIdentityStateListener(_identityStateListener);
                    if (newUser != null)
                {
                    Console.WriteLine("New User Identified\n" + newUser.ToString());
                    newUser.SetUserAttribute("uattr_array", new string[] { "You get an attribute", "And you get an atrribute" }.ToList().Aggregate((arg1, arg2) => arg1 + arg2 + ", "));
                    newUser.SetUserAttribute(ConstantUserAttribute, "Test Attribute Value");

                    //set User Tag
                    newUser.SetUserTag("Something Completely New");
                    ModifyUser();
                }
                else
                {
                    Console.WriteLine("New User is null!");
                    throw new Exception("User Should not be null");
                }
            };

            MParticle.Instance.Initialize(new MParticleOptions()
            {
                InstallType = InstallType.KnownUpgrade,
                Environment = Environment.Development,
                ApiKey = key,
                ApiSecret = secret,
                IdentifyRequest = new IdentityApiRequest()
                {
                    UserIdentities = new Dictionary<UserIdentity, string>() {
                        //{ UserIdentity.Yahoo, "tom@yahoo.com" },
                        { UserIdentity.IOSAdvertiserId, "C56A4180-65AA-42EC-A945-5FD21DEC0538" },
                        { UserIdentity.CustomerId, "Other Identity" }
                    },
                    UserAliasHandler = ((previousUser, newUser) => newUser.SetUserAttributes(previousUser.GetUserAttributes()))
                },
                DevicePerformanceMetricsDisabled = false,
                IdDisabled = false,
                UploadInterval = 650,
                SessionTimeout = 50,
                UnCaughtExceptionLogging = false,
                LogLevel = LogLevel.INFO,
                AttributionListener = new AttributionListener()
                {
                    OnAttributionError = error => Console.WriteLine("AttributionError\n" + "Error Message = " + error.Message + "\nService Provider = " + error.ServiceProviderId),
                    OnAttributionResult = result => Console.WriteLine("AttributionResult\n" + "LinkUrl = " + result.LinkUrl + "\nParameters" + result.Parameters + "\nService Provider" + result.ServiceProviderId)
                },
                LocationTracking = new LocationTracking("GPS", 100, 350, 22),
                PushRegistration = new PushRegistration()
                {
                    AndroidSenderId = "12345-abcdefg",
                    AndroidInstanceId = "andriod-secret-instance-id",
                    IOSToken = "09876654321qwerty"
                },
                IdentityStateListener = _identityStateListener
            });
        }

        public static void MakeTestCalls()
        {

            var mparticle = MParticle.Instance;

            Console.WriteLine(mparticle.Environment);
            mparticle.SetOptOut(false);

            mparticle.LogEvent("AppEvent of type location", EventType.Location, new Dictionary<string, string>() { { "Cool", "Beans" } });
            mparticle.LogEvent("AppEvent of type navigation", EventType.Navigation, new Dictionary<string, string>() { { "Cool", "Beans" } });
            mparticle.LogEvent("AppEvent of type other", EventType.Other, new Dictionary<string, string>() { { "Cool", "Beans" } });
            mparticle.LogEvent("AppEvent of type search", EventType.Search, new Dictionary<string, string>() { { "Cool", "Beans" } });
            mparticle.LogEvent("AppEvent of type social", EventType.Social, new Dictionary<string, string>() { { "Cool", "Beans" } });
            mparticle.LogEvent("AppEvent of type transaction", EventType.Transaction, new Dictionary<string, string>() { { "Cool", "Beans" } });
            mparticle.LogEvent("AppEvent of type usercontent", EventType.UserContent, new Dictionary<string, string>() { { "Cool", "Beans" } });
            mparticle.LogEvent("AppEvent of type userpreference", EventType.UserPreference, new Dictionary<string, string>() { { "Cool", "Beans" } });

            mparticle.Upload();

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

            mparticle.SetATTStatus(MPATTAuthorizationStatus.Authorized, null);

#if __ANDROID__
            // This is highly discouraged and we make no guarantees about this but just to show it is possible.
            //var unsafeNativeSDK = mparticle.GetBindingInstance();
            //unsafeNativeSDK.GetType().GetMethod("setOptOut").Invoke(unsafeNativeSDK, new object[] { true });
#endif
        }


        public static void ModifyUser()
        {
            Thread.Sleep(2000);
            MParticle.Instance.Identity.Modify(new IdentityApiRequest()
            {
                UserIdentities = new Dictionary<UserIdentity, string>()
                {
                    { UserIdentity.CustomerId, "SomeCustomer123" },
                    //{ UserIdentity.Email, "MyEmail@myemail.xyz" },
                    //{ UserIdentity.Microsoft, "FB" }
                },
                UserAliasHandler = (previousUser, newUser) =>
                {
                    if (newUser.GetUserIdentities().GetValueOrDefault(UserIdentity.CustomerId).Equals("SomeCustomer123") &&
                        newUser.GetUserIdentities().GetValueOrDefault(UserIdentity.Email).Equals("MyEmail@myemail.xyz") &&
                        newUser.GetUserIdentities().GetValueOrDefault(UserIdentity.Other).Equals("OtherVal") &&
                        newUser.GetUserIdentities().GetValueOrDefault(UserIdentity.Microsoft).Equals("FB"))
                    {
                        Console.WriteLine("Incorrect User Identities");
                    }
                }
            })
                     .AddFailureListener(result =>
                    {
                        Console.WriteLine("ModifyError!!");
                        LoginNewUser();
                    })
                     .AddSuccessListener(user =>
                    {
                        Console.WriteLine("Modified User Identified\n" + user.ToString());
                        LoginNewUser();
                    });
        }

        public static void LoginNewUser() 
        {
            Thread.Sleep(2000);
            var mparticle = MParticle.Instance;
            mparticle.Identity.Login(new IdentityApiRequest(mparticle.Identity.CurrentUser)
            {
                UserAliasHandler = (previousUser, newUser) =>
                {
                    if (previousUser.GetUserAttributes().ContainsKey(ConstantUserAttribute))
                    {
                        newUser.GetUserAttributes().TryAdd(ConstantUserAttribute, previousUser.GetUserAttributes().GetValueOrDefault(ConstantUserAttribute));
                    }
                },
            })
                     .AddFailureListener(failure => Console.WriteLine("Http Code = " + failure.HttpCode + "/nErrors = " + failure.Errors.Aggregate("", (composit, nextError) => composit += nextError.Message + ", ")))
                     .AddSuccessListener(success => Console.WriteLine("Task callback" + success.User != null ? success.User.ToString() : "User is null :<("));


            mparticle.Identity.CurrentUser.GetUserAttributes().Remove(ConstantUserAttribute);
            //mparticle.Identity.Logout();
            mparticle.SetOptOut(false);
        }
    }
}