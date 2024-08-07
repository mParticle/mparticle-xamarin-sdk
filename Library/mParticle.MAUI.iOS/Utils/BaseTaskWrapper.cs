using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;

namespace mParticle.MAUI.iOS.Utils
{
    public class BaseTaskWrapper : IMParticleTask<IdentityApiResult>
    {
        public Boolean Complete;
        public Boolean Successful;

        private IdentityApiResult _result;
        public IdentityApiResult Result
        {
            get
            {
                return _result;
            }

            set
            {
                _result = value;
                if (_result != null)
                {
                    Complete = true;
                    Successful = true;
                    _successListeners.ForEach(listener =>
                    {
                        if (listener != null)
                        {
                            listener.Invoke(_result);
                        }
                    });
                }
            }
        }

        private IdentityHttpResponse _failure;
        public IdentityHttpResponse Failure
        {
            get
            {
                return _failure;
            }

            set
            {
                _failure = value;
                if (_failure != null)
                {
                    Successful = false;
                    Complete = true;
                    _failureListeners.ForEach(listener =>
                    {
                        if (listener != null)
                        {
                            listener.Invoke(_failure);
                        }
                    });
                }
            }
        }

        List<OnFailure> _failureListeners = new List<OnFailure>();
        List<OnSuccess> _successListeners = new List<OnSuccess>();

        public IMParticleTask<IdentityApiResult> AddFailureListener(OnFailure listener)
        {
            if (Failure != null)
            {
                listener.Invoke(Failure);
            }
            else
            {
                _failureListeners.Add(listener);
            }
            return this;
        }


        public IMParticleTask<IdentityApiResult> AddSuccessListener(OnSuccess listener)
        {
            if (Result != null)
            {
                listener.Invoke(Result);
            }
            else
            {
                _successListeners.Add(listener);
            }
            return this;
        }

        public IdentityApiResult GetResult()
        {
            return Result;
        }

        public bool IsComplete()
        {
            return Complete;
        }

        public bool IsSuccessful()
        {
            return Successful;
        }
    }

    class IdentityHttpResponseWrapper : IdentityHttpResponse
    {

        public IdentityHttpResponseWrapper(NSError error)
        {
            IsSuccessful = (error == null);
            Errors.Add(new Error()
            {
                Message = error.ToString(),
                Code = error.Code.ToString()
            });
            HttpCode = Convert.ToInt32(error.Code);
        }
    }
}