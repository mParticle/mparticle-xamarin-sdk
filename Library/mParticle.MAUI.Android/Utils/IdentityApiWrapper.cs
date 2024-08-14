using mParticle.MAUI.Android.IdentityBinding;

namespace mParticle.MAUI.Android.Wrappers;

public class IdentityApiWrapper : IdentityApi
{
    Android.IdentityBinding.IdentityApi _identity;

    /**
     * This work about in neccessary, since the MParticleSDK tracks listeners internally via reference.
     * Since we are wrapping the listeners in a new object, we need to preserve the original 
     * wrapper reference for previously seen IdentityStateListeners. Failure to do this
     * will result in allowing multiple instances of the same listener to be added, and
     * removeIdentityStateListener to break entirely
     **/
    private static Dictionary<OnUserIdentified, IdentityStateListenerWrapper> _listenerInstanceWrapperMap = new Dictionary<OnUserIdentified, IdentityStateListenerWrapper>();

    private IdentityApiWrapper() {}

    private static IdentityApiWrapper _instance;

    internal static IdentityApiWrapper GetInstance(Android.IdentityBinding.IdentityApi identityApi)
    {
        if (_instance == null) {
            _instance = new IdentityApiWrapper();
        }
        _instance._identity = identityApi;
        return _instance;
    }

    public override void AddIdentityStateListener(OnUserIdentified listener)
    {
        IdentityStateListenerWrapper listenerWrapper;
        if (_listenerInstanceWrapperMap.TryGetValue(listener, out listenerWrapper))
        {
            _identity.AddIdentityStateListener(listenerWrapper);
        }
        else
        {
            var wrappedListener = new IdentityStateListenerWrapper(listener);
            _listenerInstanceWrapperMap.Add(listener, wrappedListener);
            _identity.AddIdentityStateListener(wrappedListener);
        }
    }

    public override void RemoveIdentityStateListener(OnUserIdentified listener)
    {
        IdentityStateListenerWrapper listenerWrapper;
        if (_listenerInstanceWrapperMap.TryGetValue(listener, out listenerWrapper))
        {
            _identity.RemoveIdentityStateListener(listenerWrapper);
        }
        else
        {
            _identity.RemoveIdentityStateListener(new IdentityStateListenerWrapper(listener));
        }
    }


    public override MParticleUser CurrentUser
    {
        get
        {
            if (_identity.CurrentUser == null)
            {
                return null;
            }
            return new MParticleUserWrapper(_identity.CurrentUser);
        }
    }

    public override IMParticleTask<IdentityApiResult> Identify(IdentityApiRequest request = null)
    {
        var task = _identity.Identify(Utils.ConvertToMpIdentityRequest(request));
        return task == null ? null : new MParticleTaskWrapper((BaseIdentityTask)task);
    }

    public override IMParticleTask<IdentityApiResult> Login(IdentityApiRequest request = null)
    {
        var task = _identity.Login(Utils.ConvertToMpIdentityRequest(request));
        return task == null ? null : new MParticleTaskWrapper((BaseIdentityTask)task);
    }

    public override IMParticleTask<IdentityApiResult> Logout(IdentityApiRequest request = null)
    {
        var task = _identity.Logout(Utils.ConvertToMpIdentityRequest(request));
        return task == null ? null : new MParticleTaskWrapper((BaseIdentityTask)task);
    }

    public override IMParticleTask<IdentityApiResult> Modify(IdentityApiRequest request)
    {
        var task = _identity.Modify(Utils.ConvertToMpIdentityRequest(request));
        return task == null ? null : new MParticleTaskWrapper((BaseIdentityTask)task);
    }
}
