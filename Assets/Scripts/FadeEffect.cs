using UnityEngine;

public class FadeEffect : MonoBehaviour
{
    [Header("Raise")]
    [SerializeField] private VoidEventSO _onEndFadeInEventSO;
    [SerializeField] private VoidEventSO _onEndFadeOutEventSO;
    [SerializeField] private AudioClipEventSO _requirePlaySoundFXSO;

    [Header("FXs")]
    [SerializeField] private AudioClip _inSoundFX;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void StartFadeIn()
    {
        _animator.SetTrigger("FadeIn");
        _requirePlaySoundFXSO?.RaiseEvent(_inSoundFX);
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
