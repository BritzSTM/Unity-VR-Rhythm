using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    private void Awake()
    {
        if (Inst == null)
            Inst = this;
        else
            Destroy(this);
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
}
