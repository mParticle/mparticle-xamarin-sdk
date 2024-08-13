﻿using System;

namespace mParticle.Xamarin
{
    public static class MParticle
    {
        static Lazy<MParticleSDK> TTS = new Lazy<MParticleSDK>(() => CreateInstance(), System.Threading.LazyThreadSafetyMode.PublicationOnly);

        public static MParticleSDK Instance
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

        static MParticleSDK CreateInstance()
        {
            return new MParticleSDKImpl();
        }

        internal static Exception NotImplementedInReferenceAssembly()
        {
            return new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the mParticle.Xamarin NuGet package from your main application project in order to reference the platform-specific implementation.");
        }
    }

}

