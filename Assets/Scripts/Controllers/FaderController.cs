using UnityEngine;

public sealed class FaderController : MonoBehaviour
{
    [SerializeField] private BaseFader fader;

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        ShowImmediately();
        GameController.Instance.OnShowFadeUI += OnLevelStartLoading;
        GameController.Instance.OnLevelLoaded += OnLevelLoaded;
    }

    private void OnLevelStartLoading()
    {
        Show();
    }

    private void OnLevelLoaded(int levelNumber)
    {
        Hide();
    }

    public void Show()
    {
        if (fader == null) return;
        if (_canvasGroup == null) return;

        fader.Show(Callback);
        _canvasGroup.blocksRaycasts = true;

        void Callback()
        {
            GameController.Instance.InvokeOnStartLevelLoading();
        }
    }

    public void Hide()
    {
        if (fader == null) return;
        if (_canvasGroup == null) return;

        fader.Hide(Help);

        void Help()
        {
            _canvasGroup.blocksRaycasts = false;
        }
    }

    public void ShowImmediately()
    {
        if (fader == null) return;
        if (_canvasGroup == null) return;

        fader.ShowImmediately();
        _canvasGroup.blocksRaycasts = true;
    }

    public void HideImmediately()
    {
        if (fader == null) return;
        if (_canvasGroup == null) return;

        fader.HideImmediately();
        _canvasGroup.blocksRaycasts = false;
    }
}