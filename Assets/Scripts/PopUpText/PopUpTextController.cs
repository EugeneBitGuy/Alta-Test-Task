using DG.Tweening;
using TMPro;
using UnityEngine;

public sealed class PopUpTextController : MonoSingletone<PopUpTextController>
{
    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI popUpText;
    [SerializeField] private TextMeshProUGUI perfectText;
    [Header("Animation settings")]
    [SerializeField] private float moveDuration = 0.25f;
    [SerializeField] private Vector3 moveOffset = Vector3.up;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float fadeDelay = 0.5f;

    [Header("Perfect text settings")]
    [SerializeField] private string[] perfectPhrases;
    [SerializeField] private Color[] perfectColors;
    
    private Sequence _sequenceDefault;
    private Sequence _sequencePerfect;
    private Vector3 _defaultPosition = Vector3.down * 600f;

    private void Start()
    {
        popUpText.alpha = 0f;
        perfectText.alpha = 0f;
    }

    public void ShowText(string text)
    {
        popUpText.text = text;
        popUpText.color = Color.white;
        if (_sequenceDefault != null)
        {
            _sequenceDefault.Kill();
        }
        _sequenceDefault = DOTween.Sequence();
        _sequenceDefault.Join(popUpText.transform.DOLocalMove(_defaultPosition + moveOffset, moveDuration).From(_defaultPosition).SetEase(Ease.OutCubic)).SetUpdate(true);
        _sequenceDefault.Join(popUpText.DOFade(0f, fadeDuration).From(1f).SetEase(Ease.OutCubic).SetDelay(fadeDelay)).SetUpdate(true);
    }

    public void ShowPerfect()
    {
        perfectText.text = perfectPhrases[Random.Range(0, perfectPhrases.Length)];
        perfectText.color = perfectColors[Random.Range(0, perfectColors.Length)];
        
        if (_sequencePerfect != null)
        {
            _sequencePerfect.Kill();
        }
        _sequencePerfect = DOTween.Sequence();
        _sequencePerfect.Join(perfectText.transform.DOLocalMove(moveOffset, moveDuration).From(Vector3.zero).SetEase(Ease.OutCubic)).SetUpdate(true);
        _sequencePerfect.Join(perfectText.transform.DOPunchRotation(Vector3.one, moveDuration).SetEase(Ease.OutCubic)).SetUpdate(true);
        _sequencePerfect.Join(perfectText.transform.DOPunchScale(Vector3.one, moveDuration).SetEase(Ease.OutCubic)).SetUpdate(true);
        _sequencePerfect.Join(perfectText.DOFade(0f, fadeDuration).From(1f).SetEase(Ease.OutCubic).SetDelay(fadeDelay)).SetUpdate(true);
    }
}
