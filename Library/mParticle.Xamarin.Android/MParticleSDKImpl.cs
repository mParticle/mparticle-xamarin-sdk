using System.Collections.Generic;
using mParticle.Xamarin.Android.Wrappers;
using mParticle.Xamarin.Android;
using Java.Lang;
using System;

namespace mParticle.Xamarin
{
    public class MParticleSDKImpl : MParticleSDK
    {
        internal MParticleSDKImpl()
        {
        }

        public override Environment Environment
        {
            get
            {
                var environment = AndroidBinding.MParticle.Instance.GetEnvironment();
                if (environment == AndroidBinding.MParticle.Environment.AutoDetect)
                    return Xamarin.Environment.AutoDetect;
                else if (environment == AndroidBinding.MParticle.Environment.Production)
                    return Xamarin.Environment.Production;
                else
                    return Xamarin.Environment.Development;
            }
        }


        public override void LeaveBreadcrumb(string breadcrumbName)
        {
            AndroidBinding.MParticle.Instance.LeaveBreadcrumb(breadcrumbName);
        }

        public override void LogCommerceEvent(CommerceEvent commerceEvent)
        {
            Android.CommerceBinding.CommerceEvent.Builder bindingCommerceEventBuilder = null;

            if (commerceEvent.ProductAction > 0 && commerceEvent.Products != null && commerceEvent.Products.Length > 0)
                bindingCommerceEventBuilder = new Android.CommerceBinding.CommerceEvent.Builder(Utils.ConvertToMpProductAction(commerceEvent.ProductAction), Utils.ConvertToMpProduct(commerceEvent.Products[0]));

            else if (commerceEvent.Promotions != null && commerceEvent.Promotions.Length > 0)
                bindingCommerceEventBuilder = new Android.CommerceBinding.CommerceEvent.Builder(Utils.ConvertToMpPromotionAction(commerceEvent.PromotionAction), Utils.ConvertToMpPromotion(commerceEvent.Promotions[0]));

            else
                bindingCommerceEventBuilder = new Android.CommerceBinding.CommerceEvent.Builder(Utils.ConvertToMpImpression(commerceEvent.Impressions[0]));


            if (bindingCommerceEventBuilder == null)
                return;

            if (commerceEvent.TransactionAttributes != null)
                bindingCommerceEventBuilder.TransactionAttributes(Utils.ConvertToMpTransactionAttributes(commerceEvent.TransactionAttributes));

            bindingCommerceEventBuilder.Screen(commerceEvent.ScreenName);
            bindingCommerceEventBuilder.Currency(commerceEvent.Currency);
            bindingCommerceEventBuilder.CustomAttributes(commerceEvent.CustomAttributes);
            bindingCommerceEventBuilder.CheckoutOptions(commerceEvent.CheckoutOptions);

            if (commerceEvent.Products != null)
            {
                foreach (var product in commerceEvent.Products)
                {
                    bindingCommerceEventBuilder.AddProduct(Utils.ConvertToMpProduct(product));
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
                    bindingCommerceEventBuilder.AddPromotion(Utils.ConvertToMpPromotion(promotion));
                }
            }

            if (commerceEvent.Impressions != null)
            {
                foreach (var impression in commerceEvent.Impressions)
                {
                    bindingCommerceEventBuilder.AddImpression(Utils.ConvertToMpImpression(impression));
                }
            }

            AndroidBinding.MParticle.Instance.LogEvent(bindingCommerceEventBuilder.Build());
        }

        public override void LogEvent(string eventName, EventType eventType, Dictionary<string, string> eventInfo)
        {
            var mpEventType = AndroidBinding.MParticle.EventType.ValueOf(eventType.ToString());
            var mpEvent = new AndroidBinding.MPEvent.Builder(eventName, mpEventType).Info(eventInfo).Build();
            AndroidBinding.MParticle.Instance.LogEvent(mpEvent);
        }

        public override void LogScreen(string screenName, Dictionary<string, string> eventInfo)
        {
            AndroidBinding.MParticle.Instance.LogScreen(screenName, eventInfo);
        }

        public override void SetOptOut(bool optOut)
        {
            AndroidBinding.MParticle.Instance.OptOut = new Java.Lang.Boolean(optOut);
        }

        public override object GetBindingInstance()
        {
            return AndroidBinding.MParticle.Instance;
        }

        public override MParticleSDK Initialize(MParticleOptions options)
        {
            AndroidBinding.MParticleOptions boundOptions = Utils.ConvertToMpOptions(options);
            AndroidBinding.MParticle.Start(boundOptions);
            var instance = new MParticleSDKImpl();;
            if (options.IdentityStateListener != null)
            {
                instance.Identity.AddIdentityStateListener(options.IdentityStateListener);
            }
            return instance;
        }

        public override IdentityApi Identity
        {
            get
            {
                return IdentityApiWrapper.GetInstance(AndroidBinding.MParticle.Instance.Identity());
            }
        }

        public override void Destroy()
        {
            AndroidBinding.MParticle.Instance = null;
        }

        public override void Upload()
        {
            AndroidBinding.MParticle.Instance.Upload();
        }
    }
}
