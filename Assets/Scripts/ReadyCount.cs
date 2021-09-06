using TMPro;
using UnityEngine;

public class ReadyCount : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private GameObject _APanel;
    [SerializeField] private GameObject _NPanel;

    [SerializeField] private AudioSource _soundFXPlayer;
    [SerializeField] private AudioClip[] _countDownSoundFXs;

    private Animator _animator;
    private int _count = 4;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
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
        }
        else
            gameObject.SetActive(false);
    }
}
