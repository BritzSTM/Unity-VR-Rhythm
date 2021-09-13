using System;
using UnityEngine;
using UnityEngine.Video;

[Serializable]
public struct RoomEnvDesc
{
    public Material FloorCapMat;
    public Material DomeMat;
    public VideoClip RoomVideoClip;
}

[CreateAssetMenu(fileName = "new room env desc so file", menuName = "Game/Env/Room")]
public class RoomEnvDescSO : ScriptableObject
{
    [SerializeField] private bool _isAssignable = false;

    [TextArea(3, 5)]
    public string Comment;
    public RoomEnvDesc Desc;

    public void AssignFrom(RoomEnvDescSO descSO)
    {
        if (!_isAssignable)
        {
            Debug.LogWarning($"{this.name}is not assignable room desc so");
            return;
        }

        Comment = descSO.Comment;
        Desc = descSO.Desc;
    }

    public void AssignFrom(TrackCell cell) => AssignFrom(cell.TrackDescSO.RoomDescSO);
}
