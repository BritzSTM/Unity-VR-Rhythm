using UnityEngine;

[System.Serializable]
public struct TrackData
{
    public string TitleName;
    public Sprite CoverImg;
    public AudioClip Audio;
    public int BeatsPerMinute;

    [TextArea(2, 5)]
    public string DetailComment;
}

[CreateAssetMenu(fileName = "new track desc so", menuName = "Game/Track/Track")]
public class TrackDescSO : ScriptableObject
{
    [SerializeField] private bool _isAssignable = false;

    [TextArea(2, 5)]
    public string Comment;
    public TrackData Track;
    public RoomEnvDescSO RoomDescSO;

    public void AssignFrom(TrackDescSO descSO)
    {
        if (!_isAssignable)
        {
            Debug.LogWarning($"{this.name}is not assignable room desc so");
            return;
        }

        Track = descSO.Track;
        RoomDescSO = descSO.RoomDescSO;
    }

    public void AssignFrom(TrackCell trackCell) => AssignFrom(trackCell.TrackDescSO);
}
