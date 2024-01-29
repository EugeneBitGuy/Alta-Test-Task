using UnityEngine;

public abstract class MonoSingletone<T> : MonoBehaviour where T : MonoBehaviour
{    
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    Debug.LogError($"{typeof(T).ToString()} MonoSingleton is null");
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
            _instance = this as T;
    }
}
