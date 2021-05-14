using System;
using System.Linq;
using System.Collections.Generic;
using Foundation;
using System.Collections;

namespace mParticle.Xamarin.iOS.Utils
{
    public class MParticleUserWrapper : MParticleUser
    {
        private iOSBinding.MParticleUser _user;

        internal MParticleUserWrapper(iOSBinding.MParticleUser user)
        {
            _user = user;
        }

        public override long Mpid => _user.UserId.LongValue;

        public override Dictionary<string, string> GetUserAttributes()
        {
            return ConvertToXamUserAttributes(_user.UserAttributes);
        }

        public override Dictionary<UserIdentity, string> GetUserIdentities()
        {
            return ConvertToXamIdentities(_user.identities);
        }

        public override void SetUserAttribute(string key, string val)
        {
            _user.SetUserAttribute(new NSString(key), val == null ? null : new NSString(val));
        }

        public override void SetUserAttributes(Dictionary<string, string> userAttributes)
        {
            userAttributes.ToList().ForEach(pair => _user.SetUserAttribute(new NSString(pair.Key), new NSString(pair.Value)));
        }

        public override void SetUserTag(string tag)
        {
            _user.SetUserTag(new NSString(tag));
        }

        private Dictionary<string, string> ConvertToXamUserAttributes(NSDictionary<NSString, NSObject> dict)
        {
            Dictionary<string, string> stringMap = new Dictionary<string, string>();
            dict
                .Keys
                .ToList()
                .ForEach(key =>
            {
                NSObject val = null;
                if (dict.TryGetValue(key, out val))
                {
                    stringMap.Add(key.ToString(), val.ToString());
                }
            });
            return stringMap;
        }

        private Dictionary<UserIdentity, string> ConvertToXamIdentities(NSDictionary<NSNumber, NSString> dict)
        {
            Dictionary<UserIdentity, string> stringMap = new Dictionary<UserIdentity, string>();
            dict.Keys.ToList().ForEach(key =>
            {
                NSString val = null;
                if (dict.TryGetValue(key, out val))
                {
                    stringMap.Add((UserIdentity)Enum.Parse(typeof(UserIdentity), key.ToString()), val);
                }
            }
            );
            return stringMap;
        }
    }
}