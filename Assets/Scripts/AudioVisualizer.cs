using UnityEngine;

public class AudioVisualizer : MonoBehaviour
{
    [SerializeField] private GameObject[] _bars;
    [SerializeField] private AudioSourceDisplay _audioDisplay;
    [SerializeField] private int _ch = 0;
    [SerializeField] private float _barHeight = 3.0f;

    private Vector3 _cachedVec3 = new Vector3();
    private void Awake()
    {
        _audioDisplay.OnUpdated += OnUpdateBars;
    }

    private void OnUpdateBars()
    {
        for(int i = 0; i < _bars.Length; ++i)
        {
            var barTr = _bars[i].transform;

            _cachedVec3 = barTr.localScale;
            _cachedVec3.y = _audioDisplay[_ch][i] * _barHeight;
            barTr.localScale = _cachedVec3;
        }
    }
}
