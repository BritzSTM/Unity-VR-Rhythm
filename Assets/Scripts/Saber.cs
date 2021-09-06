using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saber : MonoBehaviour
{
    public Vector3 MoveDirection { get; private set; }
    [SerializeField] private ParticleSystem _hitVisualEffect;

    private Transform _tr;
    private Transform _hitVisualTr;
    private Vector3 _prevPos;

    private void Awake()
    {
        _tr = GetComponent<Transform>();
        _hitVisualTr = _hitVisualEffect.GetComponent<Transform>();
    }

    private void Update()
    {
        MoveDirection = (_tr.position - _prevPos).normalized;
        _prevPos = _tr.position;
    }

    public void PlayHitEffect(Vector3 hitPos)
    {
        _hitVisualTr.position = hitPos;
        _hitVisualTr.localRotation = Quaternion.LookRotation(-MoveDirection);

        _hitVisualEffect.Play();
    }
}
