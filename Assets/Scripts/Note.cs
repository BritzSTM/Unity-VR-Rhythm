using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public float Speed = 3.0f;

    void Update()
    {
        transform.position += Time.deltaTime * transform.forward * Speed;
    }
}
