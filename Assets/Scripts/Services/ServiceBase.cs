using UnityEngine;

namespace Services
{
    public abstract class ServiceBase<T> : MonoBehaviour where T : ServiceBase<T>
    {
        private static bool _destroyed;
        private static T _instance;
        private bool _awaken;
 
        public static T Instance {
            get => _instance != null || _destroyed ? _instance : Load();
            set => _instance = value;
        }
 
        // services can be initiated from one source
        public static void Init() {
            if ( _instance == null )
                Load();
        }
	
        private static T Load() {
            var inst = (T)FindObjectOfType(typeof(T));
            if ( inst == null ) {
                var obj = new GameObject(typeof(T).Name);
                inst = obj.AddComponent<T>();
            }

            inst.Awake();
            return inst;
        }
 	
        public virtual void Awake()
        {
            if (_awaken)
                return;

            _awaken = true;
 
            if ( _instance != null && _instance != this ) {
                Destroy(gameObject);
                return;
            }
 
            _instance = (T)this;
            DontDestroyOnLoad(gameObject);
            DoAwake();
        }
 
        public void OnDestroy()
        {
            if (_instance != this) return;
            _destroyed = true;
            DoDestroy();
        }
 
        protected virtual void DoAwake() {
        }
 
        protected virtual void DoDestroy() {
        }

    }
}
