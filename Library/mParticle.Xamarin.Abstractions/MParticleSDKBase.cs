using System;
using System.Collections.Generic;

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
		CustomerId,
		Facebook,
		Twitter,
		Google,
		Microsoft,
		Yahoo,
		Email,
		Alias,
		FacebookCustomAudienceId
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

	public interface IMParticleSDK
	{
		void LogEvent(string eventName, EventType eventType, Dictionary<string, string> eventInfo = null);

		void LogCommerceEvent(CommerceEvent commerceEvent);

		void LogScreen(string screenName, Dictionary<string, string> eventInfo = null);

		void SetUserAttribute(string key, string val);

		void SetUserAttributeArray(string key, string[] values);

		void SetUserIdentity(string identity, UserIdentity identityType);

		void SetUserTag(string tag);

		void RemoveUserAttribute(string key);

		int IncrementUserAttribute(string key, int incrementValue);

		void LeaveBreadcrumb(string breadcrumbName);

		void SetOptOut(bool optOut);

		void Logout();

		Environment GetEnvironment();

		void Initialize(string apiKey, string apiSecret);

        object GetBindingInstance();
	}

    public abstract class MParticleSDKBase : IMParticleSDK
    {
        /// <summary>
        /// Gets the environment.
        /// </summary>
        /// <returns>The environment.</returns>
        public abstract Environment GetEnvironment();

        /// <summary>
        /// Increments the user attribute.
        /// </summary>
        /// <returns>The user attribute.</returns>
        /// <param name="key">Key.</param>
        /// <param name="incrementValue">Increment value.</param>
        public abstract int IncrementUserAttribute(string key, int incrementValue);

        /// <summary>
        /// Initialize the specified apiKey and apiSecret.
        /// </summary>
        /// <returns>The initialize.</returns>
        /// <param name="apiKey">API key.</param>
        /// <param name="apiSecret">API secret.</param>
        public abstract void Initialize(string apiKey, string apiSecret);

        /// <summary>
        /// Leaves the breadcrumb.
        /// </summary>
        /// <param name="breadcrumbName">Breadcrumb name.</param>
        public abstract void LeaveBreadcrumb(string breadcrumbName);

        /// <summary>
        /// Logs the commerce event.
        /// </summary>
        /// <param name="commerceEvent">Commerce event.</param>
        public abstract void LogCommerceEvent(CommerceEvent commerceEvent);

        /// <summary>
        /// Logs the event.
        /// </summary>
        /// <param name="eventName">Event name.</param>
        /// <param name="eventType">Event type.</param>
        /// <param name="eventInfo">Event info.</param>
        public abstract void LogEvent(string eventName, EventType eventType, Dictionary<string, string> eventInfo = null);

        /// <summary>
        /// Logout this instance.
        /// </summary>
        public abstract void Logout();

        /// <summary>
        /// Logs the screen.
        /// </summary>
        /// <param name="screenName">Screen name.</param>
        /// <param name="eventInfo">Event info.</param>
        public abstract void LogScreen(string screenName, Dictionary<string, string> eventInfo = null);

        /// <summary>
        /// Removes the user attribute.
        /// </summary>
        /// <param name="key">Key.</param>
        public abstract void RemoveUserAttribute(string key);

        /// <summary>
        /// Sets the opt out.
        /// </summary>
        /// <param name="optOut">If set to <c>true</c> opt out.</param>
        public abstract void SetOptOut(bool optOut);

        /// <summary>
        /// Sets the user attribute.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="val">Value.</param>
        public abstract void SetUserAttribute(string key, string val);

        /// <summary>
        /// Sets the user attribute array.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="values">Values.</param>
        public abstract void SetUserAttributeArray(string key, string[] values);

        /// <summary>
        /// Sets the user identity.
        /// </summary>
        /// <param name="identity">Identity.</param>
        /// <param name="identityType">Identity type.</param>
        public abstract void SetUserIdentity(string identity, UserIdentity identityType);

        /// <summary>
        /// Sets the user tag.
        /// </summary>
        /// <param name="tag">Tag.</param>
        public abstract void SetUserTag(string tag);

		/// <summary>
		/// Gets the C# bound sdk for the current platform. Use this with caution as you must rely on refelection and it is much more prone to error.
		/// </summary>
		/// <returns>The platform specific C# binding instance</returns>
		public abstract object GetBindingInstance();
    }
}
