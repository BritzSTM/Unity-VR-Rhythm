using System.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private const string _soundTrackPlayerName = "SoundTrackPlayer";

    public static GameManager Inst { get; private set; }
    public int ComboCount { get; private set; }
    public int MaxComboCount { get; private set; }
    public int TotalScore { get; private set; }

    [SerializeField] private TrackDescSO _selectedTrackSO;
    [SerializeField] private VoidEventSO _requireGameEndEventSO;

    [SerializeField] private int CoolScore = 100;
    [SerializeField] private int GoodScore = 50;

    [SerializeField] private TMP_Text _MaxComboText;
    [SerializeField] private TMP_Text _ScoreText;

    [SerializeField] private ReadyCount _readyCount;
    private AudioSource _audioPlayer;
    [SerializeField] private Spawner _spawner;

    [SerializeField] private float _autoTime = 5.0f;

    private bool _isPlaying;
    private void Awake()
    {
        if (Inst == null)
        {
            Inst = this;
            InitAudioSource();
        }
        else
            Destroy(this);
    }

    private void OnEnable()
    {
        _readyCount.OnStart += OnStart;
    }

    private void OnDisable()
    {
        _readyCount.OnStart -= OnStart;
        Inst = null;
    }

    public void AddCool()
    {
        IncCombo();
        TotalScore += CoolScore;

        UpdateScoreTexts();
    }

    public void AddGood()
    {
        IncCombo();
        TotalScore += GoodScore;

        UpdateScoreTexts();
    }

    public void AddFail()
    {
        ComboCount = 0;

        UpdateScoreTexts();
    }

    private void IncCombo()
    {
        ++ComboCount;
        if (MaxComboCount < ComboCount)
            MaxComboCount = ComboCount;
    }

    private void UpdateScoreTexts()
    {
        _MaxComboText.text = "MAX COMBO : " + MaxComboCount.ToString("D4");
        _ScoreText.text = "SCORE : " + TotalScore.ToString("D8");
    }

    private void OnStart()
    {
        _audioPlayer.clip = _selectedTrackSO.Track.Audio;
        _audioPlayer.Play();

        _isPlaying = true;
        _spawner.BeatsPerMinute = _selectedTrackSO.Track.BeatsPerMinute;
    }

    private void Update()
    {
        if (!_isPlaying)
            return;

        if (_audioPlayer.isPlaying)
        {
            // 4초 먼저 스폰을 종료하기 위함
            if(!(_audioPlayer.clip.length - 4.0f < _audioPlayer.time))
                return;
        }

        _isPlaying = false;
        _spawner.enabled = false;
        _requireGameEndEventSO.RaiseEvent();
        //StartCoroutine(RestartAuto());
    }

    private IEnumerator RestartAuto()
    {
        yield return new WaitForSeconds(_autoTime);

        ComboCount = 0;
        MaxComboCount = 0;
        TotalScore = 0;
        UpdateScoreTexts();

        _spawner.enabled = true;
        _readyCount.gameObject.SetActive(true);
    }

    private void InitAudioSource()
    {
        Scene s = SceneManager.GetSceneByName("Core");
        if (!s.isLoaded)
            return;

        var obj = s.GetRootGameObjects().First(x => x.name == _soundTrackPlayerName);

        if (obj != null)
        {
            _audioPlayer = obj.GetComponent<AudioSource>();
        }
    }

    public void Stop()
    {
        _isPlaying = false;
        _audioPlayer.Stop();
        _requireGameEndEventSO.RaiseEvent();
    }
}
