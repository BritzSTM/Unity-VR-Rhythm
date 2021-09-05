using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class AudioSourceDisplay : MonoBehaviour
{
    public const float AudibleFrequency = 22050;

    // 구간 [ [i - 1] , [i] ), i == 0 => [i - 1] = 0 
    public static readonly float[] FrequencyIntervals = { 20, 60, 250, 500, 2000, 4000, 6000, 22050 }; 

    [SerializeField] private int _sampleCount = 512;
    [SerializeField] private int _chCount = 2;
    [SerializeField] private int _spectrumCount = 8;
    [SerializeField] private FFTWindow _windowType = FFTWindow.Blackman;

    private AudioSource _audioSource;

    // 배열 [ch][sample]
    private float _frequencyUnit;
    private float[][] _samples;
    private float[][] _spectrums;
    private float[][] _spectrumsHighest;
    private float[][] _normalizedSpectrums;

    public event UnityAction OnUpdated;

    public float[] this[int ch] { get => _normalizedSpectrums[ch]; }
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        InitVars();
    }

    private void InitVars()
    {
        _samples = new float[_chCount][];

        _spectrums = new float[_chCount][];
        _spectrumsHighest = new float[_chCount][];
        _normalizedSpectrums = new float[_chCount][];

        for (int i = 0; i < _chCount; ++i)
        {
            _samples[i] = new float[_sampleCount];

            _spectrums[i] = new float[_spectrumCount];
            _spectrumsHighest[i] = new float[_spectrumCount];
            _normalizedSpectrums[i] = new float[_spectrumCount];
        }

        _frequencyUnit = AudibleFrequency / _sampleCount;
    }

    private void Update()
    {
        UpdateSpectrumData();
        UpdateSpectrums();
        NormalizeSpectrums();

        OnUpdated?.Invoke();
    }

    private void UpdateSpectrumData()
    {
        for (int i = 0; i < _chCount; ++i)
        {
            _audioSource.GetSpectrumData(_samples[i], i, _windowType);
        }
    }

    private void UpdateSpectrums()
    {
        // 512 => 2 4 8 16 32 64 128 256
        // 1024 =>
        // 스팩트럼이 늘어날때 맵핑은 어떻게?...
        int count = 0;
        for (int i = 0; i < _spectrumCount; ++i)
        {
            float avg = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            if (i == 7)
                sampleCount += 2;

            for (int j = 0; j < sampleCount; ++j)
            {
                avg += _samples[0][count] * (count + 1);
                ++count;
            }

            avg /= count;
            _spectrums[0][i] = avg * 10f;
        }
    }

    private void NormalizeSpectrums()
    {

    }
}
