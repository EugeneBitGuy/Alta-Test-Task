using UnityEngine;
using UnityEngine.UI;

public sealed class LevelProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image fillImage;

    private ILevelProgressService _levelProgressService;
    
    private void Start()
    {
        GameController.Instance.OnLevelStarted += OnLevelStarted;
        GameController.Instance.OnLevelStartLoading += OnLevelStartLoading;
    }

    private void Update()
    {
        if(_levelProgressService == null) return;
        
        fillImage.fillAmount = _levelProgressService.GetLevelProgress();
    }

    private void OnLevelStarted()
    {
        _levelProgressService = ServicesContainer.Instance.Get<ILevelProgressService>();
    }
    
    private void OnLevelStartLoading(int i)
    {
        _levelProgressService = null;
        fillImage.fillAmount = 0f;
    }
}
