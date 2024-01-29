using System.Linq;
using UnityEngine;
using Lean.Pool;

public sealed class EnergyProjectile : BaseProjectile
{
    [Header("Projectile mechanics")]
    [SerializeField] private float explosionRadiusMultiplier;
    [Header("Interactable layers")]
    [SerializeField] private LayerMask explosionDestroyableLayer;
    [SerializeField] private LayerMask finishLayer;

    private void OnTriggerEnter(Collider other)
    {
        if(explosionDestroyableLayer.Excludes(other.gameObject.layer) && finishLayer.Excludes(other.gameObject.layer)) return;
        
        Explode(explosionRadiusMultiplier * transform.localScale.x);  
    }
    
    protected override void Explode(float explosionRadius)
    {
        SpawnHitEffect(transform.position);
        
        var objectsToBlowUp = Physics.OverlapSphere(transform.position, explosionRadius, explosionDestroyableLayer);

        InvokeOnExplode(objectsToBlowUp.FirstOrDefault(obj => obj.GetComponent<IExplosionDestroyable>() != null)?.GetComponent<IExplosionDestroyable>());
        
        if (objectsToBlowUp.Length <= 0) return;
        
        foreach (var objectToBlowUp in objectsToBlowUp)
        {
            var explosionDestroyable = objectToBlowUp.GetComponent<IExplosionDestroyable>();
                
            if(explosionDestroyable == null) continue;
            
            explosionDestroyable.BlowUp();
        }
        
        if(objectsToBlowUp.Length > 1)
            PopUpTextController.Instance.ShowPerfect();
        
        
        Destroy();
    }

    protected override void Destroy()
    {
        base.Destroy();
        
        LeanPool.Despawn(this);
    }
}
