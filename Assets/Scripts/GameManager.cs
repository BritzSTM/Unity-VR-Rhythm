using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Inst { get; private set; }
    public int ComboCount { get; private set; }
    public int MaxComboCount { get; private set; }
    public int TotalScore { get; private set; }

    [SerializeField] private int CoolScore = 100;
    [SerializeField] private int GoodScore = 50;

    [SerializeField] private TMP_Text _MaxComboText;
    [SerializeField] private TMP_Text _ScoreText;

    [SerializeField] private ReadyCount _readyCount;
    [SerializeField] private AudioSource _audioPlayer;
    [SerializeField] private Spawner _spawner;

    private bool _isPlaying;
    private void Awake()
    {
        if (Inst == null)
            Inst = this;
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
        _audioPlayer.Play();
        _isPlaying = true;
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
    }
}
