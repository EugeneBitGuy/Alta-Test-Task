using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoSingletone<T> : MonoBehaviour where T : MonoBehaviour
{    
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    Debug.LogError($"{typeof(T).ToString()} MonoSingleton is null");
                }
            }

            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
            instance = this as T;
    }
}
