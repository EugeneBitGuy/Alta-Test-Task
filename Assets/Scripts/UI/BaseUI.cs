using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
    [SerializeField] private bool hideOnStart;
    [SerializeField] private GameObject content;

    protected virtual void Start()
    {
        GameController.Instance.OnInitCompleted += Init;
    }

    protected virtual void Init()
    {
        if (hideOnStart)
        {
            HideImmediately();
        }
    }

    public virtual void Show()
    {
        content.SetActive(true);
    }

    public virtual void Hide()
    {
        HideImmediately();
    }

    public virtual void HideImmediately()
    {
        content.SetActive(false);
    }
}
