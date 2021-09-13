using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Clean up boot 전략을 구현한 런처
/// </summary>
public class SceneColdLauncher : SceneLoader
{
    [SerializeField] private LaunchStateSO _launchStateSO;

    private Scene _awakeScene;
    private SceneLoadType _awakeSceneType;
    private void Awake()
    {
        Debug.Assert(_launchStateSO != null);

        if (!_launchStateSO.IsColdLaunched)
            SaveAwakeSceneTypeAndSceneStack();
        else
            Destroy(gameObject);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        if (_launchStateSO.IsColdLaunched)
            return;

        _launchStateSO.IsLaunched = true;
        _launchStateSO.IsColdLaunched = true;

        LoadScene(_awakeScene, SceneStack.ToArray());
    }

    private void SaveAwakeSceneTypeAndSceneStack()
    {
        _awakeScene = gameObject.scene;

        _awakeSceneType = new SceneLoadType();
        _awakeSceneType.Scene = _awakeScene.name;
        _awakeSceneType.LoadMode = LoadSceneMode.Additive;

        SceneStack.Add(_awakeSceneType);
    }
}
