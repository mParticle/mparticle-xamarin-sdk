﻿using System;
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

        public void OnUserIdentified(IdentityBinding.IMParticleUser newUser, IdentityBinding.IMParticleUser previousUser)
        {
            if (_listener != null)
            {
                _listener.Invoke(newUser != null ? new MParticleUserWrapper(newUser) : null);
            }
        }
    }
}
