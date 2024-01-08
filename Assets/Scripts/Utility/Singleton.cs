using UnityEngine;

namespace GyroSpace.Utility
{

    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T _instance;

        public static T _Instance { get => GetInstance(); }

        [SerializeField] protected bool _dontDestroyOnLoad = false;

        public virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = (T)this;

                if (_dontDestroyOnLoad)
                {
                    DontDestroyOnLoad(gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }

        public static T GetInstance()
        {
            if (_instance == null)
            {
                var go = new GameObject(typeof(T).ToString());
                var gs = go.AddComponent<T>();
                go.hideFlags = HideFlags.DontSaveInBuild | HideFlags.DontSaveInEditor;
                _instance = gs;
            }

            return _instance;
        }
    }
}