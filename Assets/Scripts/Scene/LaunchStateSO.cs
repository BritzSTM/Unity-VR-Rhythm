using System;
using UnityEngine;

[CreateAssetMenu(fileName = "new launch state so", menuName = "Game/LaunchStateSO")]
public class LaunchStateSO : ScriptableObject
{
    [NonSerialized] public bool IsLaunched;
    [NonSerialized] public bool IsColdLaunched;

    public void Launch() => IsLaunched = true;
}
