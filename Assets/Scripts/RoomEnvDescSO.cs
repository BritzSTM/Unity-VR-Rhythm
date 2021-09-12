using System;
using UnityEngine;
using UnityEngine.Video;

[Serializable]
public struct RoomEnvDesc
{
    public Material FloorCapMat;
    public VideoClip RoomVideoClip;
}

[CreateAssetMenu(fileName = "new room env desc so file", menuName = "Game/Env/Room")]
public class RoomEnvDescSO : ScriptableObject
{
    [TextArea(3, 5)]
    public string Comment;
    public RoomEnvDesc Desc;
}
