using System;
using System.Collections;
using UnityEngine.SceneManagement;

public sealed class LevelsController : MonoSingletone<LevelsController>
{
    private int _currentScene = -1;
    public int LevelsCount => SceneManager.sceneCountInBuildSettings - 1;

    public event Action<int> OnLevelLoaded;
    
    private void Start()
    {
        GameController.Instance.OnLevelStartLoading += LoadLevel;
    }
    
    private void LoadLevel(int levelNumber)
    {
        StartCoroutine(LoadLevelRoutine(AdjustLevelNumber(levelNumber)));
    }

    private IEnumerator LoadLevelRoutine(int number)
    {
        
        yield return UnloadScenes(1);
        _currentScene = number;
        yield return StartCoroutine(LoadSceneRoutine());

        yield return null;

        OnLevelLoaded?.Invoke(_currentScene);
    }
    
    private int AdjustLevelNumber(int levelNumber)
    {
        int levelIndex = levelNumber - 1;
        
        levelIndex %= LevelsCount;
        
        return levelIndex + 1;
    }
    
    private IEnumerator LoadSceneRoutine()
    {
        yield return SceneManager.LoadSceneAsync(_currentScene, LoadSceneMode.Additive);
      
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(1));
        
    }
    
    private IEnumerator UnloadScenes(int startFrom)
    {
        for (var i = startFrom; i < SceneManager.sceneCount; i++)
        {
            yield return SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i));
        }
    }
    
}