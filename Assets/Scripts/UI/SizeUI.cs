using UnityEngine;
using UnityEngine.UI;

public sealed class SizeUI : MonoBehaviour
{
    [SerializeField] private Image fillImage;

    private IPlayerSizeService _playerSizeService;
    
    private void Start()
    {
        GameController.Instance.OnLevelStarted += OnLevelStarted;
        GameController.Instance.OnLevelStartLoading += OnLevelStartLoading;
    }

    private void Update()
    {
        if(_playerSizeService == null) return;
        
        fillImage.fillAmount = _playerSizeService.GetSizeFullness();
    }

    private void OnLevelStarted()
    {
        _playerSizeService = ServicesContainer.Instance.Get<IPlayerSizeService>();
    }

    private void OnLevelStartLoading(int i)
    {
        _playerSizeService = null;
        fillImage.fillAmount = 1f;
    }
}
