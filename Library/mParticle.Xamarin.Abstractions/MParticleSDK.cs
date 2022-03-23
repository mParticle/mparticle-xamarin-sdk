using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;

namespace mParticle.Xamarin
{
    public sealed class CommerceEvent
    {
        public TransactionAttributes TransactionAttributes;
        public ProductAction ProductAction;
        public PromotionAction PromotionAction;
        public Product[] Products;
        public Promotion[] Promotions;
        public Impression[] Impressions;
        public string ScreenName;
        public string Currency;
        public Dictionary<string, string> CustomAttributes;
        public string CheckoutOptions;
        public string ProductActionListName;
        public string ProductActionListSource;
        public int? CheckoutStep;
        public bool? NonInteractive;

        public CommerceEvent(ProductAction productAction, Product[] products, TransactionAttributes transactionAttributes = null)
        {
            this.ProductAction = productAction;
            this.Products = products;
            this.TransactionAttributes = transactionAttributes;
        }

        public CommerceEvent(PromotionAction newPromotionAction, Promotion[] newPromotions)
        {
            this.PromotionAction = newPromotionAction;
            this.Promotions = newPromotions;
        }

        public CommerceEvent(Impression[] impressions)
        {
            this.Impressions = impressions;
        }
    }

    public sealed class Product
    {
        public string Name;
        public string Sku;
        public double Price;
        public double Quantity;
        public string Brand;
        public string CouponCode;
        public int? Position;
        public string Category;
        public string Variant;
        public Dictionary<string, string> customAttributes;

        private Product()
        {
        }

        public Product(string name, string sku, double price, double quantity)
        {
            this.Name = name;
            this.Sku = sku;
            this.Price = price;
            this.Quantity = quantity;
        }
    }

    public sealed class TransactionAttributes
    {
        public string TransactionId;
        public string Affiliation;
        public double? Revenue = null;
        public double? Shipping = null;
        public double? Tax = null;
        public string CouponCode;

        private TransactionAttributes() { }

        public TransactionAttributes(string transactionId)
        {
            this.TransactionId = transactionId;
        }
    }

    public sealed class Impression
    {
        public string ImpressionListName;
        public Product[] Products;

        private Impression() { }

        public Impression(string impressionListName, Product[] products)
        {
            this.ImpressionListName = impressionListName;
            this.Products = products;
        }
    }

    public sealed class Promotion
    {
        public string Id;
        public string Name;
        public string Creative;
        public int? Position;

        private Promotion() { }

        public Promotion(string id, string name, string creative, int? position)
        {
            this.Id = id;
            this.Name = name;
            this.Creative = creative;
            this.Position = position;
        }
    }

    public interface ICart
    {
        List<Product> GetProducts();
        void Add(Product product);
        void AddAll(List<Product> products, Boolean logEvent);
        void Clear();
        void Checkout();
        void Checkout(int step, String options);
        void GetProduct(String name);
        void Purchase(TransactionAttributes transactionAttributes, Boolean clear = false);
        void Refund(TransactionAttributes transactionAttributes, Boolean clear);
        void Remove(int index);
        void Remove(Product product);
        void SetMaximumProductCountAndroid(int max);
    }

    public sealed class MParticleOptions
    {
        public InstallType InstallType = InstallType.AutoDetect;
        public Environment Environment;
        public String ApiKey;
        public String ApiSecret;
        public IdentityApiRequest IdentifyRequest;
        public Boolean DevicePerformanceMetricsDisabled = false;
        public Boolean IdDisabled = false;
        public int UploadInterval = 600;  //seconds
        public int SessionTimeout = 60; //seconds
        public int? ConfigMaxAgeSeconds; // defaults to unlimited
        public Boolean UnCaughtExceptionLogging = false; // Android only
        public LogLevel LogLevel = LogLevel.DEBUG;
        public LocationTracking LocationTracking;
        public PushRegistration PushRegistration;
        public OnUserIdentified IdentityStateListener;
        public AttributionListener AttributionListener;
    }

    public sealed class IdentityApiRequest
    {
        public IdentityApiRequest()
        {

        }

        public IdentityApiRequest(MParticleUser user)
        {
            if (user != null)
            {
                UserIdentities = user.GetUserIdentities();
            }
        }

        public Dictionary<UserIdentity, String> UserIdentities = new Dictionary<UserIdentity, string>();
        public OnUserAlias UserAliasHandler;
    }

    public delegate void OnUserAlias(MParticleUser previousUser, MParticleUser newUser);

    public sealed class AttributionResult
    {
        public String Parameters;
        public int ServiceProviderId;
        public String LinkUrl = null;

    }

    public sealed class AttributionError
    {
        public String Message;
        public int ServiceProviderId;
    }

    public sealed class LocationTracking
    {
        public Boolean Enabled { get; }
        public String Provider { get; }
        public long MinTime { get; }
        public long MinDistance { get; }
        public long MinAccuracy { get; }

        public LocationTracking(Boolean enabled)
        {
            this.Enabled = enabled;
        }

        public LocationTracking(String provider, long minTime, long minDistance, long minAccuracy)
        {
            this.Enabled = true;
            this.Provider = provider;
            this.MinTime = minTime;
            this.MinDistance = minDistance;
            this.MinAccuracy = minAccuracy;
        }
    }

    public sealed class PushRegistration
    {
        public String AndroidSenderId;
        public String AndroidInstanceId;
        public String IOSToken;
    }

    public enum InstallType
    {
        AutoDetect,
        KnownInstall,
        KnownUpgrade
    }

    public enum LogLevel
    {
        NONE,
        /**
         * Used for critical issues with the SDK or its configuration.
         */
        ERROR,
        /**
         * (default) Used to warn developers of potentially unintended consequences of their use of the SDK.
         */
        WARNING,
        /**
         * Used to communicate the internal state and processes of the SDK.
         */
        DEBUG,
        /*
         * Used to relay fine-grained issues with the usage of the SDK
         */
        VERBOSE,
        /*
         * Used to communicate
         */
        INFO
    }


    public enum EventType
    {
        Navigation = 1,
        Location,
        Search,
        Transaction,
        UserContent,
        UserPreference,
        Social,
        Other
    };

    public enum UserIdentity
    {
        Other = 0,
        CustomerId = 1,
        Facebook = 2,
        Twitter = 3,
        Google = 4,
        Microsoft = 5,
        Yahoo = 6,
        Email = 7,
        Alias = 8,
        FacebookCustomAudienceId = 9,
        Other2 = 10,
        Other3 = 11,
        Other4 = 12,
        Other5 = 13,
        Other6 = 14,
        Other7 = 15,
        Other8 = 16,
        Other9 = 17,
        Other10 = 18,
        MobileNumber = 19,
        PhoneNumber2 = 20,
        PhoneNumber3 = 21,
        IOSAdvertiserId = 22,
        IOSVendorId = 23,
        PushToken = 24,
        DeviceApplicationStamp = 25
    };

    public enum Environment
    {
        AutoDetect = 0,
        Development,
        Production
    };

    public enum ProductAction
    {
        AddToCart = 1,
        RemoveFromCart,
        Checkout,
        CheckoutOption,
        Click,
        ViewDetail,
        Purchase,
        Refund,
        AddToWishlist,
        RemoveFromWishlist
    };

    public enum PromotionAction
    {
        View = 0,
        Click
    };

    public enum MPATTAuthorizationStatus
    {
        NotDetermined = 0,
        Restricted,
        Denied,
        Authorized
    }

    public static class UserAttribute
    {
        public const string
        FirstName = "$FirstName",
        LastName = "$LastName",
        Address = "$Address",
        State = "$State",
        City = "$City",
        Zipcode = "$Zipcode",
        Country = "$Country",
        Age = "$Age",
        Gender = "$Gender",
        MobileNumber = "$MobileNumber";
    };

    public abstract class MParticleUser
    {
        public abstract long Mpid { get; }

        public abstract void SetUserTag(String tag);

        public abstract Dictionary<UserIdentity, string> GetUserIdentities();

        public abstract Dictionary<string, string> GetUserAttributes();

        public abstract void SetUserAttributes(Dictionary<string, string> userAttributes);

        public abstract void SetUserAttribute(string key, string val);


        public override string ToString()
        {
            return "User: \n" + "\tMPID = " + Mpid + "\n\tUser Identitites = " + GetUserIdentities().Aggregate("", (aggrigate, pair) => aggrigate + pair.Key.ToString() + ":" + pair.Value + ", ") + "\n\tUser Attributes = " + GetUserAttributes().Aggregate("", (aggrigate, pair) => aggrigate + pair.Key.ToString() + ":" + pair.Value + ", ");
        }
    }

    public abstract class IdentityApi
    {
        public abstract MParticleUser CurrentUser { get; }
        public abstract void AddIdentityStateListener(OnUserIdentified listener);
        public abstract void RemoveIdentityStateListener(OnUserIdentified listener);
        public abstract IMParticleTask<IdentityApiResult> Identify(IdentityApiRequest request = null);
        public abstract IMParticleTask<IdentityApiResult> Login(IdentityApiRequest request = null);
        public abstract IMParticleTask<IdentityApiResult> Logout(IdentityApiRequest request = null);
        public abstract IMParticleTask<IdentityApiResult> Modify(IdentityApiRequest request);
    }

    public delegate void OnUserIdentified(MParticleUser user);

    public interface IMParticleTask<T>
    {
        Boolean IsComplete();
        Boolean IsSuccessful();
        T GetResult();
        IMParticleTask<T> AddSuccessListener(OnSuccess listener);
        IMParticleTask<T> AddFailureListener(OnFailure listener);
    }

    public sealed class IdentityApiResult
    {
        public MParticleUser User;
    }

    public sealed class AttributionListener
    {
        public OnAttributionError OnAttributionError;
        public OnAttributionResult OnAttributionResult;
    }

    public delegate void OnAttributionResult(AttributionResult result);
    public delegate void OnAttributionError(AttributionError error);

    public delegate void OnSuccess(IdentityApiResult result);
    public delegate void OnFailure(IdentityHttpResponse result);

    public class IdentityHttpResponse
    {
        public Boolean IsSuccessful;
        public List<Error> Errors = new List<Error>();
        public int HttpCode;
    }

    public sealed class Error
    {
        public String Message;
        public String Code;
    }

    public abstract class MParticleSDK
    {
        /// <summary>
        /// Gets the environment.
        /// </summary>
        /// <returns>The environment.</returns>
        public abstract Environment Environment { get; }

        /// <summary>
        /// Initialize the specified apiKey and apiSecret.
        /// </summary>
        /// <returns>The initialize.</returns>
        /// <param name="options">Start up options</param>
        public abstract MParticleSDK Initialize(MParticleOptions options);

        /// <summary>
        /// Leaves the breadcrumb.
        /// </summary>
        /// <param name="breadcrumbName">Breadcrumb name.</param>
        public abstract void LeaveBreadcrumb(string breadcrumbName);

        /// <summary>
        /// Logs the commerce event.
        /// </summary>
        /// <param name="commerceEvent">Commerce event.</param>
        /// <param name="shouldUploadEvent">Whether to upload event to mParticle or only pass to kits.</param>
        public abstract void LogCommerceEvent(CommerceEvent commerceEvent, bool shouldUploadEvent = true);

        /// <summary>
        /// Logs the event.
        /// </summary>
        /// <param name="eventName">Event name.</param>
        /// <param name="eventType">Event type.</param>
        /// <param name="eventInfo">Event info.</param>
        /// <param name="shouldUploadEvent">Whether to upload event to mParticle or only pass to kits.</param>
        public abstract void LogEvent(string eventName, EventType eventType, Dictionary<string, string> eventInfo = null, bool shouldUploadEvent = true);

        /// <summary>
        /// Logs the screen.
        /// </summary>
        /// <param name="screenName">Screen name.</param>
        /// <param name="eventInfo">Event info.</param>
        public abstract void LogScreen(string screenName, Dictionary<string, string> eventInfo = null);

        /// <summary>
        /// Logs the App Tracking Transparency Status.
        /// </summary>
        /// <param name="status">Status.</param>
        /// <param name="attStatusTimestampMillis">ATT Status Timestamp Millis.</param>
        public abstract void SetATTStatus(MPATTAuthorizationStatus status, long? attStatusTimestampMillis = null);

        /// <summary>
        /// Sets the opt out.
        /// </summary>
        /// <param name="optOut">If set to <c>true</c> opt out.</param>
        public abstract void SetOptOut(bool optOut);

        /// <summary>
        /// Gets the C# bound sdk for the current platform. Use this with caution as you must rely on refelection and it is much more prone to error.
        /// </summary>
        /// <returns>The platform specific C# binding instance</returns>
        public abstract object GetBindingInstance();

        /// <summary>
        /// Returns an implementation of the Identity Api
        /// </summary>
        /// <param name="tag">Tag.</param>
        public abstract IdentityApi Identity { get; }

        /// <summary>
        /// Sets the MParticleSDK instance to null
        /// </summary>
        public abstract void Destroy();


        /// <summary>
        ///  Forces an upload of recorded Events
        /// </summary>
        public abstract void Upload();

    }
}
