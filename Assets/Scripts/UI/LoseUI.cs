using UnityEngine;
using UnityEngine.UI;

public sealed class LoseUI : BaseUI
{
    [SerializeField] private Button restartButton;

    protected override void Init()
    {
        base.Init();
        restartButton.onClick.AddListener(OnRestartButtonClicked);
    }

    public override void Show()
    {
        base.Show();
        restartButton.interactable = true;
    }

    public override void Hide()
    {
        base.Hide();
        restartButton.interactable = false;
    }
        
    private void OnRestartButtonClicked()
    {
        Hide();
        GameController.Instance.RestartLevel();
    }
}
