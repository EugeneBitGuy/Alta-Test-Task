using System;
using UnityEngine;
using Lean.Pool;

public sealed class PlayerSize : MonoBehaviour, IPlayerSizeService
{
    [Header("Max/Min Health of player")]
    [SerializeField] private float maxSize;
    [SerializeField] private float minimalSize;
    [Header("Particles")]
    [SerializeField] private ParticleSystem annihilationEffectPrefab;
    
    private float _currentSize;
    private bool _isCriticalSize;
    public float MinimalSize => minimalSize;
    public float MaxSize => maxSize;
    public float CurrentSize
    {
        get => _currentSize;

        set
        {
            if(_isCriticalSize) return;

            _currentSize = value;

            transform.localScale = Vector3.one * _currentSize;

            var positionWithNewHeight = transform.position;

            positionWithNewHeight.y = _currentSize / 2;

            transform.position = positionWithNewHeight;
            
            if (_currentSize <= minimalSize && !_isCriticalSize) Annihilate();
            
            OnSizeChanged?.Invoke(_currentSize);
        }
    }

    public event Action<float> OnSizeChanged;

    public event Action OnAnnihilation;

    public void Init()
    {
        _isCriticalSize = false;
        CurrentSize = maxSize;
        transform.localScale = Vector3.one * maxSize;
        ServicesContainer.Instance.Register<IPlayerSizeService>(this);
    }

    private void Annihilate()
    {
        transform.localScale = Vector3.zero;
        _isCriticalSize = true;
        SpawnAnnihilationParticles();
        OnAnnihilation?.Invoke();
        OnAnnihilation = null;
        ServicesContainer.Instance.Unregister<IPlayerSizeService>();
    }

    public float GetSizeFullness()
    {
        return (_currentSize - minimalSize) / (maxSize - minimalSize);
    }

    private void SpawnAnnihilationParticles()
    {
        if (annihilationEffectPrefab != null)
        {
            ParticleSystem effect = LeanPool.Spawn(annihilationEffectPrefab, transform.position, annihilationEffectPrefab.transform.rotation);
            LeanPool.Despawn(effect.gameObject, effect.main.duration);
        }   
    }
}
