using DG.Tweening;
using UnityEngine;

public sealed class Tutorial : MonoBehaviour
{
    [Header("Tutorial transforms")]
    [SerializeField] private Transform tutorialContainer;
    [SerializeField] private RectTransform tutorialHand;
    [Header("Animation settings")]
    [SerializeField] private float holdTime;

    private Sequence _tutorialSequence;
    private void Start()
    {
        GameController.Instance.OnLevelLoaded += Init;
    }

    private void Init(int i)
    {
        PlayTutorial();
        InputSystem.Instance.OnTouch += StopTutorial;
    }

    private void StopTutorial()
    {
        _tutorialSequence?.Kill();
        InputSystem.Instance.OnTouch -= StopTutorial;
        tutorialContainer.gameObject.SetActive(false);
    }

    private void PlayTutorial()
    {
        tutorialContainer.gameObject.SetActive(true);

        _tutorialSequence = DOTween.Sequence()
            .Append(tutorialHand.DOScale(1*Vector3.one, holdTime / 2).From(0.6f * Vector3.one).SetEase(Ease.OutCubic))
            .Append(tutorialHand.DOScale(0.6f*Vector3.one, holdTime).From(Vector3.one).SetEase(Ease.InQuart)).SetLoops(-1);
    }
}
