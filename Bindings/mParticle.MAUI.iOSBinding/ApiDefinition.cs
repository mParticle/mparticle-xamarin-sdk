using System;
using CoreLocation;
using Foundation;
using ObjCRuntime;
using UIKit;
using UserNotifications;
using WebKit;

namespace mParticle.MAUI.iOSBinding {
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

    // @interface MPBaseEvent : NSObject <NSCopying>
    [BaseType(typeof(NSObject))]
    [Protocol]
    interface MPBaseEvent : INSCopying
    {
        // @property (nonatomic, unsafe_unretained) MPEventType type;
        [Export("type", ArgumentSemantic.Assign)]
        MPEventType Type { get; set; }

        // - (NSString *)typeName;
        [Export("typeName")]
        string TypeName { get; }

        // @property (nonatomic, strong, nullable) NSDictionary<NSString *, id> *customAttributes;
        [NullAllowed, Export("customAttributes", ArgumentSemantic.Strong)]
        NSDictionary<NSString, NSObject> CustomAttributes { get; set; }

        // @property (readonly, nonatomic, strong) NSDictionary<NSString *,__kindof NSArray<NSString *> *> * _Nonnull customFlags;
        [Export("customFlags", ArgumentSemantic.Strong)]
        NSDictionary<NSString, NSArray<NSString>> CustomFlags { get; }

        // @property (nonatomic) BOOL shouldUploadEvent;
        [Export("shouldUploadEvent")]
        bool ShouldUploadEvent { get; set; }

        // -(void)addCustomFlag:(NSString * _Nonnull)customFlag withKey:(NSString * _Nonnull)key;
        [Export("addCustomFlag:withKey:")]
        void AddCustomFlag(string customFlag, string key);

        // -(void)addCustomFlags:(NSArray<NSString *> * _Nonnull)customFlags withKey:(NSString * _Nonnull)key;
        [Export("addCustomFlags:withKey:")]
        void AddCustomFlags(string[] customFlags, string key);
    }

    // @interface MPEvent : MPBaseEvent <NSCopying>
    [BaseType(typeof(MPBaseEvent))]
    [Protocol]
    interface MPEvent : INSCopying
    {
        // @property (nonatomic, strong, nullable) NSString *category;
        [NullAllowed, Export("category", ArgumentSemantic.Strong)]
        string Category { get; set; }

        // @property (nonatomic, strong, nullable) NSDictionary<NSString *, id> *info DEPRECATED_MSG_ATTRIBUTE("use customAttributes instead");
        [NullAllowed, Export("info", ArgumentSemantic.Strong)]
        NSDictionary<NSString, NSObject> Info { get; set; }

        // @property (nonatomic, strong, nonnull) NSString *name;
        [Export("name", ArgumentSemantic.Strong)]
        string Name { get; set; }

        // @property (nonatomic, strong, nullable) NSDate *startTime;
        [NullAllowed, Export("startTime", ArgumentSemantic.Strong)]
        NSDate StartTime { get; set; }

        // @property (nonatomic, strong, nullable) NSDate *endTime;
        [NullAllowed, Export("endTime", ArgumentSemantic.Strong)]
        NSDate EndTime { get; set; }

        // @property (nonatomic, strong) NSNumber * _Nullable duration;
        [NullAllowed, Export("duration", ArgumentSemantic.Strong)]
        NSNumber Duration { get; set; }

        // -(instancetype _Nullable)initWithName:(NSString * _Nonnull)name type:(MPEventType)type __attribute__((objc_designated_initializer));
        [Export("initWithName:type:")]
        [DesignatedInitializer]
        IntPtr Constructor(string name, MPEventType type);
    }

    // @interface MPCommerceEvent : MPBaseEvent <NSSecureCoding>
    [BaseType(typeof(MPBaseEvent))]
    [Protocol]
    interface MPCommerceEvent : INSCopying, INSSecureCoding
    {
        // @property (nonatomic, strong, nullable) NSString *checkoutOptions;
        [NullAllowed, Export("checkoutOptions", ArgumentSemantic.Strong)]
        string CheckoutOptions { get; set; }

        // @property (nonatomic, strong, nullable) NSString *currency;
        [NullAllowed, Export("currency", ArgumentSemantic.Strong)]
        string Currency { get; set; }

        // @property (nonatomic, strong, readonly, nullable) NSDictionary<NSString *, __kindof NSSet<MPProduct *> *> *impressions;
        [NullAllowed, Export("impressions", ArgumentSemantic.Strong)]
        NSDictionary<NSString, NSSet<MPProduct>> Impressions { get; }

        // @property (nonatomic, strong, readonly, nullable) NSArray<MPProduct *> *products;
        [NullAllowed, Export("products", ArgumentSemantic.Strong)]
        MPProduct[] Products { get; }

        // @property (nonatomic, strong, nullable) MPPromotionContainer *promotionContainer;
        [NullAllowed, Export("promotionContainer", ArgumentSemantic.Strong)]
        MPPromotionContainer PromotionContainer { get; set; }

        // @property (nonatomic, strong, nullable) NSString *productListName;
        [NullAllowed, Export("productListName", ArgumentSemantic.Strong)]
        string ProductListName { get; set; }

        // @property (nonatomic, strong, nullable) NSString *productListSource;
        [NullAllowed, Export("productListSource", ArgumentSemantic.Strong)]
        string ProductListSource { get; set; }

        // @property (nonatomic, strong, nullable) NSString *screenName;
        [NullAllowed, Export("screenName", ArgumentSemantic.Strong)]
        string ScreenName { get; set; }

        // @property (nonatomic, strong, nullable) MPTransactionAttributes *transactionAttributes;
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

        // - (nonnull instancetype)initWithAction:(MPCommerceEventAction)action product:(nullable MPProduct *)product;
        [Export("initWithAction:product:")]
        IntPtr Constructor(MPCommerceEventAction action, [NullAllowed] MPProduct product);

        // - (nonnull instancetype)initWithImpressionName:(nullable NSString *)listName product:(nullable MPProduct *)product;
        [Export("initWithImpressionName:product:")]
        IntPtr Constructor([NullAllowed] string listName, [NullAllowed] MPProduct product);

        // - (nonnull instancetype)initWithPromotionContainer:(nullable MPPromotionContainer *)promotionContainer;
        [Export("initWithPromotionContainer:")]
        IntPtr Constructor([NullAllowed] MPPromotionContainer promotionContainer);

        // - (void)addImpression:(nonnull MPProduct *)product listName:(nonnull NSString *)listName;
        [Export("addImpression:listName:")]
        void AddImpression(MPProduct product, string listName);

        // - (void)addProduct:(nonnull MPProduct *)product;
        [Export("addProduct:")]
        void AddProduct(MPProduct product);

        // - (void)removeProduct:(nonnull MPProduct *)product;
        [Export("removeProduct:")]
        void RemoveProduct(MPProduct product);

        // - (nullable NSArray *)allKeys DEPRECATED_MSG_ATTRIBUTE("use customAttributes.allKeys instead");
        [NullAllowed, Export("allKeys")]
        NSObject[] AllKeys { get; }

        // - (nullable id)objectForKeyedSubscript:(nonnull NSString *const)key DEPRECATED_MSG_ATTRIBUTE("use customAttributes[key] instead");
        [Export("objectForKeyedSubscript:")]
        [return: NullAllowed]
        NSObject ObjectForKeyedSubscript(string key);

        // - (void)setObject:(nonnull id)obj forKeyedSubscript:(nonnull NSString *)key DEPRECATED_MSG_ATTRIBUTE("use customAttributes[key] = obj instead");
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

        // - (void)addAllProducts:(nonnull NSArray<MPProduct *> *)products shouldLogEvents:(BOOL)shouldLogEvents;
        [Export("addAllProducts:shouldLogEvents:")]
        void AddAllProducts(NSArray<MPProduct> products, bool shouldLogEvents);

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

    //@interface MPIdentityApiResult : NSObject
    [BaseType(typeof(NSObject))]
    [Protocol]
    interface MPIdentityApiResult
    {
        // @property(nonatomic, strong, readwrite, nonnull) MParticleUser* user;
        [Export("user", ArgumentSemantic.Strong)]
        MParticleUser User { get; set; }
    }

    //typedef void (^MPIdentityApiResultCallback)(MPIdentityApiResult* _Nullable apiResult, NSError * _Nullable error);
    delegate void MPIdentityApiResultCallback([NullAllowed] MPIdentityApiResult apiResult, NSError error);

    // @interface MPIdentity : NSObject
    [BaseType(typeof(NSObject))]
    [Protocol]
    interface MPIdentityApi
    {
        // @property(nonatomic, strong, readonly, nullable) MParticleUser* currentUser;
        [Export("currentUser")]
        [return: NullAllowed]
        MParticleUser CurrentUser();

        // - (void) identify:(MPIdentityApiRequest*) identifyRequest completion:(nullable MPIdentityApiResultCallback) completion;
        [Export("identify:completion:")]
        void Identify(MPIdentityApiRequest request, MPIdentityApiResultCallback callback);

        // - (void) identifyWithCompletion:(nullable MPIdentityApiResultCallback) completion;
        [Export("identify:")]
        void Identify(MPIdentityApiResultCallback callback);


        // - (void) login:(MPIdentityApiRequest*) loginRequest completion:(nullable MPIdentityApiResultCallback) completion;
        [Export("login:completion:")]
        void Login(MPIdentityApiRequest request, MPIdentityApiResultCallback callback);


        // - (void) loginWithCompletion:(nullable MPIdentityApiResultCallback) completion;
        [Export("login:")]
        void Login(MPIdentityApiResultCallback callback);

        // - (void) logout:(MPIdentityApiRequest*) logoutRequest completion:(nullable MPIdentityApiResultCallback) completion;
        [Export("logout:completion:")]
        void Logout(MPIdentityApiRequest request, MPIdentityApiResultCallback callback);

        // - (void) logoutWithCompletion:(nullable MPIdentityApiResultCallback) completion;
        [Export("logout:")]
        void Logout(MPIdentityApiResultCallback callback);

        // - (void) modify:(MPIdentityApiRequest*) modifyRequest completion:(nullable MPIdentityApiResultCallback) completion;
        [Export("modify:completion:")]
        void Modify(MPIdentityApiRequest request, MPIdentityApiResultCallback callback);
    }

    public static class IdentityListenerString
    {
        public const string MParticleIdentityStateChangeListenerNotification = "mParticleIdentityStateChangeListenerNotification";
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

    // @interface MPCaseInsensitive (NSArray)
    [Category]
    [BaseType(typeof(NSArray))]
    interface NSArray_MPCaseInsensitive
    {
        // -(BOOL)caseInsensitiveContainsObject:(NSString * _Nonnull)object;
        [Export("caseInsensitiveContainsObject:")]
        bool CaseInsensitiveContainsObject(string @object);
    }


    //@interface MPAttributionResult : NSObject
    [BaseType(typeof(NSObject))]
    [Protocol]
    interface MPAttributionResult
    {
        // @property(nonatomic) NSDictionary* linkInfo;
        [Export("linkInfo", ArgumentSemantic.Strong)]
        NSDictionary LinkInfo { get; set; }

        // @property(nonatomic, readonly) NSNumber* kitCode;
        [Export("kitCode", ArgumentSemantic.Strong)]
        NSNumber KitCode { get; }

        // @property(nonatomic, readonly) NSString* kitName;
        [Export("kitName", ArgumentSemantic.Strong)]
        NSString KitName { get; }
    }

    //@interface MParticleUser : NSObject
    [BaseType(typeof(NSObject))]
    [Protocol]
    interface MParticleUser
    {
        // @property(readonly, strong, nonnull) NSNumber* userId;
        [Export("userId", ArgumentSemantic.Strong)]
        NSNumber UserId { get; set; }

        // @property (readonly, strong, nonnull) NSDictionary<NSNumber *, NSString *> *identities;
        [Export("identities", ArgumentSemantic.Strong)]
        NSDictionary<NSNumber, NSString> identities { get; set; }

        // @property(readwrite, strong, nonnull) NSDictionary<NSString*, id>* userAttributes;
        [Export("userAttributes", ArgumentSemantic.Strong)]
        NSDictionary<NSString, NSObject> UserAttributes { get; set; }

        // @property(readonly, strong, nonnull) MPCart* cart;
        [Export("cart", ArgumentSemantic.Strong)]
        MPCart Cart { get; set; }

        // - (nullable NSNumber *)incrementUserAttribute:(NSString*) key byValue:(NSNumber*) value;
        [Export("incrementUserAttributes:value:")]
        NSNumber IncrementUserAttribute(NSString key, NSNumber value);

        // - (void) setUserAttribute:(NSString*) key value:(nullable id) value;
        [Export("setUserAttribute:value:")]
        void SetUserAttribute(NSString key, NSObject value);

        // - (void) setUserAttributeList:(NSString*) key values:(nullable NSArray<NSString*> *)values;
        [Export("setUserAttributeList:values:")]
        void SetUserAttributeList(NSString key, NSArray<NSString> values);

        // - (void) setUserTag:(NSString*) tag;
        [Export("setUserTag:")]
        void SetUserTag(NSString tag);

        // - (void) removeUserAttribute:(NSString*) key;
        [Export("removeUserAttribute:")]
        void RemoveUserAttribute(NSString key);
    }

    // @property(nonatomic, copy, nullable) void (^onUserAlias)(MParticleUser* previousUser, MParticleUser* newUser);
    delegate void OnUserAlias(MParticleUser previousUser, MParticleUser newUser);


    // @interface MPIdentityApiRequest : NSObject
    [BaseType(typeof(NSObject))]
    [Protocol]
    interface MPIdentityApiRequest
    {
        // + (MPIdentityApiRequest*) requestWithEmptyUser;
        [Static]
        [Export("requestWithEmptyUser")]
        MPIdentityApiRequest RequestWithEmptyUser();

        // + (MPIdentityApiRequest*) requestWithUser:(MParticleUser*) user;
        [Static]
        [Export("requestWithUser:", ArgumentSemantic.Strong)]
        MPIdentityApiRequest RequestWithUser(MParticleUser user);

        // @property(nonatomic, strong, nullable) NSString* email;
        [Export("email", ArgumentSemantic.Strong)]
        string Email { get; set; }

        // @property(nonatomic, strong, nullable) NSString* customerId;
        [Export("customerId", ArgumentSemantic.Strong)]
        string CustomerId { get; set; }

        // @property (nonatomic, strong, nullable, readonly) NSDictionary *identities;
        [Export("identities", ArgumentSemantic.Strong)]
        NSDictionary<NSNumber, NSString> Identities { get; }

        // - (void) setIdentity:(nullable NSString *)identityString identityType:(MPIdentity) identityType;
        [Export("setIdentity:identityType:")]
        void SetIdentity([NullAllowed] NSString identityString, MPUserIdentity identityType);

    }

    // @interface MParticleOptions : NSObject
    [BaseType(typeof(NSObject))]
    [Protocol]
    interface MParticleOptions
    {
        // @property(nonatomic, strong, readwrite) NSString* apiKey;
        [Export("apiKey", ArgumentSemantic.Strong)]
        string ApiKey { get; set; }

        // @property(nonatomic, strong, readwrite) NSString* apiSecret;
        [Export("apiSecret", ArgumentSemantic.Strong)]
        string ApiSecret { get; set; }

        // @property(nonatomic, strong, readwrite) NSString* sharedGroupID;
        [Export("sharedGroupID", ArgumentSemantic.Strong)]
        string SharedGroupId { get; set; }

        // @property(nonatomic, unsafe_unretained, readwrite) MPInstallationType installType;
        [Export("installType", ArgumentSemantic.Assign)]
        MPInstallationType InstallType { get; set; }

        // @property(nonatomic, strong, readwrite) MPIdentityApiRequest* identifyRequest;
        [Export("identifyRequest", ArgumentSemantic.Strong)]
        MPIdentityApiRequest IdentifyRequest { get; set; }

        // @property(nonatomic, unsafe_unretained, readwrite) MPEnvironment environment;
        [Export("environment", ArgumentSemantic.Assign)]
        MPEnvironment Environment { get; set; }

        // @property(nonatomic, unsafe_unretained, readwrite) BOOL proxyAppDelegate;
        [Export("proxyAppDelegate", ArgumentSemantic.Assign)]
        bool ProxyAppDelegate { get; set; }

        // @property(nonatomic, unsafe_unretained, readwrite) BOOL automaticSessionTracking;
        [Export("automaticSessionTracking", ArgumentSemantic.Assign)]
        bool AutomaticSessionTracking { get; set; }

        // @property(atomic, strong, nullable) NSString* customUserAgent;
        [Export("customUserAgent", ArgumentSemantic.Strong)]
        string CustomUserAgent { get; set; }

        // @property(atomic, unsafe_unretained, readwrite) BOOL collectUserAgent;
        [Export("collectUserAgent", ArgumentSemantic.Assign)]
        bool CollectUserAgent { get; set; }

        // @property (nonatomic, strong, readwrite, nullable) NSNumber *configMaxAgeSeconds;
        [NullAllowed, Export("configMaxAgeSeconds", ArgumentSemantic.Strong)]
        NSNumber ConfigMaxAgeSeconds { get; set; }

        // @property(nonatomic, copy) void (^onIdentifyComplete)(MPIdentityApiResult* _Nullable apiResult, NSError *_Nullable error);
        [Export("onIdentifyComplete")]
        OnIdentifyComplete OnIdentifyComplete { set; }

        // @property(nonatomic, copy) void (^onAttributionComplete)(MPAttributionResult* _Nullable attributionResult, NSError *_Nullable error);
        [Export("onAttributionComplete")]
        OnAttributionCompleted OnAttributionCompleted { set; }
    }

    // void (^onIdentifyComplete)(MPIdentityApiResult* _Nullable apiResult, NSError *_Nullable error);
    delegate void OnIdentifyComplete([NullAllowed] MPIdentityApiResult request, [NullAllowed] NSError error);

    // void (^onAttributionComplete)(MPAttributionResult* _Nullable attributionResult, NSError *_Nullable error);
    delegate void OnAttributionCompleted([NullAllowed] MPAttributionResult attributionResult, [NullAllowed] NSError error);

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

        // @property (readonly, nonatomic, strong) MPIdentityApi * _Nonnull commerce;
        [Export("identity", ArgumentSemantic.Strong)]
        MPIdentityApi Identity { get; }

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

        // @property (readwrite, nonatomic, unsafe_unretained) BOOL optOut;
        [Export("optOut")]
        bool OptOut { get; set; }

        // @property (readonly, nonatomic, unsafe_unretained) BOOL proxiedAppDelegate;
        [NullAllowed, Export("proxiedAppDelegate", ArgumentSemantic.Assign)]
        NSObject WeakProxiedAppDelegate { get; }

        // @property (nonatomic, unsafe_unretained) BOOL automaticSessionTracking;
        [Export("automaticSessionTracking", ArgumentSemantic.Assign)]
        bool AutomaticSessionTracking { get; }

        // @property (atomic, strong, nullable) NSString *customUserAgent;
        [Export("customUserAgent", ArgumentSemantic.Strong)]
        string CustomUserAgent { get; set; }

        // @property (nonatomic, strong) NSData * _Nullable pushNotificationToken;
        [NullAllowed, Export("pushNotificationToken", ArgumentSemantic.Strong)]
        NSData PushNotificationToken { get; set; }

        // @property (nonatomic, unsafe_unretained, readonly) NSTimeInterval sessionTimeout;
        [Export("sessionTimeout")]
        double SessionTimeout { get; }

        // @property (readonly, nonatomic, strong) NSString * _Nullable uniqueIdentifier;
        [NullAllowed, Export("uniqueIdentifier", ArgumentSemantic.Strong)]
        string UniqueIdentifier { get; }

        // @property(nonatomic, unsafe_unretained, readwrite) NSTimeInterval uploadInterval;
        [Export("uploadInterval")]
        double UploadInterval { get; }

        // @property (readonly, nonatomic, strong) NSString * _Nonnull version;
        [Export("version", ArgumentSemantic.Strong)]
        string Version { get; }

        // @property (nonatomic, readonly, nullable) NSNumber *configMaxAgeSeconds;
        [NullAllowed, Export("configMaxAgeSeconds")]
        NSNumber ConfigMaxAgeSeconds { get; }

        // +(instancetype _Nonnull)sharedInstance;
        [Static]
        [Export("sharedInstance")]
        MParticle SharedInstance { get; set; }

        // -(void)start;
        [Export("start")]
        void Start();

        // - (void)startWithOptions:(MParticleOptions *)options;
        [Export("startWithOptions:")]
        void StartWithOptions(MParticleOptions options);

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

        // -(void)setATTStatus:(MPATTAuthorizationStatus)status withATTStatusTimestampMillis:(NSNumber *)attStatusTimestampMillis;
        [Export("setATTStatus:withATTStatusTimestampMillis:")]
        void setATTStatus(MPATTAuthorizationStatus status, [NullAllowed] NSNumber attStatusTimestampMillis);

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

        // -(void)excludeURLFromNetworkPerformanceMeasuring:(NSURL * _Nonnull)url;
        [Export("excludeURLFromNetworkPerformanceMeasuring:")]
        void ExcludeURLFromNetworkPerformanceMeasuring(NSUrl url);

        // -(void)logNetworkPerformance:(NSString * _Nonnull)urlString httpMethod:(NSString * _Nonnull)httpMethod startTime:(NSTimeInterval)startTime duration:(NSTimeInterval)duration bytesSent:(NSUInteger)bytesSent bytesReceived:(NSUInteger)bytesReceived;
        [Export("logNetworkPerformance:httpMethod:startTime:duration:bytesSent:bytesReceived:")]
        void LogNetworkPerformance(string urlString, string httpMethod, double startTime, double duration, nuint bytesSent, nuint bytesReceived);


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

        // -(void)userNotificationCenter:(UNUserNotificationCenter * _Nonnull)center willPresentNotification:(UNNotification * _Nonnull)notification;
        [Export("userNotificationCenter:willPresentNotification:")]
        void UserNotificationCenter(UNUserNotificationCenter center, UNNotification notification);

        // -(void)userNotificationCenter:(UNUserNotificationCenter * _Nonnull)center didReceiveNotificationResponse:(UNNotificationResponse * _Nonnull)response;
        [Export("userNotificationCenter:didReceiveNotificationResponse:")]
        void UserNotificationCenter(UNUserNotificationCenter center, UNNotificationResponse response);

        // -(void)initializeWebView:(UIWebView * _Nonnull)webView;
        [Export("initializeWebView:")]
        void InitializeWebView(UIWebView webView);

        // - (void)initializeWKWebView:(WKWebView *)webView;
        [Export("initializeWKWebView:")]
        void InitializeWKWebView(WKWebView webView);

        // -(BOOL)isMParticleWebViewSdkUrl:(NSURL * _Nonnull)requestUrl;
        [Export("isMParticleWebViewSdkUrl:")]
        bool IsMParticleWebViewSdkUrl(NSUrl requestUrl);

        // -(void)processWebViewLogEvent:(NSURL * _Nonnull)requestUrl;
        [Export("processWebViewLogEvent:")]
        void ProcessWebViewLogEvent(NSUrl requestUrl);
    }
}


