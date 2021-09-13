using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] protected LaunchStateSO _launchStateSO;
    [SerializeField] private SceneLoadEventSO _onStartSceneSO;

    [Tooltip("load from scene stack")]
    [SerializeField] private bool _autoLoadWhenStartScene = false;
    [SerializeField] private bool _autoLoadIgnoreWhenColdStart = true;
    [SerializeField] private bool _unloadCurrentScene = true;
    [SerializeField] private List<SceneLoadType> _sceneStack;
    public List<SceneLoadType> SceneStack { get => _sceneStack; }

    [Header("raise event")]
    [SerializeField] private UnityEvent _beginEvent;
    [SerializeField] private UnityEvent _endEvent;

    private bool _onStartScene;
    protected virtual void OnEnable()
    {
        _onStartSceneSO.OnEvent += OnStartScene;

        if (_autoLoadWhenStartScene)
            _onStartSceneSO.OnEvent += OnAutoLoadWhenStartScene;
    }

    protected virtual void OnDisable()
    {
        _onStartSceneSO.OnEvent -= OnStartScene;

        if (_autoLoadWhenStartScene)
            _onStartSceneSO.OnEvent -= OnAutoLoadWhenStartScene;
    }

    public void LoadFromSceneStack()
    {
        LoadScene(gameObject.scene, SceneStack.ToArray(), _unloadCurrentScene);
    }

    public void LoadScene(Scene fromScene, SceneLoadType[] sceneStack, bool unloadCurrentScene = true)
    {
        if (sceneStack == null)
        {
            Debug.LogWarning($"S[{nameof(SceneLoader)}] load scene sceneStack is null.");
            return;
        }

        if (unloadCurrentScene)
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.UnloadSceneAsync(fromScene);
        }

        StartCoroutine(LoadSceneStack(sceneStack));
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

    protected IEnumerator LoadSceneStack(SceneLoadType[] sceneStack)
    {
        yield return null;
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (_beginEvent != null)
            _beginEvent.Invoke();

        foreach (var s in sceneStack)
        {
            LoadSceneFrom(s);
            yield return new WaitUntil(() => _onStartScene);
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;

        if (_endEvent != null)
            _endEvent.Invoke();

        Destroy(gameObject);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.SetActiveScene(scene);

#if UNITY_EDITOR
       var ob = FindObjectOfType<ObserverForSceneStart>();

        if (ob == null)
            Debug.LogError("Not Found Observer in scene");
#endif
    }

    private void OnStartScene(Scene s) =>_onStartScene = true;
    private void OnAutoLoadWhenStartScene(Scene s)
    {
        if (_autoLoadIgnoreWhenColdStart && _launchStateSO.IsColdLaunched)
            return;

        LoadFromSceneStack();
    }
}
