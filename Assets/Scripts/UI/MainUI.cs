using UnityEngine;

public sealed class MainUI : MonoBehaviour
{
    [SerializeField] private GameObject uiHolder;
    [SerializeField] private WinUI winUI;
    [SerializeField] private LoseUI loseUI;
    [SerializeField] private LevelUI levelUI;
    
    private void Start()
    {
        GameController.Instance.OnInitCompleted += Init;
    }

    private void Init()
    {
        GameController.Instance.OnLevelEnd += OnLevelEnd;
    }

    private void OnLevelEnd(bool playerWon)
    {
        if (playerWon)
        {
            winUI.Show();
        }
        else
        {
            loseUI.Show();
        }
    }
}
