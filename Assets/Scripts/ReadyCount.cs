using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ReadyCount : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private GameObject _APanel;
    [SerializeField] private GameObject _NPanel;

    [SerializeField] private AudioSource _soundFXPlayer;
    [SerializeField] private AudioClip[] _countDownSoundFXs;

    public event UnityAction OnStart;

    private Animator _animator;
    private int _count;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _count = 4;
        _text.text = "READY";
    }

    private void OnRotationFinish()
    {
        --_count;

        if (_count > 0)
        {
            _APanel.SetActive(false);
            _NPanel.SetActive(true);
            _text.text = _count.ToString();
            _soundFXPlayer.PlayOneShot(_countDownSoundFXs[_count]);
        }
        else if (_count == 0)
        {
            _APanel.SetActive(true);
            _NPanel.SetActive(false);
            _text.text = "START";
            _soundFXPlayer.PlayOneShot(_countDownSoundFXs[_count]);

            OnStart?.Invoke();
        }
        else
            gameObject.SetActive(false);
    }
}
