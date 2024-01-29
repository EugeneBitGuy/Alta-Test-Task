using System;
using System.Collections;
using Lean.Pool;
using UnityEngine;

public sealed class PlayerShooter : MonoBehaviour
{
    [Header("Projectile prefab")]
    [SerializeField] private BaseProjectile projectilePrefab;
    
    [Header("Shooting settings")]
    [SerializeField] private float sizeAbsorptionSpeed;
    [SerializeField] private float proportionCoefficient;

    [Header("Particles")]
    [SerializeField] private ParticleSystem projectileSpawnEffect;

    private PlayerSize _playerSize;
    private BaseProjectile _projectile;
    private IEnumerator _spawnProjectileCoroutine;

    private bool _isAllowedShooting;

    public event Action<BaseProjectile> OnProjectileLaunch;

    public void Init(PlayerSize playerSize)
    {
        _playerSize = playerSize;

        _isAllowedShooting = false;
    }

    private void OnEnable()
    {
        InputSystem.Instance.OnTouch += StartProjectileSpawn;
        InputSystem.Instance.OnRelease += LaunchProjectile;
    }

    private void OnDisable()
    {
        InputSystem.Instance.OnTouch -= StartProjectileSpawn;
        InputSystem.Instance.OnRelease -= LaunchProjectile;
        
        if(_spawnProjectileCoroutine != null)
            StopCoroutine(_spawnProjectileCoroutine);
        
        projectileSpawnEffect.gameObject.SetActive(false);
        
        if(_projectile != null)
            Destroy(_projectile.gameObject);
    }

    public void SetShootingAllowance(bool isAllowedShooting)
    {
        _isAllowedShooting = isAllowedShooting;
        if(isAllowedShooting)
            PopUpTextController.Instance.ShowText("Shoot to move!");
    }
    
    private void StartProjectileSpawn()
    {
        if(!_isAllowedShooting)
        {
            PopUpTextController.Instance.ShowText("Can't shoot right now");
            return;
        }
        
        _projectile = LeanPool.Spawn(projectilePrefab, transform.position, Quaternion.identity);

        _projectile.DetectCollision = false;

        _projectile.SetSize(_playerSize.MinimalSize * proportionCoefficient);

        projectileSpawnEffect.gameObject.SetActive(true);
        
        _spawnProjectileCoroutine = ProjectileSpawnCoroutine ();
        StartCoroutine(_spawnProjectileCoroutine);
    }

    private IEnumerator ProjectileSpawnCoroutine()
    {
        yield return null;
        while (true)
        {
            _playerSize.CurrentSize -= sizeAbsorptionSpeed * Time.deltaTime;

            _projectile.transform.position 
                = transform.position + Vector3.forward * (_playerSize.CurrentSize + _projectile.CurrentSize) / 2;
            
            _projectile.SetSize(_projectile.CurrentSize + sizeAbsorptionSpeed * proportionCoefficient * Time.deltaTime);
            
            yield return null;
        }
    }
    
    private void LaunchProjectile()
    {
        if(_spawnProjectileCoroutine == null) return;
        
        StopCoroutine(_spawnProjectileCoroutine);
        _projectile.DetectCollision = true;
        _projectile.Launch();
        OnProjectileLaunch?.Invoke(_projectile);
        projectileSpawnEffect.gameObject.SetActive(false);
        _spawnProjectileCoroutine = null;
        _projectile = null;
        
    }
}
