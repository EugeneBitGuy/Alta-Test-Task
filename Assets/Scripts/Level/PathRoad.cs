using UnityEngine;

public sealed class PathRoad : MonoBehaviour
{
    [SerializeField] private Transform roadModel;

    private PlayerSize _playerSize;
    
    public void Init(PlayerSize playerSize)
    {
        _playerSize = playerSize;
        _playerSize.OnSizeChanged += ChangeRoadSize;
        _playerSize.OnAnnihilation += DestroyRoad;
        
        ChangeRoadSize(playerSize.MaxSize);
    }

    private void ChangeRoadSize(float playerSize)
    {
        roadModel.localScale = new Vector3(playerSize, roadModel.localScale.y, roadModel.localScale.z);
    }

    private void DestroyRoad()
    {
        roadModel.gameObject.SetActive(false);
        
        _playerSize.OnSizeChanged -= ChangeRoadSize;
        _playerSize.OnAnnihilation -= DestroyRoad;
    }

}
