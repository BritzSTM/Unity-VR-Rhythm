using UnityEngine;
using TMPro;

public class ComboText : MonoBehaviour
{
    [SerializeField] private float _lifeCycleTime = 3.0f;
    private TMP_Text _text;
    private Rigidbody _rb;
    private float _accTime;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_accTime >= _lifeCycleTime)
            Destroy(gameObject);

        _accTime += Time.deltaTime;
    }

    public void SetText(string s) => _text.text = s;
}
