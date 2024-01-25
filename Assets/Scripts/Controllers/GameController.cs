using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoSingletone<GameController>
{
    public event Action OnInitCompleted;
    public event Action OnShowFadeUI;
    public event Action<int> OnLevelStartLoading;
    public event Action<int> OnLevelLoaded;
    public event Action<bool> OnLevelEnd;
    public event Action OnLevelStarted;

    private void Start()
    {
        //LevelsController.Instance.OnLevelLoaded += InvokeOnLevelLoaded;
        Application.targetFrameRate = 60;
        LateStart();
    }

    private void LateStart()
    {
        /*this.DoAfterNextFrameCoroutine(() =>
        {
            OnInitCompleted?.Invoke();
            LoadLevel();
        });*/
    }
    

    public void InvokeOnStartLevelLoading()
    {
        /*var levelNumber = SLS.Data.Game.Level.Value + 1;
        OnLevelStartLoading?.Invoke(levelNumber);*/
    }

    private void InvokeOnLevelLoaded(int sceneId)
    {
        OnLevelLoaded?.Invoke(sceneId);
        //Taptic.Light();
    }

    public void LevelStart()
    {
        OnLevelStarted?.Invoke();
    }

    public void LevelEnd(bool playerWin)
    {
        if (playerWin)
        {
            //Taptic.Success();
        }
        else
        { 
            //Taptic.Failure();
        }

        OnLevelEnd?.Invoke(playerWin);
    }

    public void LoadLevel()
    {
        //SLS.Save();
        OnShowFadeUI?.Invoke();
    }

    public void NextLevel()
    {
        //SLS.Data.Game.Level.Value++;
        LoadLevel();
    }

    public void RestartLevel()
    {
        LoadLevel();
    }
}
