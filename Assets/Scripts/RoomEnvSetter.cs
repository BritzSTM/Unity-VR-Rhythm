using UnityEngine;
using UnityEngine.Video;

public class RoomEnvSetter : MonoBehaviour
{
    [SerializeField] private RoomEnvDescSO _selectedRoomEnvSO;
    [SerializeField] private Renderer[] _floorCapRenderers;
    [SerializeField] private VideoPlayer _videoPlayer;

    private void Awake()
    {
        Debug.Assert(_selectedRoomEnvSO != null);

        Setup();
    }

    public void Setup()
    {
        foreach (var r in _floorCapRenderers)
        {
            r.material = _selectedRoomEnvSO.Desc.FloorCapMat;
        }

        RenderSettings.skybox = _selectedRoomEnvSO.Desc.DomeMat;
        _videoPlayer.clip = _selectedRoomEnvSO.Desc.RoomVideoClip;
        _videoPlayer.Play();
    }
}
