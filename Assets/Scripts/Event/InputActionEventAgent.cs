using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class InputActionEventAgent : MonoBehaviour
{
    [SerializeField] private InputActionEventSO _onInputActionEventSO;
    [SerializeField] private UnityEvent _raiseEvents;

    private void Awake()
    {
        Debug.Assert(_onInputActionEventSO != null);
    }

    private void OnEnable()
    {
        _onInputActionEventSO.OnEvent += OnInputAction;
    }

    private void OnDisable()
    {
        _onInputActionEventSO.OnEvent -= OnInputAction;
    }

    protected virtual void OnInputAction(InputAction.CallbackContext context)
    {
        _raiseEvents.Invoke();
    }
}
