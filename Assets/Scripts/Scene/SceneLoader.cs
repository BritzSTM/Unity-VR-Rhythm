using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private bool _onStartScene;
    private Scene _currentScene;

    public void LoadScene(SceneLoadType[] sceneStack, bool unloadCurrentScene = true)
    {

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

        foreach (var s in sceneStack)
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
