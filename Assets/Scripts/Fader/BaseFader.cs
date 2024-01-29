using System;
using UnityEngine;

public abstract class BaseFader : MonoBehaviour, IFader
{
    public abstract void Hide(Action callback);

    public abstract void HideImmediately();

    public abstract void Show(Action callback);

    public abstract void ShowImmediately();
}
