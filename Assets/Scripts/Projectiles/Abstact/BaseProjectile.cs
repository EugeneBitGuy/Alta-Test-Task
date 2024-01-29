using System;
using System.Collections;
using Lean.Pool;
using UnityEngine;

public abstract class BaseProjectile : MonoBehaviour
{
    [Header("Projectile mehanics")]
    [SerializeField] protected float launchForce;

    [Header("Particles")]
    [SerializeField] protected ParticleSystem hitEffect;

    [SerializeField] protected ParticleSystem trail;

    [SerializeField] protected ParticleSystem spawnEffect;

    public event Action<IExplosionDestroyable> OnExplode;

    protected Rigidbody _rigidbody;
    protected Collider _collider;
    protected float _currentSize;

    public float CurrentSize => _currentSize;

    public bool DetectCollision
    {
        get => !_rigidbody.isKinematic && _rigidbody.detectCollisions && !_collider.isTrigger;
        set
        {
            _rigidbody.isKinematic = !value;
            _rigidbody.detectCollisions = value;
            _collider.isTrigger = !value;
        }
    }

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    public virtual void Launch()
    {
        spawnEffect.gameObject.SetActive(false);
        trail.gameObject.SetActive(true);
        _rigidbody.AddForce(Vector3.forward * launchForce, ForceMode.Impulse);
    }

    public void SetSize(float size)
    {
        _currentSize = size;
        transform.localScale = size * Vector3.one;
    }

    protected abstract void Explode(float explosionRadius);

    protected virtual void Destroy()
    {
        spawnEffect.gameObject.SetActive(true);
        trail.gameObject.SetActive(false);
        DetectCollision = false;
    }

    protected virtual void SpawnHitEffect(Vector3 hitPoint)
    {
        if (hitEffect != null)
        {
            ParticleSystem effect = LeanPool.Spawn(hitEffect, hitPoint, hitEffect.transform.rotation);
            effect.transform.localScale = transform.localScale;
            LeanPool.Despawn(effect.gameObject, effect.main.duration);
        }
    }

    protected void InvokeOnExplode(IExplosionDestroyable explosionDestroyable)
    {
        OnExplode?.Invoke(explosionDestroyable);
        OnExplode = null;
    }
}