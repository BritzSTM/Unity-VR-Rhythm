using UnityEngine;

[System.Serializable]
public struct TrackData
{
    public string TitleName;
    public Texture CoverImg;
    public AudioClip Audio;
    public int BeatsPerMinute;

    [TextArea(2, 5)]
    public string DetailComment;
}

[CreateAssetMenu(fileName = "new track desc so", menuName = "Game/Track")]
public class TrackDescSO : ScriptableObject
{
    [TextArea(2, 5)]
    public string Comment;
    public TrackData track;
}
