using UnityEngine;

public class SceneLoaderWhenOnEvent : SceneLoader
{
    [SerializeField] private VoidEventSO _onEventSO;

    protected override void OnEnable()
    {
        base.OnEnable();

        if (_onEventSO != null)
            _onEventSO.OnEvent += OnEvent;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        if (_onEventSO != null)
            _onEventSO.OnEvent -= OnEvent;
    }

    private void OnEvent()
    {
        if (_onEventSO != null)
            _onEventSO.OnEvent -= OnEvent;

        LoadFromSceneStack();
    }
}
