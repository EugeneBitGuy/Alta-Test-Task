using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerObstaclesDetector : MonoBehaviour
{
    [Header("Interactable layer")]
    [SerializeField] private LayerMask obstaclesLayer;

    private readonly List<IExplosionDestroyable> _detectedObstacles = new List<IExplosionDestroyable>();

    public bool HasDetectedObstacles => _detectedObstacles.Count > 0;
    
    public event Action OnDetectObstacle;
    
    private void OnTriggerEnter(Collider other)
    {
        if(obstaclesLayer.Excludes(other.gameObject.layer)) 
            return;
        
        OnDetectObstacle?.Invoke();

        var detectedObstacle = other.GetComponent<IExplosionDestroyable>();
        detectedObstacle.OnBlowUp += OnObstacleBlowUp;
        _detectedObstacles.Add(detectedObstacle);
    }
    
    private void OnTriggerExit(Collider other)
    {
        if(obstaclesLayer.Excludes(other.gameObject.layer)) 
            return;
        

        var detectedObstacle = other.GetComponent<IExplosionDestroyable>();
        _detectedObstacles.Remove(detectedObstacle);
        
    }
    
    private void OnObstacleBlowUp(IExplosionDestroyable obstacle)
    {
        _detectedObstacles.Remove(obstacle);
    }
}
