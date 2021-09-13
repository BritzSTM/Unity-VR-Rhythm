using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundWhenOnEvent : MonoBehaviour
{
    [Header("Recive Event")]
    [SerializeField] private AudioClipEventSO _requirePlaySO;
    [SerializeField] private AudioClipEventSO _requirePlayOneShotSO;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        if(_requirePlaySO != null)
            _requirePlaySO.OnEvent += RequirePlaySound;

        if(_requirePlayOneShotSO != null)
            _requirePlayOneShotSO.OnEvent += RequirePlaySoundOneShot;
    }

    private void OnDisable()
    {
        if (_requirePlaySO != null)
            _requirePlaySO.OnEvent -= RequirePlaySound;

        if (_requirePlayOneShotSO != null)
            _requirePlayOneShotSO.OnEvent -= RequirePlaySoundOneShot;
    }

    private void RequirePlaySound(AudioClip clip)
    {
        if (clip == null)
            return;

        _audioSource.clip = clip;
        _audioSource.Play();
    }

    private void RequirePlaySoundOneShot(AudioClip clip)
    {
        if (clip == null)
            return;

        _audioSource.PlayOneShot(clip);
    }
}
