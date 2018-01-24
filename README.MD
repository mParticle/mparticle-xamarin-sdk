# mParticle Xamarin SDK

This is the repository of the mParticle Xamarin SDK, which is compatible with iOS and Android apps. The SDK consists of a common C# API layer and two Xamarin binding libraries for mParticle's native iOS and Android SDKs.

## Installation

This SDK is distributed via the public NuGet gallery: https://www.nuget.org/packages/mParticle.Xamarin/. 

If you have both an iOS and an Android project, you should add the NuGet package to each following the Xamarin Add Package dialog. [See here](https://developer.xamarin.com/guides/cross-platform/xamarin-studio/nuget_walkthrough/) for a detailed walkthrough on using NuGet with a Xamarin project.

```bash
Install-Package mParticle.Xamarin -Version 2.0.0
```

### mParticle Singleton

The mParticle.Xamarin package contains a class file `MParticle.cs`, which exposes the mParticle API via `MParticle.Instance`.

### Initialization

The SDK must be initialized with your mParticle workspace key and secret prior to use. This call should be placed in your Xamarin application initialization, *as early as possible*:

```cs
using mParticle.Xamarin;
namespace MyProject
{
    public class Example
    {
        void Example ()
        {
         //use the correct workspace API key and secret for iOS and Android
         string apiKey = "";
         string apiSecret = "";
         #if __ANDROID__
				apiKey = "REPLACE ME ANDROID KEY", ;
				apiSecret = "REPLACE ME ANDROID SECRET"
         #elif __IOS__
          		apiKey = "REPLACE ME IOS KEY", ;
				apiSecret = "REPLACE ME IOS SECRET"         
		 #endif
		 	
	 	new MParticleSDK().Initialize(new MParticleOptions()
        {
        	ApiKey = apiKey,
        	ApiSecret = apiSecret
        });
    }
}
```

### Events

Events are central to many of mParticle's integrations; analytics integrations typically require events, and you can create mParticle Audiences based on the recency and frequency of different events.

#### App Events

App Events represent specific actions that a user has taken in your app. At minimum they require a name and a type, but can also be associated with a free-form dictionary of key/value pairs.

```cs
MParticle.Instance.LogEvent (
        "Hello world", 
        EventType.Navigation, 
        new Dictionary<string, string>{{ "foo", "bar" }}
);
```
#### Commerce Events

The `CommerceEvent` is central to mParticle's eCommerce measurement. `CommerceEvents` can contain many data points but it's important to understand that there are 3 core variations:

- Product-based: Used to measure datapoints associated with one or more products
- Promotion-based: Used to measure datapoints associated with internal promotions or campaigns
- Impression-based: Used to measure interactions with impressions of products and product-listings

Here's an example of a Product-based purchase event:

```cs
Product[] products = new Product[2];
products[0] = new Product("foo name", "foo sku", 42, 2);
products[0].Brand = "foo brand";
products[0].Category = "foo category";
products[0].CouponCode = "foo coupon";

products[1] = new Product("foo name 2", "foo sku 2", 100, 3);
products[1].Brand = "foo brand 2";
products[1].Category = "foo category 2";
products[1].CouponCode = "foo coupon 2";

TransactionAttributes transactionAttributes = new TransactionAttributes("foo transaction id");
transactionAttributes.Revenue = 180;
transactionAttributes.Shipping = 10;
transactionAttributes.Tax = 15;
transactionAttributes.Affiliation = "foo affiliation";
transactionAttributes.CouponCode = "foo coupon code";
CommerceEvent eCommEvent = new CommerceEvent (
    ProductAction.Purchase, 
    products, 
    transactionAttributes
);
MParticle.Instance.LogCommerceEvent(eCommEvent);       
```
#### Screen events

```cs
MParticle.Instance.LogScreenEvent
(
    "Test screen", 
    new Dictionary<string, string>{{ "Test key 1", "Test value 1" }}
);
```

### Identity

Version 2 of the MParticle Xamarin SDK supports the full range of Identity Sync features. For more indepth information about MParticle's Identity features, checkout either the [Android docs](http://docs.mparticle.com/developers/sdk/android/identity/) or the [iOS docs](http://docs.mparticle.com/developers/sdk/ios/identity) on the topic


#### Accessing IdentityApi

To get a reference to the IdentityApi

```cs
var identityApi = MParticle.Instance.Identity;
```

#### Updating Identities 


User identities allow you to associate specific identifiers with the current user: 

```cs
identityApi.CurrentUser.UserIdentities.Add(UserIdentity.CustomerId, "customerId");
identityApi.CurrentUser.UserIdentities.Remove(UserIdentity.Email);

identityApi.Modify(new IdentityApiRequest(identity.CurrentUser));
```

or

```cs
identityApi.Modify(new IdentityApiRequest() 
                {
                    UserIdentities = new Dictionary<UserIdentity, string>()
                    {
                        { UserIdentity.CustomerId, "" }
                    }
                });
```

In addition to this, the underlying iOS and Android SDKs will automatically collect device IDs.

#### User Attributes

User attributes allow for free form description of a user for segmentation and analytics:

```cs
identityApi.CurrentUser.UserAttributes.Add("foo attribute", "bar value");
```

```cs
identityApi.CurrentUser.SetUserTag("foo tag");
```

```cs
identityApi.CurrentUser.UserAttributes.Remove("foo attribute");
```

#### Identity Change Callbacks

Since the IdentityApi calls `identify`, `login()`, `logout()`, and `modify` are asynchronous, you can register a callback for the results of the request

```
 identityApi.Logout()
         .AddSuccessListener(success => Console.WriteLine(success.User.UserAttributes))
         .AddFailureListener(failure => Console.WriteLine("HttpCode = " + failure.HttpCode + "/nErrors = " + failure.Errors));
```

#### User Aliasing

When a new User is returned through and IdentityApi request, you may want to transition the SDK and data from the previous user to the new user.

```
identityApi.Login(new IdentityApiRequest()
            {
                UserAliasHandler = (previousUser, newUser) => 
                {
                    // do some stuff, but for example:
                    var persistentAttribute = "persistent user attribute";
                    if (previousUser.UserAttributes.ContainsKey(persistentAttribute))
                    {
                        newUser.UserAttributes.Add(persistentAttribute, previousUser.UserAttributes.GetValueOrDefault(persistentAttribute));
                    }
                }
            });
```

## Kit Integrations

While most of mParticle's integration are server side, several require additional client side libraries called kits. An mParticle Kit is composed of a class that typically wraps a 3rd-party SDK, and maps the mParticle API onto a that SDK's API.

**Android**

1. Create a new Xamarin Android binding project
2. Find the required Kit aar/artifact by navigating to the [mParticle Core repository](https://github.com/mParticle/mparticle-android-sdk#kits), which links to all kits on Maven Central.
3. Download the aar file directly from Maven Central.
2. Add the `.aar` file to the Jars folder and set the build action to LibraryProjectZip.
3. View the `POM` of the kit Maven artifact to see if it specifies a transitive dependency. Most kits specify a transitive dependency on a 3rd-party SDK.
3. Download the `.jar` or `.aar` file of the transitive dependency:
   * If this file is a jar, you can add to the same binding project in the Jars folder and set the build action to EmbeddedReferenceJar.
   * If the file is an aar, another binding project must be made and referenced by the kit binding project. (This is due to a limiting of Xamarin bindings).
4. Reference the binding project in your main Xamarin application.

To verify that the kit was successfully detected, look for a string that matches "[Service Provider Name] kit detected" so for instance "AppsFlyer Detected"

**iOS**

1. Compile a static version of the kit library that targets i386 and x86_64. All our kits are open source [located here](https://github.com/mparticle-integrations).
2. Compile or retrieve a static version of the service provider's library
3. You can add these as NativeReferences to your Xamarin iOS application

Additional information on troubleshooting Xamarin bindings can be found here for Android and iOS:

- Android: https://developer.xamarin.com/guides/android/advanced_topics/binding-a-java-library/troubleshooting-bindings/
- iOS: https://developer.xamarin.com/guides/ios/troubleshooting/mtouch-errors/

## Building this project

If you do not use NuGet in your Xamarin project, you can build this project manually:

1. Open the `.sln` file in Xamarin Studio, select `Release and Build`. Alternatively, run `msbuild mParticle.Xamarin.sln /p:Configuration=Release /t:Rebuild` from a terminal.
4. (Optional) Go to to the Library folder and run `nuget pack`. This will create the NuGet package.

## Testing install referrer on Android

In order for attribution, deep linking, and many other integrations to work properly, add the mParticle `ReferrerReceiver` to your manifest file within the `<application>` tag. The mParticle SDK will collect any campaign referral information and automatically forward it to kits (such as AppsFlyer, Kochava, and Adjust) and server-side integrations.

```xml
<receiver android:name="com.mparticle.ReferrerReceiver" android:exported="true">
    <intent-filter>
        <action android:name="com.android.vending.INSTALL_REFERRER"/>
    </intent-filter>
</receiver>
```

To test if the install referrer is working, run the following in the terminal: 
`adb shell`
`am broadcast -a com.android.vending.INSTALL_REFERRER -n com.companyname.SampleAndroid/com.mparticle.ReferrerReceiver --es "referrer" "utm_source=test_source\&utm_medium=test_medium\&utm_term=test_term\&utm_content=test_content\&utm_campaign=test_name"`

Then go to the mParticle Live Stream, click on the Android - Batch, click View Event and you should see your value in the device info.

## License

[Apache License 2.0](http://www.apache.org/licenses/LICENSE-2.0)
