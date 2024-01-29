using System;
using System.Collections;
using UnityEngine;

public sealed class GameController : MonoSingletone<GameController>
{
    public event Action OnInitCompleted;
    public event Action OnShowFadeUI;
    public event Action<int> OnLevelStartLoading;
    public event Action<int> OnLevelLoaded;
    public event Action<bool> OnLevelEnd;
    public event Action OnLevelStarted;

    private void Start()
    {
        ServicesContainer.Init();
        LevelsController.Instance.OnLevelLoaded += InvokeOnLevelLoaded;
        Application.targetFrameRate = 60;
        StartCoroutine(LateStart());
    }

    private IEnumerator LateStart()
    {
        yield return null;
        
        OnInitCompleted?.Invoke();
        LoadLevel();
    }

    public void InvokeOnStartLevelLoading()
    {
        var levelNumber = SaveLoadController.Instance.Data.GameData.LevelNumber + 1;
        OnLevelStartLoading?.Invoke(levelNumber);
        InputSystem.Instance.OnTouch += LevelStart;
    }

    public void LevelStart()
    {
        OnLevelStarted?.Invoke();
        InputSystem.Instance.OnTouch -= LevelStart;
    }

    public void LevelEnd(bool playerWin)
    {
        OnLevelEnd?.Invoke(playerWin);
    }

    public void LoadLevel()
    {
        SaveLoadController.Instance.Save();
        OnShowFadeUI?.Invoke();
    }

    public void NextLevel()
    {
        SaveLoadController.Instance.Data.GameData.LevelNumber++;
        LoadLevel();
    }

    public void RestartLevel()
    {
        LoadLevel();
    }

    private void InvokeOnLevelLoaded(int sceneId)
    {
        OnLevelLoaded?.Invoke(sceneId);
    }
}
