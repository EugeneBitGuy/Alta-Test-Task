using DG.Tweening;
using UnityEngine;

public sealed class FinishDoors : MonoBehaviour
{
    [SerializeField] private Transform leftDoor;
    [SerializeField] private Transform rightDoor;
    [Header("Animation settings")]
    [SerializeField] private float openedAngle = 110;
    [SerializeField] private float openingTime = 3f;
    public void Open()
    {
        leftDoor.DORotate(Vector3.up * openedAngle, openingTime).SetEase(Ease.OutBounce);
        rightDoor.DORotate(Vector3.down * openedAngle, openingTime).SetEase(Ease.OutBounce);
    }
}
