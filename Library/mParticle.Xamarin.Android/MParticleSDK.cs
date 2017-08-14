using System;
using System.Collections.Generic;
using Android.App;

namespace mParticle.Xamarin
{
    public class MParticleSDK : MParticleSDKBase
    {
		public MParticleSDK()
		{
		}

        public override Environment GetEnvironment()
		{
			var environment = AndroidBinding.MParticle.Instance.GetEnvironment();
			if (environment == AndroidBinding.MParticle.Environment.AutoDetect)
				return Environment.AutoDetect;
			else if (environment == AndroidBinding.MParticle.Environment.Production)
				return Environment.Production;
			else
				return Environment.Development;
		}

		public override int IncrementUserAttribute(string key, int incrementValue)
		{
			AndroidBinding.MParticle.Instance.IncrementUserAttribute(key, incrementValue);
			return 0;
		}

		public override void Initialize(string apiKey, string apiSecret)
		{
            AndroidBinding.MParticle.Start(Application.Context.ApplicationContext, apiKey, apiSecret);
		}

		public override void LeaveBreadcrumb(string breadcrumbName)
		{
			AndroidBinding.MParticle.Instance.LeaveBreadcrumb(breadcrumbName);
		}

		public override void LogCommerceEvent(CommerceEvent commerceEvent)
		{
            Android.CommerceBinding.CommerceEvent.Builder bindingCommerceEventBuilder = null;

			if (commerceEvent.ProductAction > 0 && commerceEvent.Products != null && commerceEvent.Products.Length > 0)
				bindingCommerceEventBuilder = new Android.CommerceBinding.CommerceEvent.Builder(ConvertToMpProductAction(commerceEvent.ProductAction), ConvertToMpProduct(commerceEvent.Products[0]));
			
			else if (commerceEvent.Promotions != null && commerceEvent.Promotions.Length > 0)
				bindingCommerceEventBuilder = new Android.CommerceBinding.CommerceEvent.Builder(ConvertToMpPromotionAction(commerceEvent.PromotionAction), ConvertToMpPromotion(commerceEvent.Promotions[0]));
			
			else
				bindingCommerceEventBuilder = new Android.CommerceBinding.CommerceEvent.Builder(ConvertToMpImpression(commerceEvent.Impressions[0]));
			

			if (bindingCommerceEventBuilder == null)
				return;

			if (commerceEvent.TransactionAttributes != null)
				bindingCommerceEventBuilder.TransactionAttributes(ConvertToMpTransactionAttributes(commerceEvent.TransactionAttributes));

			bindingCommerceEventBuilder.Screen(commerceEvent.ScreenName);
			bindingCommerceEventBuilder.Currency(commerceEvent.Currency);
			bindingCommerceEventBuilder.CustomAttributes(commerceEvent.CustomAttributes);
			bindingCommerceEventBuilder.CheckoutOptions(commerceEvent.CheckoutOptions);

			if (commerceEvent.Products != null)
			{
				foreach (var product in commerceEvent.Products)
				{
					bindingCommerceEventBuilder.AddProduct(ConvertToMpProduct(product));
				}
			}

			if (commerceEvent.CheckoutStep != null)
				bindingCommerceEventBuilder.CheckoutStep(new Java.Lang.Integer(commerceEvent.CheckoutStep.Value));

			if (commerceEvent.NonInteractive.HasValue)
				bindingCommerceEventBuilder.NonInteraction(commerceEvent.NonInteractive.Value);

			if (commerceEvent.Promotions != null)
			{
				foreach (var promotion in commerceEvent.Promotions)
				{
					bindingCommerceEventBuilder.AddPromotion(ConvertToMpPromotion(promotion));
				}
			}

			if (commerceEvent.Impressions != null)
			{
				foreach (var impression in commerceEvent.Impressions)
				{
					bindingCommerceEventBuilder.AddImpression(ConvertToMpImpression(impression));
				}
			}

			AndroidBinding.MParticle.Instance.LogEvent(bindingCommerceEventBuilder.Build());
		}

		private string ConvertToMpProductAction(ProductAction action)
		{
			switch (action)
			{
				case ProductAction.AddToCart:
					return "add_to_cart";
				case ProductAction.AddToWishlist:
					return "add_to_wishlist";
				case ProductAction.Checkout:
					return "checkout";
				case ProductAction.CheckoutOption:
					return "checkout_option";
				case ProductAction.Click:
					return "click";
				case ProductAction.Purchase:
					return "purchase";
				case ProductAction.Refund:
					return "refund";
				case ProductAction.RemoveFromWishlist:
					return "remove_from_wishlist";
				case ProductAction.RemoveFromCart:
					return "remove_from_cart";
                case ProductAction.ViewDetail:
                    return "view_detail";
				default:
					return null;
			}
		}

		private string ConvertToMpPromotionAction(PromotionAction action)
		{
			switch (action)
			{
				case PromotionAction.Click:
					return "click";
				case PromotionAction.View:
					return "view";
				default:
					return null;
			}
		}

		private Android.CommerceBinding.Product ConvertToMpProduct(Product product)
		{
			var builder = new Android.CommerceBinding.Product
				.Builder(product.Name, product.Sku, product.Price)
				.CustomAttributes(product.customAttributes)
				.Category(product.Category)
				.CouponCode(product.CouponCode)
				.Quantity(product.Quantity)
				.Brand(product.Brand)
				.Variant(product.Variant);


			if (product.Position.HasValue)
				builder = builder.Position(new Java.Lang.Integer(product.Position.Value));

			return builder.Build();
		}

		private Android.CommerceBinding.Promotion ConvertToMpPromotion(Promotion promotion)
		{
			var bindingPromotion = new Android.CommerceBinding.Promotion();
			bindingPromotion.SetCreative(promotion.Creative);
			bindingPromotion.SetId(promotion.Id);
			bindingPromotion.SetName(promotion.Name);

			if (promotion.Position.HasValue)
				bindingPromotion.SetPosition(promotion.Position.Value.ToString());

			return bindingPromotion;
		}


		private Android.CommerceBinding.Impression ConvertToMpImpression(Impression impression)
		{
			var bindingImpression = new Android.CommerceBinding.Impression(impression.ImpressionListName, null);
			if (impression.Products != null)
			{
				foreach (var product in impression.Products)
				{
					bindingImpression.AddProduct(ConvertToMpProduct(product));
				}
			}

			return bindingImpression;
		}

		private Android.CommerceBinding.TransactionAttributes ConvertToMpTransactionAttributes(TransactionAttributes attributes)
		{
			var bindingTransaction = new Android.CommerceBinding.TransactionAttributes(attributes.TransactionId);
			bindingTransaction.SetCouponCode(attributes.CouponCode);
			bindingTransaction.SetAffiliation(attributes.Affiliation);

			if (attributes.Tax.HasValue)
				bindingTransaction.SetTax(new Java.Lang.Double(attributes.Tax.Value));

			if (attributes.Shipping.HasValue)
				bindingTransaction.SetShipping(new Java.Lang.Double(attributes.Shipping.Value));

			if (attributes.Revenue.HasValue)
				bindingTransaction.SetRevenue(new Java.Lang.Double(attributes.Revenue.Value));

			return bindingTransaction;
		}

		public override void LogEvent(string eventName, EventType eventType, Dictionary<string, string> eventInfo)
		{
			AndroidBinding.MParticle.Instance.LogEvent(eventName, AndroidBinding.MParticle.EventType.ValueOf(eventType.ToString()), eventInfo);
		}

		public override void Logout()
		{
			AndroidBinding.MParticle.Instance.Logout();
		}

		public override void LogScreen(string screenName, Dictionary<string, string> eventInfo)
		{
			AndroidBinding.MParticle.Instance.LogScreen(screenName, eventInfo);
		}

		public override void RemoveUserAttribute(string key)
		{
			AndroidBinding.MParticle.Instance.RemoveUserAttribute(key);
		}

		public override void SetOptOut(bool optOut)
		{
			AndroidBinding.MParticle.Instance.OptOut = new Java.Lang.Boolean(optOut);
		}

		public override void SetUserAttribute(string key, string val)
		{
			AndroidBinding.MParticle.Instance.SetUserAttribute(key, new Java.Lang.String(val));
		}

		public override void SetUserAttributeArray(string key, string[] values)
		{
			AndroidBinding.MParticle.Instance.SetUserAttributeList(key, values);
		}

		public override void SetUserIdentity(string identity, UserIdentity identityType)
		{
			AndroidBinding.MParticle.Instance.SetUserIdentity(identity, AndroidBinding.MParticle.IdentityType.ValueOf(identityType.ToString()));
		}

		public override void SetUserTag(string tag)
		{
			AndroidBinding.MParticle.Instance.SetUserTag(tag);
		}

        public override object GetBindingInstance()
        {
            return AndroidBinding.MParticle.Instance;
        }
    }
}
