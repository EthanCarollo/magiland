using UnityEngine;

public class BaseController<T> : MonoBehaviour where T : Object
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null) _instance = FindFirstObjectByType<T>();
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance != null && _instance != (Object)(T)(object)this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = (T)(object)this;
        DontDestroyOnLoad(gameObject);
    }
}