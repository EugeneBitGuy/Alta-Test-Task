using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class LevelUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Button restartButton;
    [SerializeField] private SettingsUI settingsUI;
    [SerializeField] private SizeUI sizeUI;
    [SerializeField] private LevelProgressBarUI levelProgressBarUI;
    [SerializeField] private string levelFormat = "Level {0}";
    [SerializeField] private float hideElementsTime;
    
    protected override void Init()
    {
        base.Init();
        
        restartButton.onClick.AddListener(OnRestartButtonClick);
        
        GameController.Instance.OnLevelLoaded += OnLevelLoaded;
        GameController.Instance.OnLevelEnd += OnLevelEnd;
        
    }

    private void OnLevelLoaded(int levelNumber)
    {
        restartButton.interactable = true;
        levelText.text = string.Format(levelFormat, SaveLoadController.Instance.Data.GameData.LevelNumber + 1);
        
        ShowElements();
    }

    private void OnLevelEnd(bool isWin)
    {
        HideElements();
    }

    private void HideElements()
    {
        levelText.transform.DOKill();
        levelText.transform.DOScale(Vector3.zero, hideElementsTime).SetEase(Ease.Linear);
        
        settingsUI.transform.DOKill();
        settingsUI.transform.DOScale(Vector3.zero, hideElementsTime).SetEase(Ease.Linear);
        
        sizeUI.transform.DOKill();
        sizeUI.transform.DOScale(Vector3.zero, hideElementsTime).SetEase(Ease.Linear);
        
        restartButton.transform.DOKill();
        restartButton.transform.DOScale(Vector3.zero, hideElementsTime).SetEase(Ease.Linear);
        
        levelProgressBarUI.transform.DOKill();
        levelProgressBarUI.transform.DOScale(Vector3.zero, hideElementsTime).SetEase(Ease.Linear);
        
    }

    private void ShowElements()
    {
        levelText.transform.localScale = Vector3.one;
        settingsUI.transform.localScale = Vector3.one;
        sizeUI.transform.localScale = Vector3.one;
        restartButton.transform.localScale = Vector3.one;
        levelProgressBarUI.transform.localScale = Vector3.one;
    }

    private void OnRestartButtonClick()
    {
        GameController.Instance.RestartLevel();
    }
}
