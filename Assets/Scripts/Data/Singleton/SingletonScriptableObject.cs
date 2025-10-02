using UnityEngine;

namespace Data.Singleton
{
    public abstract class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    // Recherche dans le projet
                    T[] assets = Resources.LoadAll<T>("");

                    if (assets.Length > 0)
                    {
                        _instance = assets[0];
                    }

#if UNITY_EDITOR
                    if (_instance == null)
                    {
                        Debug.LogError($"Aucun asset de type {typeof(T)} trouvé dans Resources !");
                    }
#endif
                }
                return _instance;
            }
        }
    }

}