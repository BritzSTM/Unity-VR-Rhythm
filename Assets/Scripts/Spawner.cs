using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _spawnRange = 2.0f;
    [SerializeField] private GameObject[] _notesPrefabs;

    [SerializeField] private int _beatsPerMinute = 120;
    public int BeatsPerMinute
    {
        get => _beatsPerMinute;
        set
        {
            _beatsPerMinute = value;
            UpdateBPM();
        }
    }

    [SerializeField] private int _baseRate = 2;

    private Transform _tr;
    private float _spawnTime;
    private float _accTime;

    private void Awake()
    {
        _tr = GetComponent<Transform>();
    }

    private void Start()
    {
        UpdateBPM();
    }

    private void Update()
    {
        if (_accTime > _spawnTime)
        {
            var spawnPos = (Random.insideUnitSphere * _spawnRange) + _tr.position;
            var spawnCubeType = Random.Range(0, _notesPrefabs.Length);
            var spwanRot = 90 * Random.Range(0, 4);

            GameObject cube = Instantiate(_notesPrefabs[spawnCubeType], spawnPos, _tr.rotation);
            cube.transform.Rotate(_tr.forward, spwanRot);

            _accTime = 0;
        }

        _accTime += Time.deltaTime;
    }

    private void UpdateBPM() => _spawnTime = (1.0f / (_beatsPerMinute / 60.0f)) * _baseRate;

    private void OnValidate()
    {
        UpdateBPM();
    }
}
