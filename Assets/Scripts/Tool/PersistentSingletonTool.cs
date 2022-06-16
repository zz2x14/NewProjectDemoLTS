using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentSingletonTool<T> : MonoBehaviour where T : PersistentSingletonTool<T>//T必须继承名为这个类
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = (T) this;
        }
        else
        {
            if (Instance != (T) this)
            {
                Destroy(gameObject);
            }
        }
        
        DontDestroyOnLoad(gameObject);
    }
}
