using UnityEngine;

public sealed class Player : MonoBehaviour
{
    [SerializeField] private PlayerSize playerSize;
    [SerializeField] private PlayerShooter playerShooter;
    [SerializeField] private PlayerMover playerMover;
    [SerializeField] private PlayerObstaclesDetector playerObstaclesDetector;
    
    
    public PlayerSize PlayerSize => playerSize;
    public PlayerShooter PlayerShooter => playerShooter;
    public PlayerMover PlayerMover => playerMover;
    public PlayerObstaclesDetector PlayerObstaclesDetector => playerObstaclesDetector;

    public void Init()
    {
        playerSize.Init();
        playerShooter.Init(playerSize);
        playerMover.Init();

        playerSize.OnAnnihilation += OnAnnihilation;
        
    }

    private void OnAnnihilation()
    {
        playerSize.enabled = false;
        playerShooter.enabled = false;
        playerMover.enabled = false;
        playerObstaclesDetector.enabled = false;
        
        playerSize.OnAnnihilation -= OnAnnihilation;
    }
}
