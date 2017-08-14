using System;
using CoreLocation;
using Foundation;
using ObjCRuntime;
using UIKit;
using UserNotifications;

namespace mParticle.Xamarin.iOSBinding
{

	[BaseType(typeof(NSObject))]
	[Protocol]
	interface MPTransactionAttributes : INSCopying, INSCoding
	{
		// @property (nonatomic, strong) NSString * _Nullable affiliation;
		[NullAllowed, Export("affiliation", ArgumentSemantic.Strong)]
		string Affiliation { get; set; }

		// @property (nonatomic, strong) NSString * _Nullable couponCode;
		[NullAllowed, Export("couponCode", ArgumentSemantic.Strong)]
		string CouponCode { get; set; }

		// @property (nonatomic, strong) NSNumber * _Nullable shipping;
		[NullAllowed, Export("shipping", ArgumentSemantic.Strong)]
		NSNumber Shipping { get; set; }

		// @property (nonatomic, strong) NSNumber * _Nullable tax;
		[NullAllowed, Export("tax", ArgumentSemantic.Strong)]
		NSNumber Tax { get; set; }

		// @property (nonatomic, strong) NSNumber * _Nullable revenue;
		[NullAllowed, Export("revenue", ArgumentSemantic.Strong)]
		NSNumber Revenue { get; set; }

		// @property (nonatomic, strong) NSString * _Nullable transactionId;
		[NullAllowed, Export("transactionId", ArgumentSemantic.Strong)]
		string TransactionId { get; set; }
	}

	// @interface MPIHasher : NSObject
	[BaseType(typeof(NSObject))]
	[Protocol]
	interface MPIHasher
	{
		// +(uint64_t)hashFNV1a:(NSData *)data;
		[Static]
		[Export("hashFNV1a:")]
		ulong HashFNV1a(NSData data);

		// +(NSString *)hashString:(NSString *)stringToHash;
		[Static]
		[Export("hashString:")]
		string HashString(string stringToHash);
	}


	// @protocol MPExtensionProtocol <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject))]
	interface MPExtensionProtocol
	{
	}

	// @interface MPCommerceEvent : NSObject <NSCopying, NSCoding>
	[BaseType(typeof(NSObject))]
	[Protocol]
	interface MPCommerceEvent : INSCopying, INSCoding
	{
		// @property (nonatomic, strong) NSString * _Nullable checkoutOptions;
		[NullAllowed, Export("checkoutOptions", ArgumentSemantic.Strong)]
		string CheckoutOptions { get; set; }

		// @property (nonatomic, strong) NSString * _Nullable currency;
		[NullAllowed, Export("currency", ArgumentSemantic.Strong)]
		string Currency { get; set; }

		// @property (readonly, nonatomic, strong) NSDictionary<NSString *,__kindof NSSet<MPProduct *> *> * _Nullable impressions;
		[NullAllowed, Export("impressions", ArgumentSemantic.Strong)]
		NSDictionary<NSString, NSSet<MPProduct>> Impressions { get; }

		// @property (readonly, nonatomic, strong) NSArray<MPProduct *> * _Nullable products;
		[NullAllowed, Export("products", ArgumentSemantic.Strong)]
		MPProduct[] Products { get; }

		// @property (nonatomic, strong) MPPromotionContainer * _Nullable promotionContainer;
		[NullAllowed, Export("promotionContainer", ArgumentSemantic.Strong)]
		MPPromotionContainer PromotionContainer { get; set; }

		// @property (nonatomic, strong) NSString * _Nullable productListName;
		[NullAllowed, Export("productListName", ArgumentSemantic.Strong)]
		string ProductListName { get; set; }

		// @property (nonatomic, strong) NSString * _Nullable productListSource;
		[NullAllowed, Export("productListSource", ArgumentSemantic.Strong)]
		string ProductListSource { get; set; }

		// @property (nonatomic, strong) NSString * _Nullable screenName;
		[NullAllowed, Export("screenName", ArgumentSemantic.Strong)]
		string ScreenName { get; set; }

		// @property (nonatomic, strong) MPTransactionAttributes * _Nullable transactionAttributes;
		[NullAllowed, Export("transactionAttributes", ArgumentSemantic.Strong)]
		MPTransactionAttributes TransactionAttributes { get; set; }

		// @property (nonatomic, unsafe_unretained) MPCommerceEventAction action;
		[Export("action", ArgumentSemantic.Assign)]
		MPCommerceEventAction Action { get; set; }

		// @property (nonatomic, unsafe_unretained) NSInteger checkoutStep;
		[Export("checkoutStep")]
		nint CheckoutStep { get; set; }

		// @property (nonatomic, unsafe_unretained) BOOL nonInteractive;
		[Export("nonInteractive")]
		bool NonInteractive { get; set; }

		// -(instancetype _Nonnull)initWithAction:(MPCommerceEventAction)action product:(MPProduct * _Nullable)product;
		[Export("initWithAction:product:")]
		IntPtr Constructor(MPCommerceEventAction action, [NullAllowed] MPProduct product);

		// -(instancetype _Nonnull)initWithImpressionName:(NSString * _Nullable)listName product:(MPProduct * _Nullable)product;
		[Export("initWithImpressionName:product:")]
		IntPtr Constructor([NullAllowed] string listName, [NullAllowed] MPProduct product);

		// -(instancetype _Nonnull)initWithPromotionContainer:(MPPromotionContainer * _Nullable)promotionContainer;
		[Export("initWithPromotionContainer:")]
		IntPtr Constructor([NullAllowed] MPPromotionContainer promotionContainer);

		// -(void)addImpression:(MPProduct * _Nonnull)product listName:(NSString * _Nonnull)listName;
		[Export("addImpression:listName:")]
		void AddImpression(MPProduct product, string listName);

		// -(void)addProduct:(MPProduct * _Nonnull)product;
		[Export("addProduct:")]
		void AddProduct(MPProduct product);

		// -(void)removeProduct:(MPProduct * _Nonnull)product;
		[Export("removeProduct:")]
		void RemoveProduct(MPProduct product);

		// -(void)setCustomAttributes:(NSDictionary<NSString *,NSString *> * _Nullable)customAttributes;
		[Export("setCustomAttributes:")]
		void SetCustomAttributes([NullAllowed] NSDictionary<NSString, NSString> customAttributes);

		// -(NSArray * _Nullable)allKeys;
		[NullAllowed, Export("allKeys")]
		NSObject[] AllKeys { get; }

		// -(NSUInteger)count;
		[Export("count")]
		nuint Count { get; }

		// -(id _Nullable)objectForKeyedSubscript:(NSString *const _Nonnull)key;
		[Export("objectForKeyedSubscript:")]
		[return: NullAllowed]
		NSObject ObjectForKeyedSubscript(string key);

		// -(void)setObject:(id _Nonnull)obj forKeyedSubscript:(NSString * _Nonnull)key;
		[Export("setObject:forKeyedSubscript:")]
		void SetObject(NSObject obj, string key);
	}

	// @interface MPProduct : NSObject <NSCopying, NSCoding>
	[BaseType(typeof(NSObject))]
	[Protocol]
	interface MPProduct : INSCopying, INSCoding
	{
		// @property (nonatomic, strong) NSString * _Nullable brand;
		[NullAllowed, Export("brand", ArgumentSemantic.Strong)]
		string Brand { get; set; }

		// @property (nonatomic, strong) NSString * _Nullable category;
		[NullAllowed, Export("category", ArgumentSemantic.Strong)]
		string Category { get; set; }

		// @property (nonatomic, strong) NSString * _Nullable couponCode;
		[NullAllowed, Export("couponCode", ArgumentSemantic.Strong)]
		string CouponCode { get; set; }

		// @property (nonatomic, strong) NSString * _Nonnull name;
		[Export("name", ArgumentSemantic.Strong)]
		string Name { get; set; }

		// @property (nonatomic, strong) NSNumber * _Nullable price;
		[NullAllowed, Export("price", ArgumentSemantic.Strong)]
		NSNumber Price { get; set; }

		// @property (nonatomic, strong) NSString * _Nonnull sku;
		[Export("sku", ArgumentSemantic.Strong)]
		string Sku { get; set; }

		// @property (nonatomic, strong) NSString * _Nullable variant;
		[NullAllowed, Export("variant", ArgumentSemantic.Strong)]
		string Variant { get; set; }

		// @property (nonatomic, unsafe_unretained) NSUInteger position;
		[Export("position")]
		nuint Position { get; set; }

		// @property (nonatomic, strong) NSNumber * _Nonnull quantity;
		[Export("quantity", ArgumentSemantic.Strong)]
		NSNumber Quantity { get; set; }

		// -(instancetype _Nonnull)initWithName:(NSString * _Nonnull)name sku:(NSString * _Nonnull)sku quantity:(NSNumber * _Nonnull)quantity price:(NSNumber * _Nullable)price;
		[Export("initWithName:sku:quantity:price:")]
		IntPtr Constructor(string name, string sku, NSNumber quantity, [NullAllowed] NSNumber price);

		// -(NSArray * _Nonnull)allKeys;
		[Export("allKeys")]
		NSObject[] AllKeys { get; }

		// -(NSUInteger)count;
		[Export("count")]
		nuint Count { get; }

		// -(id _Nullable)objectForKeyedSubscript:(NSString *const _Nonnull)key;
		[Export("objectForKeyedSubscript:")]
		[return: NullAllowed]
		NSObject ObjectForKeyedSubscript(string key);

		// -(void)setObject:(id _Nonnull)obj forKeyedSubscript:(NSString * _Nonnull)key;
		[Export("setObject:forKeyedSubscript:")]
		void SetObject(NSObject obj, string key);

		// @property (nonatomic, strong) NSString * _Nullable affiliation __attribute__((deprecated("use MPTransactionAttributes.affiliation instead")));
		[NullAllowed, Export("affiliation", ArgumentSemantic.Strong)]
		string Affiliation { get; set; }

		// @property (nonatomic, strong) NSString * _Nullable currency __attribute__((deprecated("use MPCommerceEvent.currency instead")));
		[NullAllowed, Export("currency", ArgumentSemantic.Strong)]
		string Currency { get; set; }

		// @property (nonatomic, strong) NSString * _Nullable transactionId __attribute__((deprecated("use MPTransactionAttributes.transactionId instead")));
		[NullAllowed, Export("transactionId", ArgumentSemantic.Strong)]
		string TransactionId { get; set; }

		// @property (readwrite, nonatomic) double shippingAmount __attribute__((deprecated("use MPTransactionAttributes.shipping instead")));
		[Export("shippingAmount")]
		double ShippingAmount { get; set; }

		// @property (readwrite, nonatomic) double taxAmount __attribute__((deprecated("use MPTransactionAttributes.tax instead")));
		[Export("taxAmount")]
		double TaxAmount { get; set; }

		// @property (readwrite, nonatomic) double totalAmount __attribute__((deprecated("use MPTransactionAttributes.revenue instead")));
		[Export("totalAmount")]
		double TotalAmount { get; set; }

		// @property (readwrite, nonatomic) double unitPrice __attribute__((deprecated("use the price property instead")));
		[Export("unitPrice")]
		double UnitPrice { get; set; }
	}

	// @interface MPCart : NSObject <NSCoding>
	[BaseType(typeof(NSObject))]
	[Protocol]
	interface MPCart : INSCoding
	{
		// +(instancetype _Nonnull)sharedInstance;
		[Static]
		[Export("sharedInstance")]
		MPCart SharedInstance();

		// -(void)addProduct:(MPProduct * _Nonnull)product;
		[Export("addProduct:")]
		void AddProduct(MPProduct product);

		// -(void)clear;
		[Export("clear")]
		void Clear();

		// -(NSArray<MPProduct *> * _Nullable)products;
		[NullAllowed, Export("products")]
		MPProduct[] Products { get; }

		// -(void)removeProduct:(MPProduct * _Nonnull)product;
		[Export("removeProduct:")]
		void RemoveProduct(MPProduct product);
	}

	// @interface MPCommerceEventInstruction : NSObject
	[BaseType(typeof(NSObject))]
	[Protocol]
	interface MPCommerceEventInstruction
	{
		// @property (readonly, nonatomic, strong) MPEvent * event;
		[Export("event", ArgumentSemantic.Strong)]
		MPEvent Event { get; }

		// @property (readonly, nonatomic, strong) MPProduct * product;
		[Export("product", ArgumentSemantic.Strong)]
		MPProduct Product { get; }

		// @property (readonly, nonatomic, unsafe_unretained) MPCommerceInstruction instruction;
		[Export("instruction", ArgumentSemantic.Assign)]
		MPCommerceInstruction Instruction { get; }

		// -(instancetype)initWithInstruction:(MPCommerceInstruction)instruction event:(MPEvent *)event;
		[Export("initWithInstruction:event:")]
		IntPtr Constructor(MPCommerceInstruction instruction, MPEvent @event);

		// -(instancetype)initWithInstruction:(MPCommerceInstruction)instruction event:(MPEvent *)event product:(MPProduct *)product __attribute__((objc_designated_initializer));
		[Export("initWithInstruction:event:product:")]
		[DesignatedInitializer]
		IntPtr Constructor(MPCommerceInstruction instruction, MPEvent @event, MPProduct product);
	}

	// @interface MPBags : NSObject
	[BaseType(typeof(NSObject))]
	[Protocol]
	interface MPBags
	{
		// -(void)addProduct:(MPProduct * _Nonnull)product toBag:(NSString * _Nonnull)bagName;
		[Export("addProduct:toBag:")]
		void AddProduct(MPProduct product, string bagName);

		// -(void)removeProduct:(MPProduct * _Nonnull)product fromBag:(NSString * _Nonnull)bagName;
		[Export("removeProduct:fromBag:")]
		void RemoveProduct(MPProduct product, string bagName);

		// -(NSDictionary<NSString *,NSArray<MPProduct *> *> * _Nullable)productBags;
		[NullAllowed, Export("productBags")]
		NSDictionary<NSString, NSArray<MPProduct>> ProductBags { get; }

		// -(void)removeAllProductBags;
		[Export("removeAllProductBags")]
		void RemoveAllProductBags();

		// -(void)removeProductBag:(NSString * _Nonnull)bagName;
		[Export("removeProductBag:")]
		void RemoveProductBag(string bagName);
	}

	// @interface MPCommerce : NSObject
	[BaseType(typeof(NSObject))]
	[Protocol]
	interface MPCommerce
	{
		// @property (readonly, nonatomic, strong) MPCart * _Nonnull cart;
		[Export("cart", ArgumentSemantic.Strong)]
		MPCart Cart { get; }

		// @property (nonatomic, strong) NSString * _Nullable currency;
		[NullAllowed, Export("currency", ArgumentSemantic.Strong)]
		string Currency { get; set; }

		// -(void)checkout;
		[Export("checkout")]
		void Checkout();

		// -(void)checkoutWithOptions:(NSString * _Nullable)options step:(NSInteger)step;
		[Export("checkoutWithOptions:step:")]
		void CheckoutWithOptions([NullAllowed] string options, nint step);

		// -(void)clearCart;
		[Export("clearCart")]
		void ClearCart();

		// -(void)purchaseWithTransactionAttributes:(MPTransactionAttributes * _Nonnull)transactionAttributes clearCart:(BOOL)clearCart;
		[Export("purchaseWithTransactionAttributes:clearCart:")]
		void PurchaseWithTransactionAttributes(MPTransactionAttributes transactionAttributes, bool clearCart);

		// -(void)refundTransactionAttributes:(MPTransactionAttributes * _Nonnull)transactionAttributes clearCart:(BOOL)clearCart;
		[Export("refundTransactionAttributes:clearCart:")]
		void RefundTransactionAttributes(MPTransactionAttributes transactionAttributes, bool clearCart);
	}

	// @interface MPDateFormatter : NSObject
	[BaseType(typeof(NSObject))]
	[Protocol]
	interface MPDateFormatter
	{
		// +(NSDate * _Nullable)dateFromString:(NSString * _Nonnull)dateString;
		[Static]
		[Export("dateFromString:")]
		[return: NullAllowed]
		NSDate DateFromString(string dateString);

		// +(NSDate * _Nullable)dateFromStringRFC1123:(NSString * _Nonnull)dateString;
		[Static]
		[Export("dateFromStringRFC1123:")]
		[return: NullAllowed]
		NSDate DateFromStringRFC1123(string dateString);

		// +(NSDate * _Nullable)dateFromStringRFC3339:(NSString * _Nonnull)dateString;
		[Static]
		[Export("dateFromStringRFC3339:")]
		[return: NullAllowed]
		NSDate DateFromStringRFC3339(string dateString);

		// +(NSString * _Nullable)stringFromDateRFC1123:(NSDate * _Nonnull)date;
		[Static]
		[Export("stringFromDateRFC1123:")]
		[return: NullAllowed]
		string StringFromDateRFC1123(NSDate date);

		// +(NSString * _Nullable)stringFromDateRFC3339:(NSDate * _Nonnull)date;
		[Static]
		[Export("stringFromDateRFC3339:")]
		[return: NullAllowed]
		string StringFromDateRFC3339(NSDate date);
	}

	// @interface MPEvent : NSObject <NSCopying>
	[BaseType(typeof(NSObject))]
	[Protocol]
	interface MPEvent : INSCopying
	{
		// @property (nonatomic, strong) NSString * _Nullable category;
		[NullAllowed, Export("category", ArgumentSemantic.Strong)]
		string Category { get; set; }

		// @property (readonly, nonatomic, strong) NSDictionary<NSString *,__kindof NSArray<NSString *> *> * _Nonnull customFlags;
		[Export("customFlags", ArgumentSemantic.Strong)]
		NSDictionary<NSString, NSArray<NSString>> CustomFlags { get; }

		// @property (nonatomic, strong) NSNumber * _Nullable duration;
		[NullAllowed, Export("duration", ArgumentSemantic.Strong)]
		NSNumber Duration { get; set; }

		// @property (nonatomic, strong) NSDate * _Nullable endTime;
		[NullAllowed, Export("endTime", ArgumentSemantic.Strong)]
		NSDate EndTime { get; set; }

		// @property (nonatomic, strong) NSDictionary<NSString *,id> * _Nullable info;
		[NullAllowed, Export("info", ArgumentSemantic.Strong)]
		NSDictionary<NSString, NSObject> Info { get; set; }

		// @property (nonatomic, strong) NSString * _Nonnull name;
		[Export("name", ArgumentSemantic.Strong)]
		string Name { get; set; }

		// @property (nonatomic, strong) NSDate * _Nullable startTime;
		[NullAllowed, Export("startTime", ArgumentSemantic.Strong)]
		NSDate StartTime { get; set; }

		// @property (readonly, nonatomic, strong) NSString * _Nonnull typeName;
		[Export("typeName", ArgumentSemantic.Strong)]
		string TypeName { get; }

		// @property (nonatomic, unsafe_unretained) MPEventType type;
		[Export("type", ArgumentSemantic.Assign)]
		MPEventType Type { get; set; }

		// -(instancetype _Nullable)initWithName:(NSString * _Nonnull)name type:(MPEventType)type __attribute__((objc_designated_initializer));
		[Export("initWithName:type:")]
		[DesignatedInitializer]
		IntPtr Constructor(string name, MPEventType type);

		// -(void)addCustomFlag:(NSString * _Nonnull)customFlag withKey:(NSString * _Nonnull)key;
		[Export("addCustomFlag:withKey:")]
		void AddCustomFlag(string customFlag, string key);

		// -(void)addCustomFlags:(NSArray<NSString *> * _Nonnull)customFlags withKey:(NSString * _Nonnull)key;
		[Export("addCustomFlags:withKey:")]
		void AddCustomFlags(string[] customFlags, string key);
	}

	// @interface MPKitExecStatus : NSObject
	[BaseType(typeof(NSObject))]
	[Protocol]
	interface MPKitExecStatus
	{
		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull kitCode;
		[Export("kitCode", ArgumentSemantic.Strong)]
		NSNumber KitCode { get; }

		// @property (nonatomic, unsafe_unretained) MPKitReturnCode returnCode;
		[Export("returnCode", ArgumentSemantic.Assign)]
		MPKitReturnCode ReturnCode { get; set; }

		// @property (readonly, nonatomic, unsafe_unretained) NSUInteger forwardCount;
		[Export("forwardCount")]
		nuint ForwardCount { get; }

		// @property (readonly, nonatomic, unsafe_unretained) BOOL success;
		[Export("success")]
		bool Success { get; }

		// -(instancetype _Nonnull)initWithSDKCode:(NSNumber * _Nonnull)kitCode returnCode:(MPKitReturnCode)returnCode;
		[Export("initWithSDKCode:returnCode:")]
		IntPtr Constructor(NSNumber kitCode, MPKitReturnCode returnCode);

		// -(instancetype _Nonnull)initWithSDKCode:(NSNumber * _Nonnull)kitCode returnCode:(MPKitReturnCode)returnCode forwardCount:(NSUInteger)forwardCount;
		[Export("initWithSDKCode:returnCode:forwardCount:")]
		IntPtr Constructor(NSNumber kitCode, MPKitReturnCode returnCode, nuint forwardCount);

		// -(void)incrementForwardCount;
		[Export("incrementForwardCount")]
		void IncrementForwardCount();
	}

	// @interface MPPromotion : NSObject <NSCopying, NSCoding>
	[BaseType(typeof(NSObject))]
	[Protocol]
	interface MPPromotion : INSCopying, INSCoding
	{
		// @property (nonatomic, strong) NSString * _Nullable creative;
		[NullAllowed, Export("creative", ArgumentSemantic.Strong)]
		string Creative { get; set; }

		// @property (nonatomic, strong) NSString * _Nullable name;
		[NullAllowed, Export("name", ArgumentSemantic.Strong)]
		string Name { get; set; }

		// @property (nonatomic, strong) NSString * _Nullable position;
		[NullAllowed, Export("position", ArgumentSemantic.Strong)]
		string Position { get; set; }

		// @property (nonatomic, strong) NSString * _Nullable promotionId;
		[NullAllowed, Export("promotionId", ArgumentSemantic.Strong)]
		string PromotionId { get; set; }
	}

	// @interface MPPromotionContainer : NSObject <NSCopying, NSCoding>
	[BaseType(typeof(NSObject))]
	[Protocol]
	interface MPPromotionContainer : INSCopying, INSCoding
	{
		// @property (readonly, nonatomic, strong) NSArray<MPPromotion *> * _Nullable promotions;
		[NullAllowed, Export("promotions", ArgumentSemantic.Strong)]
		MPPromotion[] Promotions { get; }

		// @property (readonly, nonatomic, unsafe_unretained) MPPromotionAction action;
		[Export("action", ArgumentSemantic.Assign)]
		MPPromotionAction Action { get; }

		// -(instancetype _Nonnull)initWithAction:(MPPromotionAction)action promotion:(MPPromotion * _Nullable)promotion;
		[Export("initWithAction:promotion:")]
		IntPtr Constructor(MPPromotionAction action, [NullAllowed] MPPromotion promotion);

		// -(void)addPromotion:(MPPromotion * _Nonnull)promotion;
		[Export("addPromotion:")]
		void AddPromotion(MPPromotion promotion);
	}

	// @interface MPUserSegments : NSObject <NSCopying>
	[BaseType(typeof(NSObject))]
	interface MPUserSegments : INSCopying
	{
		// @property (readonly, nonatomic, strong) NSArray<MPSegment *> * _Nullable segmentsIds;
		[NullAllowed, Export("segmentsIds", ArgumentSemantic.Strong)]
		MPSegment[] SegmentsIds { get; }

		// @property (readonly, nonatomic, strong) NSDate * _Nullable expiration;
		[NullAllowed, Export("expiration", ArgumentSemantic.Strong)]
		NSDate Expiration { get; }

		// @property (readonly, nonatomic) BOOL expired;
		[Export("expired")]
		bool Expired { get; }

		// -(NSString * _Nullable)commaSeparatedSegments;
		[NullAllowed, Export("commaSeparatedSegments")]
		string CommaSeparatedSegments { get; }
	}

	// This was added manually. For some reason, Sharpie didn't generate it.
	// @interface MPDataModelAbstract : NSObject <NSCopying> 
	[BaseType(typeof(NSObject))]
	interface MPDataModelAbstract : INSCopying
	{

	}

	// This was added manually. For some reason, Sharpie didn't generate it.
	// @interface MPSegment : MPDataModelAbstract <NSCopying>
	[BaseType(typeof(MPDataModelAbstract))]
	interface MPSegment : INSCopying
	{
		// @property (nonatomic, strong) NSNumber * segmentId;
		[Export("segmentId", ArgumentSemantic.Strong)]
		NSNumber SegmentId { get; }

		//@property(nonatomic, strong) NSArray* endpointIds;
		[Export("endpointIds")]
		NSObject[] EndpointIds { get; }

		//@property(nonatomic, strong, readonly) NSDate* expiration;
		[Export("expiration")]
		NSDate Expiration { get; }

		//@property(nonatomic, strong) NSString* name;
		[Export("name")]
		NSString Name { get; }

		//@property(nonatomic, strong) NSArray<MPSegmentMembership*>* memberships;
		[Export("memberships")]
		MPSegmentMembership[] Memberships { get; }

		//@property(nonatomic, unsafe_unretained, readonly) BOOL expired;
		[Export("expired")]
		bool Expired { get; }
	}

	// This was added manually. For some reason, Sharpie didn't generate it.
	// @interface MPSegmentMembership : NSObject<NSCopying>
	[BaseType(typeof(NSObject))]
	interface MPSegmentMembership : INSCopying
	{

		//@property(nonatomic, unsafe_unretained) int64_t segmentId;
		[Export("segmentId")]
		ulong SegmentId { get; }

		//@property(nonatomic, unsafe_unretained) int64_t segmentMembershipId;
		[Export("segmentMembershipId")]
		ulong SegmentMembershipId { get; }

		//@property(nonatomic, unsafe_unretained) NSTimeInterval timestamp;
		[Export("timestamp")]
		double Timestamp { get; }

		//@property(nonatomic, unsafe_unretained) MPSegmentMembershipAction action;
		[Export("action")]
		MPSegmentMembership Action { get; set; }
	}

	// typedef void (^MPUserSegmentsHandler)(MPUserSegments * _Nullable, NSError * _Nullable);
	delegate void MPUserSegmentsHandler([NullAllowed] MPUserSegments arg0, [NullAllowed] NSError arg1);

	// @interface MPCaseInsensitive (NSArray)
	[Category]
	[BaseType(typeof(NSArray))]
	interface NSArray_MPCaseInsensitive
	{
		// -(BOOL)caseInsensitiveContainsObject:(NSString * _Nonnull)object;
		[Export("caseInsensitiveContainsObject:")]
		bool CaseInsensitiveContainsObject(string @object);
	}

	// @interface MParticle : NSObject
	[BaseType(typeof(NSObject))]
	[Protocol]
	interface MParticle
	{
		// @property (readonly, nonatomic, strong) MPBags * _Nonnull bags;
		[Export("bags", ArgumentSemantic.Strong)]
		MPBags Bags { get; }

		// @property (readonly, nonatomic, strong) MPCommerce * _Nonnull commerce;
		[Export("commerce", ArgumentSemantic.Strong)]
		MPCommerce Commerce { get; }

		// @property (nonatomic, unsafe_unretained) BOOL debugMode;
		[Export("debugMode")]
		bool DebugMode { get; set; }

		// @property (nonatomic, unsafe_unretained) BOOL consoleLogging;
		[Export("consoleLogging")]
		bool ConsoleLogging { get; set; }

		// @property (readonly, nonatomic, unsafe_unretained) MPEnvironment environment;
		[Export("environment", ArgumentSemantic.Assign)]
		MPEnvironment Environment { get; }

		// @property (readonly, nonatomic, unsafe_unretained) BOOL initialized;
		[Export("initialized")]
		bool Initialized { get; }

		// @property (nonatomic, unsafe_unretained) MPILogLevel logLevel;
		[Export("logLevel", ArgumentSemantic.Assign)]
		MPILogLevel LogLevel { get; set; }

		// @property (readonly, nonatomic, unsafe_unretained) BOOL measuringNetworkPerformance;
		[Export("measuringNetworkPerformance")]
		bool MeasuringNetworkPerformance { get; }

		// @property (readwrite, nonatomic, unsafe_unretained) BOOL optOut;
		[Export("optOut")]
		bool OptOut { get; set; }

		// @property (readonly, nonatomic, unsafe_unretained) BOOL proxiedAppDelegate;
		[NullAllowed, Export("proxiedAppDelegate", ArgumentSemantic.Assign)]
		NSObject WeakProxiedAppDelegate { get; }

		// @property (nonatomic, strong) NSData * _Nullable pushNotificationToken;
		[NullAllowed, Export("pushNotificationToken", ArgumentSemantic.Strong)]
		NSData PushNotificationToken { get; set; }

		// @property (readwrite, nonatomic, unsafe_unretained) NSTimeInterval sessionTimeout;
		[Export("sessionTimeout")]
		double SessionTimeout { get; set; }

		// @property (readonly, nonatomic, strong) NSString * _Nullable uniqueIdentifier;
		[NullAllowed, Export("uniqueIdentifier", ArgumentSemantic.Strong)]
		string UniqueIdentifier { get; }

		// @property (readwrite, nonatomic, unsafe_unretained) NSTimeInterval uploadInterval;
		[Export("uploadInterval")]
		double UploadInterval { get; set; }

		// @property (readonly, nonatomic, strong) NSDictionary<NSString *,id> * _Nullable userAttributes;
		[NullAllowed, Export("userAttributes", ArgumentSemantic.Strong)]
		NSDictionary<NSString, NSObject> UserAttributes { get; }

		// @property (readonly, nonatomic, strong) NSString * _Nonnull version;
		[Export("version", ArgumentSemantic.Strong)]
		string Version { get; }

		// +(instancetype _Nonnull)sharedInstance;
		[Static]
		[Export("sharedInstance")]
		MParticle SharedInstance();

		// -(void)start;
		[Export("start")]
		void Start();

		// -(void)startWithKey:(NSString * _Nonnull)apiKey secret:(NSString * _Nonnull)secret;
		[Export("startWithKey:secret:")]
		void StartWithKey(string apiKey, string secret);

		// -(void)startWithKey:(NSString * _Nonnull)apiKey secret:(NSString * _Nonnull)secret installationType:(MPInstallationType)installationType environment:(MPEnvironment)environment;
		[Export("startWithKey:secret:installationType:environment:")]
		void StartWithKey(string apiKey, string secret, MPInstallationType installationType, MPEnvironment environment);

		// -(void)startWithKey:(NSString * _Nonnull)apiKey secret:(NSString * _Nonnull)secret installationType:(MPInstallationType)installationType environment:(MPEnvironment)environment proxyAppDelegate:(BOOL)proxyAppDelegate;
		[Export("startWithKey:secret:installationType:environment:proxyAppDelegate:")]
		void StartWithKey(string apiKey, string secret, MPInstallationType installationType, MPEnvironment environment, bool proxyAppDelegate);

		// -(void)didReceiveLocalNotification:(UILocalNotification * _Nonnull)notification;
		[Export("didReceiveLocalNotification:")]
		void DidReceiveLocalNotification(UILocalNotification notification);

		// -(void)didReceiveRemoteNotification:(NSDictionary * _Nonnull)userInfo;
		[Export("didReceiveRemoteNotification:")]
		void DidReceiveRemoteNotification(NSDictionary userInfo);

		// -(void)didFailToRegisterForRemoteNotificationsWithError:(NSError * _Nullable)error;
		[Export("didFailToRegisterForRemoteNotificationsWithError:")]
		void DidFailToRegisterForRemoteNotificationsWithError([NullAllowed] NSError error);

		// -(void)didRegisterForRemoteNotificationsWithDeviceToken:(NSData * _Nonnull)deviceToken;
		[Export("didRegisterForRemoteNotificationsWithDeviceToken:")]
		void DidRegisterForRemoteNotificationsWithDeviceToken(NSData deviceToken);

		// -(void)handleActionWithIdentifier:(NSString * _Nullable)identifier forLocalNotification:(UILocalNotification * _Nullable)notification;
		[Export("handleActionWithIdentifier:forLocalNotification:")]
		void HandleActionWithIdentifier([NullAllowed] string identifier, [NullAllowed] UILocalNotification notification);

		// -(void)handleActionWithIdentifier:(NSString * _Nullable)identifier forRemoteNotification:(NSDictionary * _Nullable)userInfo;
		[Export("handleActionWithIdentifier:forRemoteNotification:")]
		void HandleActionWithIdentifier([NullAllowed] string identifier, [NullAllowed] NSDictionary userInfo);

		// -(void)openURL:(NSURL * _Nonnull)url sourceApplication:(NSString * _Nullable)sourceApplication annotation:(id _Nullable)annotation;
		[Export("openURL:sourceApplication:annotation:")]
		void OpenURL(NSUrl url, [NullAllowed] string sourceApplication, [NullAllowed] NSObject annotation);

		// -(void)openURL:(NSURL * _Nonnull)url options:(NSDictionary<NSString *,id> * _Nullable)options;
		[Export("openURL:options:")]
		void OpenURL(NSUrl url, [NullAllowed] NSDictionary<NSString, NSObject> options);

		// -(BOOL)continueUserActivity:(NSUserActivity * _Nonnull)userActivity restorationHandler:(void (^ _Nonnull)(NSArray * _Nullable))restorationHandler;
		[Export("continueUserActivity:restorationHandler:")]
		bool ContinueUserActivity(NSUserActivity userActivity, Action<NSArray> restorationHandler);

		// -(NSSet * _Nullable)activeTimedEvents;
		[NullAllowed, Export("activeTimedEvents")]
		NSSet ActiveTimedEvents { get; }

		// -(void)beginTimedEvent:(MPEvent * _Nonnull)event;
		[Export("beginTimedEvent:")]
		void BeginTimedEvent(MPEvent @event);

		// -(void)endTimedEvent:(MPEvent * _Nonnull)event;
		[Export("endTimedEvent:")]
		void EndTimedEvent(MPEvent @event);

		// -(MPEvent * _Nullable)eventWithName:(NSString * _Nonnull)eventName;
		[Export("eventWithName:")]
		[return: NullAllowed]
		MPEvent EventWithName(string eventName);

		// -(void)logEvent:(MPEvent * _Nonnull)event;
		[Export("logEvent:")]
		void LogEvent(MPEvent @event);

		// -(void)logEvent:(NSString * _Nonnull)eventName eventType:(MPEventType)eventType eventInfo:(NSDictionary<NSString *,id> * _Nullable)eventInfo;
		[Export("logEvent:eventType:eventInfo:")]
		void LogEvent(string eventName, MPEventType eventType, [NullAllowed] NSDictionary<NSString, NSObject> eventInfo);

		// -(void)logScreenEvent:(MPEvent * _Nonnull)event;
		[Export("logScreenEvent:")]
		void LogScreenEvent(MPEvent @event);

		// -(void)logScreen:(NSString * _Nonnull)screenName eventInfo:(NSDictionary<NSString *,id> * _Nullable)eventInfo;
		[Export("logScreen:eventInfo:")]
		void LogScreen(string screenName, [NullAllowed] NSDictionary<NSString, NSObject> eventInfo);

		// -(void)checkForDeferredDeepLinkWithCompletionHandler:(void (^ _Nonnull)(NSDictionary * _Nullable, NSError * _Nullable))completionHandler;
		[Export("checkForDeferredDeepLinkWithCompletionHandler:")]
		void CheckForDeferredDeepLinkWithCompletionHandler(Action<NSDictionary, NSError> completionHandler);

		// -(void)beginUncaughtExceptionLogging;
		[Export("beginUncaughtExceptionLogging")]
		void BeginUncaughtExceptionLogging();

		// -(void)endUncaughtExceptionLogging;
		[Export("endUncaughtExceptionLogging")]
		void EndUncaughtExceptionLogging();

		// -(void)leaveBreadcrumb:(NSString * _Nonnull)breadcrumbName;
		[Export("leaveBreadcrumb:")]
		void LeaveBreadcrumb(string breadcrumbName);

		// -(void)leaveBreadcrumb:(NSString * _Nonnull)breadcrumbName eventInfo:(NSDictionary<NSString *,id> * _Nullable)eventInfo;
		[Export("leaveBreadcrumb:eventInfo:")]
		void LeaveBreadcrumb(string breadcrumbName, [NullAllowed] NSDictionary<NSString, NSObject> eventInfo);

		// -(void)logError:(NSString * _Nonnull)message;
		[Export("logError:")]
		void LogError(string message);

		// -(void)logError:(NSString * _Nonnull)message eventInfo:(NSDictionary<NSString *,id> * _Nullable)eventInfo;
		[Export("logError:eventInfo:")]
		void LogError(string message, [NullAllowed] NSDictionary<NSString, NSObject> eventInfo);

		// -(void)logException:(NSException * _Nonnull)exception;
		[Export("logException:")]
		void LogException(NSException exception);

		// -(void)logException:(NSException * _Nonnull)exception topmostContext:(id _Nullable)topmostContext;
		[Export("logException:topmostContext:")]
		void LogException(NSException exception, [NullAllowed] NSObject topmostContext);

		// -(void)logCommerceEvent:(MPCommerceEvent * _Nonnull)commerceEvent;
		[Export("logCommerceEvent:")]
		void LogCommerceEvent(MPCommerceEvent commerceEvent);

		// -(void)logLTVIncrease:(double)increaseAmount eventName:(NSString * _Nonnull)eventName;
		[Export("logLTVIncrease:eventName:")]
		void LogLTVIncrease(double increaseAmount, string eventName);

		// -(void)logLTVIncrease:(double)increaseAmount eventName:(NSString * _Nonnull)eventName eventInfo:(NSDictionary<NSString *,id> * _Nullable)eventInfo;
		[Export("logLTVIncrease:eventName:eventInfo:")]
		void LogLTVIncrease(double increaseAmount, string eventName, [NullAllowed] NSDictionary<NSString, NSObject> eventInfo);

		// +(BOOL)registerExtension:(id<MPExtensionProtocol> _Nonnull)extension;
		[Static]
		[Export("registerExtension:")]
		bool RegisterExtension(MPExtensionProtocol extension);

		// -(MPKitExecStatus * _Nonnull)setIntegrationAttributes:(NSDictionary<NSString *,NSString *> * _Nonnull)attributes forKit:(NSNumber * _Nonnull)kitCode;
		[Export("setIntegrationAttributes:forKit:")]
		MPKitExecStatus SetIntegrationAttributes(NSDictionary<NSString, NSString> attributes, NSNumber kitCode);

		// -(MPKitExecStatus * _Nonnull)clearIntegrationAttributesForKit:(NSNumber * _Nonnull)kitCode;
		[Export("clearIntegrationAttributesForKit:")]
		MPKitExecStatus ClearIntegrationAttributesForKit(NSNumber kitCode);

		// -(BOOL)isKitActive:(NSNumber * _Nonnull)kitCode;
		[Export("isKitActive:")]
		bool IsKitActive(NSNumber kitCode);

		// -(id  _Nullable const)kitInstance:(NSNumber * _Nonnull)kitCode;
		[Export("kitInstance:")]
		[return: NullAllowed]
		NSObject KitInstance(NSNumber kitCode);

		// -(void)kitInstance:(NSNumber * _Nonnull)kitCode completionHandler:(void (^ _Nonnull)(id _Nullable))completionHandler;
		[Export("kitInstance:completionHandler:")]
		void KitInstance(NSNumber kitCode, Action<NSObject> completionHandler);

		// @property (nonatomic, unsafe_unretained) BOOL backgroundLocationTracking;
		[Export("backgroundLocationTracking")]
		bool BackgroundLocationTracking { get; set; }

		// @property (nonatomic, strong) CLLocation * _Nullable location;
		[NullAllowed, Export("location", ArgumentSemantic.Strong)]
		CLLocation Location { get; set; }

		// -(void)beginLocationTracking:(CLLocationAccuracy)accuracy minDistance:(CLLocationDistance)distanceFilter;
		[Export("beginLocationTracking:minDistance:")]
		void BeginLocationTracking(double accuracy, double distanceFilter);

		// -(void)beginLocationTracking:(CLLocationAccuracy)accuracy minDistance:(CLLocationDistance)distanceFilter authorizationRequest:(MPLocationAuthorizationRequest)authorizationRequest;
		[Export("beginLocationTracking:minDistance:authorizationRequest:")]
		void BeginLocationTracking(double accuracy, double distanceFilter, MPLocationAuthorizationRequest authorizationRequest);

		// -(void)endLocationTracking;
		[Export("endLocationTracking")]
		void EndLocationTracking();

		// -(void)beginMeasuringNetworkPerformance;
		[Export("beginMeasuringNetworkPerformance")]
		void BeginMeasuringNetworkPerformance();

		// -(void)endMeasuringNetworkPerformance;
		[Export("endMeasuringNetworkPerformance")]
		void EndMeasuringNetworkPerformance();

		// -(void)excludeURLFromNetworkPerformanceMeasuring:(NSURL * _Nonnull)url;
		[Export("excludeURLFromNetworkPerformanceMeasuring:")]
		void ExcludeURLFromNetworkPerformanceMeasuring(NSUrl url);

		// -(void)logNetworkPerformance:(NSString * _Nonnull)urlString httpMethod:(NSString * _Nonnull)httpMethod startTime:(NSTimeInterval)startTime duration:(NSTimeInterval)duration bytesSent:(NSUInteger)bytesSent bytesReceived:(NSUInteger)bytesReceived;
		[Export("logNetworkPerformance:httpMethod:startTime:duration:bytesSent:bytesReceived:")]
		void LogNetworkPerformance(string urlString, string httpMethod, double startTime, double duration, nuint bytesSent, nuint bytesReceived);

		// -(void)preserveQueryMeasuringNetworkPerformance:(NSString * _Nonnull)queryString;
		[Export("preserveQueryMeasuringNetworkPerformance:")]
		void PreserveQueryMeasuringNetworkPerformance(string queryString);

		// -(void)resetNetworkPerformanceExclusionsAndFilters;
		[Export("resetNetworkPerformanceExclusionsAndFilters")]
		void ResetNetworkPerformanceExclusionsAndFilters();

		// -(NSNumber * _Nullable)incrementSessionAttribute:(NSString * _Nonnull)key byValue:(NSNumber * _Nonnull)value;
		[Export("incrementSessionAttribute:byValue:")]
		[return: NullAllowed]
		NSNumber IncrementSessionAttribute(string key, NSNumber value);

		// -(void)setSessionAttribute:(NSString * _Nonnull)key value:(id _Nonnull)value;
		[Export("setSessionAttribute:value:")]
		void SetSessionAttribute(string key, NSObject value);

		// -(void)upload;
		[Export("upload")]
		void Upload();

		// -(NSString * _Nullable)surveyURL:(MPSurveyProvider)surveyProvider;
		[Export("surveyURL:")]
		[return: NullAllowed]
		string SurveyURL(MPSurveyProvider surveyProvider);

		// -(NSNumber * _Nullable)incrementUserAttribute:(NSString * _Nonnull)key byValue:(NSNumber * _Nonnull)value;
		[Export("incrementUserAttribute:byValue:")]
		[return: NullAllowed]
		NSNumber IncrementUserAttribute(string key, NSNumber value);

		// -(void)logout;
		[Export("logout")]
		void Logout();

		// -(void)setUserAttribute:(NSString * _Nonnull)key value:(id _Nullable)value;
		[Export("setUserAttribute:value:")]
		void SetUserAttribute(string key, [NullAllowed] NSObject value);

		// -(void)setUserAttribute:(NSString * _Nonnull)key values:(NSArray<NSString *> * _Nullable)values;
		[Export("setUserAttribute:values:")]
		void SetUserAttribute(string key, [NullAllowed] string[] values);

		// -(void)setUserIdentity:(NSString * _Nullable)identityString identityType:(MPUserIdentity)identityType;
		[Export("setUserIdentity:identityType:")]
		void SetUserIdentity([NullAllowed] string identityString, MPUserIdentity identityType);

		// -(void)setUserTag:(NSString * _Nonnull)tag;
		[Export("setUserTag:")]
		void SetUserTag(string tag);

		// -(void)removeUserAttribute:(NSString * _Nonnull)key;
		[Export("removeUserAttribute:")]
		void RemoveUserAttribute(string key);

		// -(void)userNotificationCenter:(UNUserNotificationCenter * _Nonnull)center willPresentNotification:(UNNotification * _Nonnull)notification;
		[Export("userNotificationCenter:willPresentNotification:")]
		void UserNotificationCenter(UNUserNotificationCenter center, UNNotification notification);

		// -(void)userNotificationCenter:(UNUserNotificationCenter * _Nonnull)center didReceiveNotificationResponse:(UNNotificationResponse * _Nonnull)response;
		[Export("userNotificationCenter:didReceiveNotificationResponse:")]
		void UserNotificationCenter(UNUserNotificationCenter center, UNNotificationResponse response);

		// -(void)userSegments:(NSTimeInterval)timeout endpointId:(NSString * _Nonnull)endpointId completionHandler:(MPUserSegmentsHandler _Nonnull)completionHandler;
		[Export("userSegments:endpointId:completionHandler:")]
		void UserSegments(double timeout, string endpointId, MPUserSegmentsHandler completionHandler);

		// -(void)initializeWebView:(UIWebView * _Nonnull)webView;
		[Export("initializeWebView:")]
		void InitializeWebView(UIWebView webView);

		// -(BOOL)isMParticleWebViewSdkUrl:(NSURL * _Nonnull)requestUrl;
		[Export("isMParticleWebViewSdkUrl:")]
		bool IsMParticleWebViewSdkUrl(NSUrl requestUrl);

		// -(void)processWebViewLogEvent:(NSURL * _Nonnull)requestUrl;
		[Export("processWebViewLogEvent:")]
		void ProcessWebViewLogEvent(NSUrl requestUrl);
	}
}