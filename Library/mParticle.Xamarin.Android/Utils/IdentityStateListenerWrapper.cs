using System;
using mParticle.Xamarin.Android.Wrappers;

namespace mParticle.Xamarin.Android
{
    public class IdentityStateListenerWrapper : Java.Lang.Object, Android.IdentityBinding.IIdentityStateListener
    {
        private OnUserIdentified _listener;

        internal IdentityStateListenerWrapper(OnUserIdentified listener)
        {
            _listener = listener;
        }

        public void OnUserIdentified(IdentityBinding.MParticleUser user)
        {
            if (_listener != null)
            {
                _listener.Invoke(user != null ? new MParticleUserWrapper(user) : null);
            }
        }
    }
}
