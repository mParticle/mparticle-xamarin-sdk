using System;
namespace mParticle.Xamarin.iOS.Utils
{
    public class UserAliasWrapper : iOSBinding.OnUserAlias
    {
        IUserAliasHandler _aliasHandler;

        public UserAliasWrapper(IUserAliasHandler aliasHandler)
        {
            _aliasHandler = aliasHandler;
        }

       
    }
}
