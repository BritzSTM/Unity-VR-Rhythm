using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "new void event so", menuName = "Game/Event/Void")]
public class VoidEventSO : CommentableSO
{
    public event UnityAction OnEvent;

    public void RaiseEvent()
    {
        if (OnEvent != null)
            OnEvent.Invoke();
    }
}
