using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using mParticle.Xamarin.iOSBinding;

namespace mParticle.Xamarin.iOS.Utils
{
    internal static class Utils
    {
        internal static iOSBinding.MPInstallationType ConvertToMpInstallType(InstallType installType)
        {
            switch (installType)
            {
                case InstallType.AutoDetect:
                    return iOSBinding.MPInstallationType.Autodetect;
                case InstallType.KnownInstall:
                    return iOSBinding.MPInstallationType.KnownInstall;
                case InstallType.KnownUpgrade:
                    return iOSBinding.MPInstallationType.KnownInstall;
                default:
                    return iOSBinding.MPInstallationType.Autodetect;
            }
        }

        internal static iOSBinding.MPEnvironment ConvertToMpEnvironment(Environment environment)
        {
            switch (environment)
            {
                case Environment.AutoDetect:
                    return iOSBinding.MPEnvironment.AutoDetect;
                case Environment.Development:
                    return iOSBinding.MPEnvironment.Development;
                case Environment.Production:
                    return iOSBinding.MPEnvironment.Production;
                default:
                    return iOSBinding.MPEnvironment.AutoDetect;
            }
        }

        internal static iOSBinding.MPILogLevel ConvertToMpLogLevel(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.INFO:
                case LogLevel.VERBOSE:
                    return iOSBinding.MPILogLevel.Verbose;
                case LogLevel.DEBUG:
                    return iOSBinding.MPILogLevel.Debug;
                case LogLevel.WARNING:
                    return iOSBinding.MPILogLevel.Warning;
                case LogLevel.ERROR:
                    return iOSBinding.MPILogLevel.Error;
                case LogLevel.NONE:
                    return iOSBinding.MPILogLevel.None;
                default:
                    if (iOSBinding.MParticle.SharedInstance.Environment.Equals(iOSBinding.MPEnvironment.Production))
                    {
                        return iOSBinding.MPILogLevel.None;
                    }
                    else
                    {
                        return iOSBinding.MPILogLevel.Debug;
                    }
            }
        }


        internal static iOSBinding.MParticleOptions ConvertToMpOptions(MParticleOptions options)
        {
            return new iOSBinding.MParticleOptions()
            {
                InstallType = ConvertToMpInstallType(options.InstallType),
                Environment = ConvertToMpEnvironment(options.Environment),
                ApiKey = options.ApiKey,
                ApiSecret = options.ApiSecret,
                IdentifyRequest = ConvertToMpIdentityRequest(options.IdentifyRequest),
                OnAttributionCompleted = ConvertToMpAttributionListener(options.AttributionListener),
                OnIdentifyComplete = ConvertToMpIdentifyCompleteListener(options.IdentityStateListener)
            };
        }

        internal static iOSBinding.MPProduct ConvertToMpProduct(Product product)
        {
            var bindingProduct = new iOSBinding.MPProduct();
            bindingProduct.Sku = product.Sku;
            bindingProduct.Name = product.Name;
            bindingProduct.UnitPrice = product.Price;
            bindingProduct.Quantity = new NSNumber(product.Quantity);
            bindingProduct.Brand = product.Brand;
            bindingProduct.Category = product.Category;
            bindingProduct.CouponCode = product.CouponCode;
            bindingProduct.Variant = product.Variant;

            if (product.customAttributes != null)
            {
                foreach (var kvp in product.customAttributes)
                {
                    bindingProduct.SetObject((NSObject)new NSString(kvp.Value), new NSString(kvp.Key));
                }
            }
            return bindingProduct;
        }

        internal static Product ConvertToXamProduct(iOSBinding.MPProduct product)
        {
            {
                if (product == null)
                {
                    return null;
                }
                var customKeys = product.AllKeys.ToDictionary(key => key.ToString(), key => product.ObjectForKeyedSubscript(key.ToString()).ToString());
                return new Product(product.Name, product.Sku, product.UnitPrice, product.Quantity.DoubleValue)
                {
                    Brand = product.Brand,
                    CouponCode = product.CouponCode,
                    Position = Convert.ToInt32(product.Position),
                    Category = product.Category,
                    Variant = product.Variant,
                    customAttributes = customKeys
                };
            }
        }

        internal static iOSBinding.MPIdentityApiRequest ConvertToMpIdentityRequest(IdentityApiRequest request)
        {
            var mpRequest = new iOSBinding.MPIdentityApiRequest();
            request.UserIdentities.ToList().ForEach(pair =>
            {
                mpRequest.UserIdentities.Add(
                    new NSNumber((float)(int)pair.Key),
                    new NSString(pair.Value));
            });
            mpRequest.OnUserAlias = new iOSBinding.OnUserAlias((previousUser, newUser) => request.UserAliasHandler.Invoke(new MParticleUserWrapper(previousUser), new MParticleUserWrapper(newUser)));
            return mpRequest;
        }

        internal static MPTransactionAttributes ConvertToMpTransactionAttributes(TransactionAttributes transactionAttributes)
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

        internal static iOSBinding.OnAttributionCompleted ConvertToMpAttributionListener(AttributionListener attributionListener)
        {
            return new iOSBinding.OnAttributionCompleted((attributionResult, nsError) =>
            {
                if (attributionResult != null && attributionListener.OnAttributionResult != null)
                {
                    attributionListener.OnAttributionResult(new AttributionResult()
                    {
                        //TODO 
                        //is this correct??
                        Parameters = attributionResult.KitName,
                        ServiceProviderId = attributionResult.KitCode.Int32Value,
                        LinkUrl = attributionResult.LinkInfo.Description
                    });
                }
                if (nsError != null && attributionListener.OnAttributionError != null)
                {
                    attributionListener.OnAttributionError(new AttributionError()
                    {
                        Message = nsError.ToString(),
                        ServiceProviderId = attributionResult.KitCode == null ? attributionResult.KitCode.Int32Value : 0
                    });
                }
            });
        }

        internal static iOSBinding.OnIdentifyComplete ConvertToMpIdentifyCompleteListener(OnUserIdentified identityListener)
        {
            return new iOSBinding.OnIdentifyComplete((request, error) =>
            {
                if (identityListener != null && request != null)
                {
                    identityListener.Invoke(new MParticleUserWrapper(request.User));
                }
                else
                {
                    if (error != null)
                    {
                        identityListener.Invoke(null);
                    }
                }
            });
        }
    }
}