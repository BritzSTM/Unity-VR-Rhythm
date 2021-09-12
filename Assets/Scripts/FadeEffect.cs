using UnityEngine;

public class FadeEffect : MonoBehaviour
{
    [Header("Raise")]
    [SerializeField] private VoidEventSO _onEndFadeInEventSO;
    [SerializeField] private VoidEventSO _onEndFadeOutEventSO;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void StartFadeIn()
    {
        _animator.SetTrigger("FadeIn");
    }

    public void StartFadeOut()
    {
        _animator.SetTrigger("FadeOut");
    }

    private void OnEndFadeIn()
    {
        if (_onEndFadeInEventSO != null)
            _onEndFadeInEventSO.RaiseEvent();
    }

    private void OnEndFadeOut()
    {
        if (_onEndFadeOutEventSO != null)
            _onEndFadeOutEventSO.RaiseEvent();
    }
}
