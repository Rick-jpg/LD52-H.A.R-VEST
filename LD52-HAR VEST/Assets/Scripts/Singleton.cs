using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static bool m_ShuttingDown = false;
    private static object m_Lock = new object();
    private static T m_Instance;
    private bool _initialized = false;

    public static T Instance
    {
        get
        {
            if (m_ShuttingDown)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                                 "' already destroyed. Returning null.");
                return null;
            }

            lock (m_Lock)
            {
                if (m_Instance == null)
                {
                    m_Instance = (T)FindObjectOfType(typeof(T));
                    DontDestroyOnLoad(m_Instance.gameObject);

                    if (m_Instance == null)
                    {
                        var singletonObject = new GameObject();
                        m_Instance = singletonObject.AddComponent<T>();
                        singletonObject.name = typeof(T).ToString() + " (Singleton)";
                        //DontDestroyOnLoad(singletonObject);
                    }

                    if (!m_Instance._initialized)
                    {
                        m_Instance.Init();
                        m_Instance._initialized = true;
                    }
                }

                return m_Instance;
            }
        }
    }

    protected virtual void Init()
    {

    }

    private void OnApplicationQuit()
    {
        m_ShuttingDown = true;
    }
}