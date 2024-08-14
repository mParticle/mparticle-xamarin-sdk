using System;
using System.Collections.Generic;
using Foundation;
using mParticle.MAUI.iOSBinding;

namespace mParticle.MAUI.iOS.Utils
{
    public class IdentityApiWrapper : IdentityApi
    {
        private iOSBinding.MPIdentityApi _identityApi;

        private IdentityApiWrapper(){}

        private static IdentityApiWrapper _instance;

        internal static IdentityApiWrapper GetInstance(iOSBinding.MPIdentityApi identityApi)
        {
            if (_instance == null) {
                _instance = new IdentityApiWrapper();
            }
            _instance._identityApi = identityApi;
            return _instance;
        }

        private static List<OnUserIdentified> _identityStateListeners;
        public override void AddIdentityStateListener(OnUserIdentified listener)
        {
            if (listener != null)
            {
                if (_identityStateListeners == null)
                {
                    NSNotificationCenter.DefaultCenter.AddObserver(new NSString("mParticleIdentityStateChangeListenerNotification"), OnUserIdentifiedCallback);
                    _identityStateListeners = new List<OnUserIdentified>();
                }
                _identityStateListeners.Add(listener);
            }
        }

        public void OnUserIdentifiedCallback(NSNotification notification)
        {
            if (_identityStateListeners != null)
            {
                var localListeners = new List<OnUserIdentified>(_identityStateListeners);
                localListeners?.ForEach(list => list.Invoke(new MParticleUserWrapper((iOSBinding.MParticleUser)notification.UserInfo["mParticleUserKey"])));
            }
        }

        public override void RemoveIdentityStateListener(OnUserIdentified listener)
        {
            if (listener != null)
            {
                _identityStateListeners?.Remove(listener);
            }
        }

        public override MParticleUser CurrentUser
        {
            get
            {
                return new MParticleUserWrapper(iOSBinding.MParticle.SharedInstance.Identity.CurrentUser());
            }
        }

        public override IMParticleTask<IdentityApiResult> Identify(IdentityApiRequest request = null)
        {
            BaseTaskWrapper task = new BaseTaskWrapper();
            MPIdentityApiResultCallback callback = (MPIdentityApiResult apiResult, Foundation.NSError error) =>
            {
                if (error != null)
                {
                    task.Failure = new IdentityHttpResponseWrapper(error);
                }
                if (apiResult != null && apiResult.User != null)
                {
                    task.Result = new IdentityApiResult()
                    {
                        User = new MParticleUserWrapper(apiResult.User)
                    };
                }
            };
            iOSBinding.MParticle.SharedInstance.Identity.Identify(Utils.ConvertToMpIdentityRequest(request), callback);
            return task;
        }

        public override IMParticleTask<IdentityApiResult> Login(IdentityApiRequest request = null)
        {
            BaseTaskWrapper task = new BaseTaskWrapper();
            MPIdentityApiResultCallback callback = (MPIdentityApiResult apiResult, Foundation.NSError error) =>
            {
                if (error != null)
                {
                    task.Failure = new IdentityHttpResponseWrapper(error);
                }
                if (apiResult != null && apiResult.User != null)
                {
                    task.Result = new IdentityApiResult()
                    {
                        User = apiResult != null ? new MParticleUserWrapper(apiResult.User) : null
                    };
                }
            };
            _identityApi.Login(Utils.ConvertToMpIdentityRequest(request), callback);
            return task;
        }

        public override IMParticleTask<IdentityApiResult> Logout(IdentityApiRequest request = null)
        {
            BaseTaskWrapper task = new BaseTaskWrapper();
            MPIdentityApiResultCallback callback = (MPIdentityApiResult apiResult, Foundation.NSError error) =>
            {
                if (error != null)
                {
                    task.Failure = new IdentityHttpResponseWrapper(error);
                }
                if (apiResult != null && apiResult.User != null)
                {
                    task.Result = new IdentityApiResult()
                    {
                        User = apiResult != null ? new MParticleUserWrapper(apiResult.User) : null
                    };
                }
            };
            _identityApi.Logout(Utils.ConvertToMpIdentityRequest(request), callback);
            return task;
        }

        public override IMParticleTask<IdentityApiResult> Modify(IdentityApiRequest request)
        {
            BaseTaskWrapper task = new BaseTaskWrapper();
            MPIdentityApiResultCallback callback = (MPIdentityApiResult apiResult, Foundation.NSError error) =>
            {
                if (error != null)
                {
                    task.Failure = new IdentityHttpResponseWrapper(error);
                }
                if (apiResult != null && apiResult.User != null)
                {
                    task.Result = new IdentityApiResult()
                    {
                        User = apiResult != null ? new MParticleUserWrapper(apiResult.User) : null
                    };
                }
            };
            _identityApi.Modify(Utils.ConvertToMpIdentityRequest(request), callback);
            return task;
        }

    }
}
