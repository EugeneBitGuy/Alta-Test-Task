using System;
using UnityEngine;

public sealed class CollisionTrigger : MonoBehaviour
{
    private LayerMask _interactableLayer;
    
    public event Action OnTriggered;

    public void Init(LayerMask layerMask)
    {
        _interactableLayer = layerMask;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_interactableLayer.Excludes(other.gameObject.layer)) return;
        
        OnTriggered?.Invoke();
        OnTriggered = null;
    }
}
