namespace mParticle.MAUI.Android.Wrappers;

public class MParticleTaskWrapper : IMParticleTask<IdentityApiResult>
{
    Android.IdentityBinding.BaseIdentityTask _task;

    internal MParticleTaskWrapper(Android.IdentityBinding.BaseIdentityTask task)
    {
        _task = task;
    }

    public IMParticleTask<IdentityApiResult> AddFailureListener(OnFailure listener)
    {
        _task.AddFailureListener(new TaskFailureWrapper(listener));
        return this;
    }

    public IMParticleTask<IdentityApiResult> AddSuccessListener(OnSuccess listener)
    {
        _task.AddSuccessListener(new TaskSuccessWrapper(listener));
        return this;
    }

    public IdentityApiResult GetResult()
    {
        if (_task.Result.User == null)
        {
            return null;
        }
        return new IdentityApiResult()
        {
            User = new MParticleUserWrapper(_task.Result.User)
        };
    }

    public bool IsComplete()
    {
        return _task.IsComplete;
    }

    public bool IsSuccessful()
    {
        return _task.IsSuccessful;
    }
}

internal class TaskFailureWrapper : Java.Lang.Object, Android.IdentityBinding.ITaskFailureListener
{
    private OnFailure _listener;

    internal TaskFailureWrapper(OnFailure listener)
    {
        _listener = listener;
    }

    public void OnFailure(IdentityBinding.IdentityHttpResponse result)
    {
        if (_listener != null)
        {
            _listener.Invoke(new IdentityHttpResponse()
            {
                Errors = result.Errors.Select(arg => new Error()
                {
                    Message = arg.Message,
                    Code = arg.Code
                }).ToList(),
                IsSuccessful = result.IsSuccessful,
                HttpCode = result.HttpCode,
            });
        }
    }
}

class TaskSuccessWrapper : Java.Lang.Object, Android.IdentityBinding.ITaskSuccessListener
{
    private OnSuccess _listener;

    public TaskSuccessWrapper(OnSuccess listener)
    {
        _listener = listener;
    }

    public void OnSuccess(IdentityBinding.IdentityApiResult result)
    {
        if (_listener != null)
        {
            var boundResult = new IdentityApiResult();
            if (result != null && result.User != null)
            {
                boundResult.User = new MParticleUserWrapper(result.User);
            }
            else
            {
                boundResult.User = null;
            }
            _listener.Invoke(boundResult);
        }
    }
}
