using System;
using DG.Tweening;
using Lean.Pool;
using UnityEngine;
using UnityEngine.Serialization;

public class Obstacle : MonoBehaviour, IExplosionDestroyable
{
    [Header("Animation Settings")]
    [FormerlySerializedAs("renderer")] [SerializeField] private MeshRenderer meshRenderer;
    
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color infectionColor;

    [SerializeField] private float timeToBlow;
    [SerializeField] private AnimationCurve infectionEasing;

    [Header("Particles")]
    [SerializeField] private ParticleSystem blowUpEffectPrefab;
    
    public event Action<IExplosionDestroyable> OnBlowUp;
    
    private void Awake()
    {
        meshRenderer.material = new Material(meshRenderer.material);
        meshRenderer.material.color = defaultColor;
    }

    public void BlowUp()
    {
        meshRenderer.material.DOColor(infectionColor, timeToBlow).SetEase(infectionEasing).OnComplete(() =>
        {
            SpawnBlowUpEffect();
            
            OnBlowUp?.Invoke(this);
            OnBlowUp = null;
            
            gameObject.SetActive(false);
        });
    }
    
    protected virtual void SpawnBlowUpEffect()
    {
        if (blowUpEffectPrefab != null)
        {
            ParticleSystem effect = LeanPool.Spawn(blowUpEffectPrefab, transform.position, blowUpEffectPrefab.transform.rotation);
           
            LeanPool.Despawn(effect.gameObject, effect.main.duration);
        }
    }
}