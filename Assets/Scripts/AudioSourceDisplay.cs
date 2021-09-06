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
    public int SpectrumCount { get => _spectrumCount; }

    [SerializeField] private FFTWindow _windowType = FFTWindow.Blackman;

    private AudioSource _audioSource;

    // 배열 [ch][sample]
    private float _frequencyUnit;
    private float[][] _samples;
    private float[][] _spectrums;
    private float[][] _spectrumsHighest;
    private float[][] _normalizedSpectrums;

    public event UnityAction OnUpdated;

    public float[] this[int ch] { get => _spectrums[ch]; }
    
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
        for (int ch = 0; ch < _chCount; ++ch)
        {
            int count = 0;
            for (int i = 0; i < _spectrumCount; ++i)
            {
                float avg = 0;
                int sampleCount = (int)Mathf.Pow(2, i) * 2;

                if (i == 7)
                    sampleCount += 2;

                for (int j = 0; j < sampleCount; ++j)
                {
                    avg += _samples[ch][count] * (count + 1);
                    ++count;
                }

                avg /= count;
                _spectrums[ch][i] = avg * 10f;
            }
        }
    }
}
