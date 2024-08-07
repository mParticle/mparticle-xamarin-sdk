using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using mParticle.MAUI.iOS.Utils;
using mParticle.MAUI.iOSBinding;

namespace mParticle.MAUI
{
    public class MParticleSDKImpl : MParticleSDK
    {

        internal MParticleSDKImpl()
        {
        }

        public override mParticle.MAUI.Environment Environment
        {
            get
            {
                switch (iOSBinding.MParticle.SharedInstance.Environment)
                {
                    case iOSBinding.MPEnvironment.Development:
                        return mParticle.MAUI.Environment.Development;
                    case iOSBinding.MPEnvironment.Production:
                        return mParticle.MAUI.Environment.Production;
                    default:
                        return mParticle.MAUI.Environment.AutoDetect;
                }
            }
        }

        public override MParticleSDK Initialize(MParticleOptions options)
        {
            if (options.IdentityStateListener != null)
            {
                Identity.AddIdentityStateListener(options.IdentityStateListener);
            }
            iOSBinding.MParticle.SharedInstance.StartWithOptions(Utils.ConvertToMpOptions(options));
            var mparticle = iOSBinding.MParticle.SharedInstance;
            mparticle.LogLevel = Utils.ConvertToMpLogLevel(options.LogLevel);
            if (options.LocationTracking != null && options.LocationTracking.Enabled)
            {
                mparticle.BeginLocationTracking(options.LocationTracking.MinDistance, options.LocationTracking.MinDistance);
            }
            else
            {
                mparticle.EndLocationTracking();
            }
            if (options.PushRegistration != null && options.PushRegistration.IOSToken != null)
            {
                mparticle.PushNotificationToken = options.PushRegistration.IOSToken;
            }
            return this;
        }

        public override void LeaveBreadcrumb(string breadcrumbName)
        {
            iOSBinding.MParticle.SharedInstance.LeaveBreadcrumb(breadcrumbName);
        }

        public override void LogCommerceEvent(CommerceEvent commerceEvent, bool shouldUploadEvent = true)
        {
            var bindingCommerceEvent = new iOSBinding.MPCommerceEvent();

            if (commerceEvent.TransactionAttributes != null)
                bindingCommerceEvent.TransactionAttributes = Utils.ConvertToMpTransactionAttributes(commerceEvent.TransactionAttributes);

            bindingCommerceEvent.ScreenName = commerceEvent.ScreenName;
            bindingCommerceEvent.Currency = commerceEvent.Currency;
            bindingCommerceEvent.SetCustomAttributes(ConvertToNSDictionary<NSString, NSObject>(commerceEvent.CustomAttributes));
            bindingCommerceEvent.CheckoutOptions = commerceEvent.CheckoutOptions;

            if (commerceEvent.Products != null)
            {
                bindingCommerceEvent.Action = ConvertToMpProductAction(commerceEvent.ProductAction);
                foreach (var product in commerceEvent.Products)
                {
                    bindingCommerceEvent.AddProduct(Utils.ConvertToMpProduct(product));
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
                    foreach (var product in impression.Products)
                    {
                        bindingCommerceEvent.AddImpression(Utils.ConvertToMpProduct(product), impression.ImpressionListName);
                    }
                }
            }

            bindingCommerceEvent.ShouldUploadEvent = shouldUploadEvent;

            iOSBinding.MParticle.SharedInstance.LogCommerceEvent(bindingCommerceEvent);
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

        public override void LogEvent(string eventName, EventType eventType, Dictionary<string, string> eventInfo, bool shouldUploadEvent = true)
        {
            var mpEventType = (MPEventType)Enum.Parse(typeof(iOSBinding.MPEventType), eventType.ToString());
            var mpEvent = new MPEvent(eventName, mpEventType);
            mpEvent.CustomAttributes = ConvertToNSDictionary<NSString, NSObject>(eventInfo);
            mpEvent.ShouldUploadEvent = shouldUploadEvent;
            iOSBinding.MParticle.SharedInstance.LogEvent(mpEvent);
        }

        public override void LogScreen(string screenName, Dictionary<string, string> eventInfo)
        {
            iOSBinding.MParticle.SharedInstance.LogScreen(screenName, ConvertToNSDictionary<NSString, NSObject>(eventInfo));
        }

        public override void SetATTStatus(MPATTAuthorizationStatus status, long? attStatusTimestampMillis)
        {
            iOSBinding.MParticle.SharedInstance.setATTStatus((iOSBinding.MPATTAuthorizationStatus)status, attStatusTimestampMillis);
        }

        public override void SetOptOut(bool optOut)
        {
            iOSBinding.MParticle.SharedInstance.OptOut = optOut;
        }



        private static NSDictionary<T, V> ConvertToNSDictionary<T, V>(Dictionary<string, string> dictionary) where T : NSString where V : NSObject
        {
            if (dictionary == null || !dictionary.Any())
                return new NSDictionary<T, V>();

            return NSDictionary<T, V>.FromObjectsAndKeys(dictionary.Values.ToArray(), dictionary.Keys.ToArray());
        }

        public override object GetBindingInstance()
        {
            return iOSBinding.MParticle.SharedInstance;
        }

        public override IdentityApi Identity
        {
            get
            {
                return IdentityApiWrapper.GetInstance(iOSBinding.MParticle.SharedInstance.Identity);
            }
        }

        public override void Destroy()
        {
            iOSBinding.MParticle.SharedInstance = null;
        }

        public override void Upload()
        {
            iOSBinding.MParticle.SharedInstance.Upload();
        }
    }
}