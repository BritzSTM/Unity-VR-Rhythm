using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "new debug void event so", menuName = "Game/Event/DebugVoid")]
public class VoidEventSOForDebug : CommentableSO
{
    public event UnityAction OnEvent;

    public void RaiseEvent()
    {
        Debug.Log("On! void Event");
    }
}
