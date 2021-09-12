using UnityEngine;

public class ObserverForSceneStart : MonoBehaviour
{
    [SerializeField] private SceneLoadEventSO _onStartEventSO;

    private void Awake()
    {
        Debug.Assert(_onStartEventSO != null);
    }

    private void Start()
    {
        _onStartEventSO.RaiseEvent(gameObject.scene);

        gameObject.SetActive(false);
    }
}
