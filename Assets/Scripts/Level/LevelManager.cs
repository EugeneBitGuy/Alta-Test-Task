using UnityEngine;

public sealed class LevelManager : MonoBehaviour, ILevelProgressService
{
    private Player _player;
    private Finish _finish;
    private PathRoad _pathRoad;
    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _finish = FindObjectOfType<Finish>();
        _pathRoad = FindObjectOfType<PathRoad>();
    }

    private void OnEnable()
    {
        GameController.Instance.OnLevelLoaded += Init;
        GameController.Instance.OnLevelStarted += OnLevelStarted;
    }

    private void OnDisable()
    {
        GameController.Instance.OnLevelLoaded -= Init;
        GameController.Instance.OnLevelStarted -= OnLevelStarted;
    }

    private void Init(int i)
    {
        _player.Init();
        _finish.Init();
        _pathRoad.Init(_player.PlayerSize);

        _player.PlayerSize.OnAnnihilation += Loose;
        _finish.OnFinishEntered += Win;
        
        ServicesContainer.Instance.Register<ILevelProgressService>(this);
    }

    public float GetLevelProgress()
    {
        return _player.transform.position.z / _finish.transform.position.z;
    }

    private void OnLevelStarted()
    {
        _player.PlayerObstaclesDetector.OnDetectObstacle += SetShootingState;
        _player.PlayerShooter.OnProjectileLaunch += SetProjectileLaunchState;
        
        _player.PlayerShooter.SetShootingAllowance(true);
    }

    private void SetShootingState()
    {
        _player.PlayerMover.SetMovementAllowance(false);
        _player.PlayerShooter.SetShootingAllowance(true);
    }

    private void SetProjectileLaunchState(BaseProjectile projectile)
    {
        _player.PlayerMover.SetMovementAllowance(false);
        _player.PlayerShooter.SetShootingAllowance(false);


        projectile.OnExplode += OnProjectileExplode;
    }

    private void OnProjectileExplode(IExplosionDestroyable obstacle)
    {
        if (obstacle == null)
        {
            DecideToMoveOrShoot();
        }
        else
        {
            obstacle.OnBlowUp += OnObstacleBlowUp;
        }
    }

    private void OnObstacleBlowUp(IExplosionDestroyable obstacle)
    {
        DecideToMoveOrShoot();
    }

    private void DecideToMoveOrShoot()
    {
        if (_player.PlayerObstaclesDetector.HasDetectedObstacles)
        {
            SetShootingState();
        }
        else
        {
            SetSearchNearestObstaclesState();
        }
    }

    private void SetSearchNearestObstaclesState()
    {
        _player.PlayerMover.SetMovementAllowance(true);
        _player.PlayerShooter.SetShootingAllowance(false); 
    }

    private void Win()
    {
        SetEndLevelState(true);
    }

    private void Loose()
    {
        SetEndLevelState(false);
    }

    private void SetEndLevelState(bool isWin)
    {
        ServicesContainer.Instance.Unregister<ILevelProgressService>();

        _player.PlayerMover.SetMovementAllowance(false);
        _player.PlayerShooter.SetShootingAllowance(false);
        
        GameController.Instance.LevelEnd(isWin);
        
    }
}
