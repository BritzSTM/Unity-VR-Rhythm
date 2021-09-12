using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// Clean up boot 전략을 구현한 런처
/// </summary>
public class SceneColdLauncher : MonoBehaviour
{
    [SerializeField] private LaunchStateSO _launchStateSO;
    [SerializeField] private SceneLoadEventSO _onStartSceneSO;

    [SerializeField] private List<SceneLoadType> _sceneStack;

    private bool _onStartScene;

    private void Awake()
    {
        Debug.Assert(_launchStateSO != null);

        if (!_launchStateSO.IsColdLaunched)
        {
            SaveAwakeSceneTypeInStack();
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        if (_launchStateSO.IsColdLaunched)
            return;

        _launchStateSO.IsLaunched = true;
        _launchStateSO.IsColdLaunched = true;
        _onStartSceneSO.OnEvent += OnStartScene;

        StartCoroutine(LoadSceneStack());
    }

    private void OnDisable()
    {
        _onStartSceneSO.OnEvent -= OnStartScene;
    }

    private void SaveAwakeSceneTypeInStack()
    {
        var awakeSceneType = new SceneLoadType();
        awakeSceneType.Scene = gameObject.scene.name;
        awakeSceneType.LoadMode = LoadSceneMode.Additive;

        _sceneStack.Add(awakeSceneType);
    }

    private void LoadSceneFrom(SceneLoadType desc)
    {
        var sceneObj = SceneManager.GetSceneByName(desc.Scene);
        if (sceneObj.isLoaded)
        {
            Debug.LogWarning($"Scene[{desc.Scene}] is loaded already.");
            return;
        }

        Debug.Log($"Scene[{desc.Scene}] is cold launch.");

        _onStartScene = false;
        SceneManager.LoadScene(desc.Scene, desc.LoadMode);
    }

    private IEnumerator LoadSceneStack()
    {
        yield return null;
        SceneManager.sceneLoaded += OnSceneLoaded;

        foreach (var s in _sceneStack)
        {
            LoadSceneFrom(s);
            yield return new WaitUntil(() => _onStartScene);
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;
        Destroy(this.gameObject);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.SetActiveScene(scene);
    }

    private void OnStartScene(Scene s) => _onStartScene = true;
}
