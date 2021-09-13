using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 현재 XR 모듈에서는 anykey가 지원되지 않는다.
/// 따라서 수동으로 입력액션들을 할당해 이를 추적하여 any key 이벤트를 일으킨다.
/// 그리고 1.1.1 버전 이후로 부터는 공식 API에서 지원할 예정이므로
/// 하위 버전을 사용할 때만 사용한다.
/// </summary>
public class InputDeviceAnyKey : MonoBehaviour
{
    [SerializeField] private InputActionEventSO _onAnyKeyEventSO;
    [SerializeField] private InputActionReference[] _anyKeys;

    private void OnEnable()
    {
        foreach(var key in _anyKeys)
        {
            key.action.performed += OnAnykeyEvent;
        }
    }

    private void OnDisable()
    {
        foreach (var key in _anyKeys)
        {
            key.action.performed -= OnAnykeyEvent;
        }
    }

    private void OnAnykeyEvent(InputAction.CallbackContext context)
    {
        if (_onAnyKeyEventSO != null)
            _onAnyKeyEventSO.RaiseEvent(context);
    }
}
