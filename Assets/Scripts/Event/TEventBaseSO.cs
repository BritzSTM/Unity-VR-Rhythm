using UnityEngine.Events;

public class TEventBaseSO<T> : CommentableSO
{
    public event UnityAction<T> OnEvent;

    public void RaiseEvent(T arg)
    {
        if (OnEvent != null)
            OnEvent.Invoke(arg);
    }
}
