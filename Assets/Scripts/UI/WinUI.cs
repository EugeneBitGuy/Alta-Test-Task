using UnityEngine;
using UnityEngine.UI;

public sealed class WinUI : BaseUI
{
    [SerializeField] private Button continueButton;

    protected override void Init()
    {
        base.Init();
        continueButton.onClick.AddListener(OnContinueButtonClicked);
    }

    public override void Show()
    {
        base.Show();
        continueButton.interactable = true;
    }

    public override void Hide()
    {
        base.Hide();
        continueButton.interactable = false;
    }
        
    private void OnContinueButtonClicked()
    {
        Hide();
        GameController.Instance.NextLevel();
    }
}
