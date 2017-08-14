using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using mParticle.Xamarin.iOSBinding;

namespace mParticle.Xamarin
{
    public class MParticleSDK : MParticleSDKBase
	{
		public MParticleSDK()
		{
		}

        public override Environment GetEnvironment()
        {
            switch (iOSBinding.MParticle.SharedInstance().Environment)
            {
                case iOSBinding.MPEnvironment.Development:
                    return Environment.Development;

                case iOSBinding.MPEnvironment.Production:
                    return Environment.Production;

				case iOSBinding.MPEnvironment.AutoDetect:
                default:
					return Environment.AutoDetect;
            }   
        }

        public override int IncrementUserAttribute(string key, int incrementValue)
        {
            iOSBinding.MParticle.SharedInstance().IncrementUserAttribute(key, incrementValue);
            return 0;
        }

        public override void Initialize(string apiKey, string apiSecret)
        {
            iOSBinding.MParticle.SharedInstance().StartWithKey(apiKey, apiSecret, MPInstallationType.Autodetect, MPEnvironment.AutoDetect, true);
            iOSBinding.MParticle.SharedInstance().LogLevel = MPILogLevel.Verbose;
        }

        public override void LeaveBreadcrumb(string breadcrumbName)
        {
            iOSBinding.MParticle.SharedInstance().LeaveBreadcrumb(breadcrumbName);
		}

        public override void LogCommerceEvent(CommerceEvent commerceEvent)
        {
            var bindingCommerceEvent = new iOSBinding.MPCommerceEvent();

			if (commerceEvent.TransactionAttributes != null)
                bindingCommerceEvent.TransactionAttributes = ConvertToMpTransactionAttributes(commerceEvent.TransactionAttributes);

            bindingCommerceEvent.ScreenName = commerceEvent.ScreenName;
            bindingCommerceEvent.Currency = commerceEvent.Currency;
            bindingCommerceEvent.SetCustomAttributes(ConvertToNSDictionary<NSString, NSString>(commerceEvent.CustomAttributes));
            bindingCommerceEvent.CheckoutOptions = commerceEvent.CheckoutOptions;

			if (commerceEvent.Products != null)
			{
                bindingCommerceEvent.Action = ConvertToMpProductAction(commerceEvent.ProductAction);
				foreach (var product in commerceEvent.Products)
				{
					bindingCommerceEvent.AddProduct(ConvertToMpProduct(product));
				}
			}

			if (commerceEvent.CheckoutStep != null)
				bindingCommerceEvent.CheckoutStep = commerceEvent.CheckoutStep.Value;

			if (commerceEvent.NonInteractive.HasValue)
                bindingCommerceEvent.NonInteractive = commerceEvent.NonInteractive.Value;

			if (commerceEvent.Promotions != null)
			{
                bindingCommerceEvent.PromotionContainer = new MPPromotionContainer(ConvertToMpPromotionAction(commerceEvent.PromotionAction), null);
				foreach (var promotion in commerceEvent.Promotions)
				{
                    bindingCommerceEvent.PromotionContainer.AddPromotion(ConvertToMpPromotion(promotion));
				}
			}

			if (commerceEvent.Impressions != null)
			{
				foreach (var impression in commerceEvent.Impressions)
				{
                    foreach(var product in impression.Products){
                        bindingCommerceEvent.AddImpression(ConvertToMpProduct(product), impression.ImpressionListName);
                    }
				}
			}

            iOSBinding.MParticle.SharedInstance().LogCommerceEvent(bindingCommerceEvent);
        }

        private MPCommerceEventAction ConvertToMpProductAction(ProductAction action)
        {
            switch (action)
            {
                case ProductAction.AddToCart:
                    return MPCommerceEventAction.AddToCart;
                case ProductAction.AddToWishlist:
                    return MPCommerceEventAction.AddToWishList;
                case ProductAction.Checkout:
                    return MPCommerceEventAction.Checkout;
                case ProductAction.CheckoutOption:
                    return MPCommerceEventAction.CheckoutOptions;
                case ProductAction.Click:
                    return MPCommerceEventAction.Click;
                case ProductAction.Purchase:
                    return MPCommerceEventAction.Purchase;
                case ProductAction.Refund:
                    return MPCommerceEventAction.Refund;
                case ProductAction.RemoveFromWishlist:
                    return MPCommerceEventAction.RemoveFromWishlist;
                case ProductAction.RemoveFromCart:
                    return MPCommerceEventAction.RemoveFromCart;
                case ProductAction.ViewDetail:
                    return MPCommerceEventAction.ViewDetail;
                default:
                    return 0;
            }
        }

        private MPPromotionAction ConvertToMpPromotionAction(PromotionAction action)
		{
			switch (action)
			{
				case PromotionAction.Click:
                    return MPPromotionAction.Click;
				case PromotionAction.View:
				default:
					return MPPromotionAction.View;
			}
		}

        private MPPromotion ConvertToMpPromotion(Promotion promotion)
        {
            var bindingPromotion = new MPPromotion();
            bindingPromotion.Creative = promotion.Creative;
            bindingPromotion.PromotionId = promotion.Id;
            bindingPromotion.Name = promotion.Name;
            bindingPromotion.Position = promotion.Position?.ToString();
            return bindingPromotion;      
        }

        private MPProduct ConvertToMpProduct(Product product)
        {
            var bindingProduct = new MPProduct();
            bindingProduct.Sku = product.Sku;
            bindingProduct.Name = product.Name;
            bindingProduct.UnitPrice = product.Price;
            bindingProduct.Quantity = new NSNumber(product.Quantity);
            bindingProduct.Brand = product.Brand;
            bindingProduct.Category = product.Category;
            bindingProduct.CouponCode = product.CouponCode;
            bindingProduct.Variant = product.Variant;

            if (product.customAttributes != null){
                foreach(var kvp in product.customAttributes){
                    bindingProduct.SetObject( (NSObject) new NSString(kvp.Value), new NSString(kvp.Key));
				}
            }
            return bindingProduct;
        }

        private MPTransactionAttributes ConvertToMpTransactionAttributes(TransactionAttributes transactionAttributes)
        {
            var bindingTransactions = new MPTransactionAttributes();
            bindingTransactions.TransactionId = transactionAttributes.TransactionId;
			bindingTransactions.Affiliation = transactionAttributes.Affiliation;
			bindingTransactions.CouponCode = transactionAttributes.CouponCode;
            bindingTransactions.Shipping = transactionAttributes.Shipping.HasValue ? transactionAttributes.Shipping.Value : 0;
            bindingTransactions.Tax = transactionAttributes.Tax.HasValue ? transactionAttributes.Tax.Value : 0;
            bindingTransactions.Revenue = transactionAttributes.Revenue.HasValue ? transactionAttributes.Revenue.Value : 0;
            return bindingTransactions;
        }

        public override void LogEvent(string eventName, EventType eventType, Dictionary<string, string> eventInfo)
        {
            iOSBinding.MParticle.SharedInstance().LogEvent(eventName, (MPEventType)Enum.Parse(typeof(iOSBinding.MPEventType), eventType.ToString()), ConvertToNSDictionary<NSString, NSObject>(eventInfo));
		}

        public override void Logout()
        {
            iOSBinding.MParticle.SharedInstance().Logout();
        }

        public override void LogScreen(string screenName, Dictionary<string, string> eventInfo)
        {
            iOSBinding.MParticle.SharedInstance().LogScreen(screenName, ConvertToNSDictionary<NSString, NSObject>(eventInfo));
		}

        public override void RemoveUserAttribute(string key)
        {
            iOSBinding.MParticle.SharedInstance().RemoveUserAttribute(key);
        }

        public override void SetOptOut(bool optOut)
        {
            iOSBinding.MParticle.SharedInstance().OptOut = optOut;
        }

        public override void SetUserAttribute(string key, string val)
        {
            iOSBinding.MParticle.SharedInstance().SetUserAttribute(key, new NSString(val));
        }

        public override void SetUserAttributeArray(string key, string[] values)
        {
            iOSBinding.MParticle.SharedInstance().SetUserAttribute(key, values);              		
        }

        public override void SetUserIdentity(string identity, UserIdentity identityType)
        {
            iOSBinding.MParticle.SharedInstance().SetUserIdentity(identity, (iOSBinding.MPUserIdentity)Enum.Parse(typeof(iOSBinding.MPUserIdentity), identityType.ToString()));
		}

        public override void SetUserTag(string tag)
        {
            iOSBinding.MParticle.SharedInstance().SetUserTag(tag);
        }

        private static NSDictionary<T, V> ConvertToNSDictionary<T, V>(Dictionary<string, string> dictionary) where T : NSString where V : NSObject
        {
            if (dictionary == null || !dictionary.Any())
                return new NSDictionary<T,V>();
            
            return NSDictionary<T, V>.FromObjectsAndKeys(dictionary.Values.ToArray(), dictionary.Keys.ToArray());
        }

        public override object GetBindingInstance()
        {
            return iOSBinding.MParticle.SharedInstance();
        }
    }
}
