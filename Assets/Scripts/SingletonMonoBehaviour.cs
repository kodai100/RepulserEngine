using UnityEngine;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance != null) return _instance;
            
            var t = typeof(T);
            
            _instance = (T) FindObjectOfType(t);
            
            if (_instance == null)
            {
                Debug.LogError($"There is no {t} in this scene");
            }

            return _instance;
        }
    }
}