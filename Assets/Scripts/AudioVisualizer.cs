using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioVisualizer : MonoBehaviour
{
    private const string _soundTrackPlayerName = "SoundTrackPlayer";

    [SerializeField] private GameObject[] _bars;
    private AudioSourceDisplay _audioDisplay;
    [SerializeField] private int _ch = 0;
    [SerializeField] private float _barHeight = 3.0f;

    private Vector3 _cachedVec3 = new Vector3();
    private void Awake()
    {
        // 연결채널 구성에 대한 것을 생각해야 하므로 지금은 간단히 장면 탐색을 통해 획득함.
        Scene s = SceneManager.GetSceneByName("Core");
        if (!s.isLoaded)
            return;

        var obj = s.GetRootGameObjects().First(x => x.name == _soundTrackPlayerName);

        if (obj != null)
            _audioDisplay = obj.GetComponent<AudioSourceDisplay>();
    }

    private void OnEnable()
    {
        if(_audioDisplay != null)
            _audioDisplay.OnUpdated += OnUpdateBars;
    }

    private void OnDisable()
    {
        if (_audioDisplay != null)
            _audioDisplay.OnUpdated -= OnUpdateBars;
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
