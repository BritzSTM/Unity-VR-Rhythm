using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailedArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "RedNote" && other.tag != "BlueNote")
            return;

        Destroy(other.gameObject);
        GameManager.Inst.AddFail();
    }
}
