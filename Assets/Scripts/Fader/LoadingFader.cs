using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class LoadingFader : BaseFader
{
    [Header("Animation settings")]
    [SerializeField] private float fadeTime;
    [SerializeField] private Ease fadeEasing;

    [Header("Fader Image")]
    [SerializeField] private Image faderImage;

    [Header("Fader text")]
    [SerializeField] private CanvasGroup loadingTextCanvas;
    [SerializeField] private TextMeshProUGUI loadingText;
    [SerializeField] private string loadingTextStr = "Loading";
    [SerializeField] private float loadingDotsSpeed;

    public override void Show(Action callback)
    {
        loadingTextCanvas.DOFade(1f, fadeTime).SetEase(fadeEasing);
        faderImage
            .DOFade(1f, fadeTime)
            .SetEase(fadeEasing)
            .OnComplete(() => callback?.Invoke())
            .OnUpdate(LoadingText);
    }

    public override void Hide(Action callback)
    {
        loadingTextCanvas.DOFade(0, fadeTime);
        faderImage.DOFade(0f, fadeTime)
            .SetEase(fadeEasing)
            .OnComplete(() => callback?.Invoke());
    }

    public override void ShowImmediately()
    {
        gameObject.SetActive(true);

        var fullAlphaColor = faderImage.color;
        fullAlphaColor.a = 1;
        faderImage.color = fullAlphaColor;
        
        loadingTextCanvas.alpha = 1;
        loadingText.text = string.Empty;
    }

    public override void HideImmediately()
    {
        var zeroAlphaColor = faderImage.color;
        zeroAlphaColor.a = 0;
        faderImage.color = zeroAlphaColor;
        
        loadingTextCanvas.alpha = 0;
        loadingText.text = string.Empty;
    }
    
    private void LoadingText()
    {
        loadingText.text = loadingTextStr;
        for (int i = 0; i < Mathf.Floor((Time.time * loadingDotsSpeed) % 4); i++)
            loadingText.text += ".";
    }
}
