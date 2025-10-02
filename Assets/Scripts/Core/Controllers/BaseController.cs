using UnityEngine;

public class BaseController<T> : MonoBehaviour
where T : Object
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<T>();
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
