using System;

namespace GoalApp.ErrorWrapping;

public class ActionResult<T>
{
    public T Result { get; set; }
    
    public Exception Error { get; set; }

    public bool Success => Error == null && Result != null;
}

public class ActionResult
{
    public bool Success { get; private set; }

    private Exception _error;

    public Exception Error
    {
        get => _error;
        set
        {
            Success = false;
            _error = value;
        }
    }

    public ActionResult() => Success = true;
}