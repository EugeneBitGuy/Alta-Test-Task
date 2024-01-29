using System;
using UnityEngine;

public sealed class Finish : MonoBehaviour
{
    [Header("Interactable layer")]
    [SerializeField] private LayerMask playerMask;
    [Header("Doors properties")]
    [SerializeField] private FinishDoors finishDoorsModel;
    [SerializeField] private CollisionTrigger finishDoorsTrigger;
    [Header("Finish trigger")]
    [SerializeField] private CollisionTrigger finishTrigger;
    [Header("Particles")]
    [SerializeField] private ParticleSystem winParticles;

    public event Action OnFinishEntered;
    public void Init()
    {
        finishDoorsTrigger.Init(playerMask);
        
        finishTrigger.Init(playerMask);

        finishDoorsTrigger.OnTriggered += finishDoorsModel.Open;
        finishTrigger.OnTriggered += OnFinish;
    }

    private void OnFinish()
    {
        OnFinishEntered?.Invoke();
        winParticles.gameObject.SetActive(true);
        winParticles.Play(true);
    }
}
