namespace mParticle.MAUI.Android.Wrappers;

public class UserAliasHandlerWrapper : Java.Lang.Object, Android.IdentityBinding.IUserAliasHandler
{
    private OnUserAlias _handler;

    internal UserAliasHandlerWrapper(OnUserAlias handler)
    {
        _handler = handler;
    }

    void IdentityBinding.IUserAliasHandler.OnUserAlias(Android.IdentityBinding.IMParticleUser previousUser, Android.IdentityBinding.IMParticleUser newUser)
    {
        if (_handler != null)
        {
            MParticleUserWrapper boundPreviousUser = null;
            MParticleUserWrapper boundNewUser = null;
            if (previousUser != null)
            {
                boundPreviousUser = new MParticleUserWrapper(previousUser);
            }
            if (newUser != null)
            {
                boundNewUser = new MParticleUserWrapper(newUser);
            }
            _handler.Invoke(boundPreviousUser, boundNewUser);
        }
    }
}
