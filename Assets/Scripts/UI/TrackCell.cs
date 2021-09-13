using PolyAndCode.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrackCell : MonoBehaviour, ICell
{
    [SerializeField] private TMP_Text _orderText;
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private Image _thumbnailImg;
    [SerializeField] private TMP_Text _bpmText;

    private TrackDescSO _trackDescSO;
    public TrackDescSO TrackDescSO { get => _trackDescSO; }

    private int _cellIndex;

    public void ConfigureCell(TrackDescSO trackDescSO, int cellIndex)
    {
        _trackDescSO = trackDescSO;
        _cellIndex = cellIndex;

        _orderText.text = cellIndex.ToString("D3");
        _titleText.text = _trackDescSO.Track.TitleName;
        _thumbnailImg.sprite = _trackDescSO.Track.CoverImg;
        _bpmText.text = "BPM :" + _trackDescSO.Track.BeatsPerMinute.ToString("D3");
    }
}
