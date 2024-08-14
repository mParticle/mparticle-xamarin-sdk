namespace mParticle.MAUI.Android.Wrappers;

public class MParticleUserWrapper : MParticleUser
{
    private Android.IdentityBinding.IMParticleUser _user;

    internal MParticleUserWrapper(Android.IdentityBinding.IMParticleUser user)
    {
        _user = user;
    }

    public override long Mpid => _user.Id;

    public override Dictionary<string, string> GetUserAttributes()
    {
        return _user.UserAttributes
                             .ToList()
                             .ToDictionary(key => key.Key, val => val.Value?.ToString());
    }

    public override Dictionary<UserIdentity, string> GetUserIdentities()
    {
        return _user.UserIdentities
                             .ToList()
                             .ToDictionary(key => (UserIdentity)Enum.Parse(typeof(UserIdentity), key.Key.ToString()), val => val.Value);
    }

    public override void SetUserAttribute(string key, string val)
    {
        _user.SetUserAttribute(key, val);
    }

    public override void SetUserAttributes(Dictionary<string, string> userAttributes)
    {
        _user.SetUserAttributes(new Dictionary<string, Java.Lang.Object>(userAttributes
                                                                         .Select(pair => new KeyValuePair<string, Java.Lang.Object>(pair.Key, pair.Value))));
    }

    public override void SetUserTag(string tag)
    {
        _user.SetUserTag(tag);
    }
}

