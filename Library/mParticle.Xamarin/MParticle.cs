using System;
namespace mParticle.Xamarin
{
    public static class MParticle
    {
        static Lazy<MParticleSDKBase> TTS = new Lazy<MParticleSDKBase>(() => CreateInstance(), System.Threading.LazyThreadSafetyMode.PublicationOnly);

        public static MParticleSDKBase Instance
        {
            get
            {
                var ret = TTS.Value;
                if (ret == null)
                {
                    throw NotImplementedInReferenceAssembly();
                }
                return ret;
            }
        }

        static MParticleSDKBase CreateInstance()
        {
            #if PORTABLE
                return null;
            #else
                return new MParticleSDK();
            #endif
        }

        internal static Exception NotImplementedInReferenceAssembly()
        {
            return new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the mParticle.Xamarin NuGet package from your main application project in order to reference the platform-specific implementation.");
        }
    }
}
