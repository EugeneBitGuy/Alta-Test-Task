using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public sealed class SettingsUI : MonoBehaviour
{
    [Header("Select settings")]
    [SerializeField] private bool showVibration;
    [SerializeField] private bool showSound;

    [Header("Buttons")]
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button soundButton;
    [SerializeField] private Button vibrationButton;
    
    [Header("Images")]
    [SerializeField] private Image soundImage;
    [SerializeField] private Image vibrationImage;
    [SerializeField] private Image background;
    
    [Header("On/Off Sprites")]
    [SerializeField] private Sprite soundOnSprite;
    [SerializeField] private Sprite soundOffSprite;
    [SerializeField] private Sprite vibrationOnSprite;
    [SerializeField] private Sprite vibrationOffSprite;

    [SerializeField] private float openCloseAnimationTime = 0.2f;

    private bool _isOpened = false;
    private Sequence _toggleSequence;
    
    private void Start()
    {
        settingsButton.onClick.AddListener(OnSettingsButtonClick);
        soundButton.onClick.AddListener(OnSoundButtonClick);
        vibrationButton.onClick.AddListener(OnVibrationButtonClick);

        SetSound(SaveLoadController.Instance.Data.SettingsData.SoundEnabled);
        SetVibration(SaveLoadController.Instance.Data.SettingsData.VibrationEnabled);
    }

    private void TogglePanel()
    {
        _isOpened = !_isOpened;
        
        soundButton.gameObject.SetActive(_isOpened && showSound);
        vibrationButton.gameObject.SetActive(_isOpened && showVibration);

        var newAlpha = _isOpened ? 1f : 0f;
        
        _toggleSequence?.Kill();

        _toggleSequence = DOTween.Sequence(
            background.DOFade(newAlpha, openCloseAnimationTime)).Join(
            soundImage.DOFade(newAlpha, openCloseAnimationTime)).Join(
            vibrationImage.DOFade(newAlpha, openCloseAnimationTime)).Join(
            soundButton.image.DOFade(newAlpha, openCloseAnimationTime)).Join(
            vibrationButton.image.DOFade(newAlpha, openCloseAnimationTime)).SetEase(Ease.Linear);

        _toggleSequence.Play();
    }
    
    private void OnSettingsButtonClick()
    {
        TogglePanel();
    }

    private void OnSoundButtonClick()
    {
        SetSound(!SaveLoadController.Instance.Data.SettingsData.SoundEnabled);
    }

    private void OnVibrationButtonClick()
    {
        SetVibration(!SaveLoadController.Instance.Data.SettingsData.VibrationEnabled);
        Handheld.Vibrate();
    }

    private void SetVibration(bool enable)
    {
        SaveLoadController.Instance.Data.SettingsData.VibrationEnabled = enable;
        vibrationImage.sprite = enable ? vibrationOnSprite : vibrationOffSprite;
    }

    private void SetSound(bool enable)
    {
        SaveLoadController.Instance.Data.SettingsData.SoundEnabled = enable;
        soundImage.sprite = enable ? soundOnSprite : soundOffSprite;
        AudioListener.volume = enable ? 1f : 0f;
    }
}
