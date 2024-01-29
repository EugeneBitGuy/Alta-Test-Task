using System;
using UnityEngine;
using UnityEngine.EventSystems;

public sealed class InputSystem : MonoSingletone<InputSystem>, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private bool enableMultiTouch;

    private Camera _mainCamera;
    
    public event Action OnTouch;
    public event Action OnRelease;

    protected override void Awake()
    {
        base.Awake();
        _mainCamera = Camera.main;
    }

    private void Start()
    {
        Input.multiTouchEnabled = enableMultiTouch;
    }
    

    public void OnPointerDown(PointerEventData eventData)
    {
        if(_mainCamera != null)
            OnTouch?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(_mainCamera != null)
            OnRelease?.Invoke();
    }
}
