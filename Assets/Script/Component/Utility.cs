using System.Collections.Generic;
using UnityEngine;

public class Utility
{
    #region Design Pattern
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T instance;
        public static T Instance { get { return instance; } }

        protected void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("Instance of this singleton " + (T)this + " already exists, deleting!");
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
                instance = (T)this;
            }
        }
    }
    #endregion

    #region Optimization Pattern
    public class ObjectPool
    {
        public GameObject basePrefab;

        private Queue<GameObject> objectPool;

        private Transform parent;

        public string PrefabName => basePrefab.name;

        public ObjectPool(GameObject _prefab, Transform _parent, int _poolSize)
        {
            parent = _parent;
            basePrefab = _prefab;
            objectPool = new Queue<GameObject>();

            for (int i = 0; i < _poolSize; i++)
            {
                GameObject obj = MonoBehaviour.Instantiate(_prefab, _parent);
                obj.name = PrefabName + "_" + i; // to remove the (Clone) label
                objectPool.Enqueue(obj);
                obj.SetActive(false);
            }
        }

        public GameObject ActivateObject()
        {
            if (objectPool.Count > 0)
            {
                GameObject obj = objectPool.Dequeue();
                return obj;
            }
            //else
            //{
            //    GameObject obj = MonoBehaviour.Instantiate(basePrefab, parent);
            //    obj.name = PrefabName; // to remove the (Clone) label
            //    return obj;
            //}
            return null;
        }

        public void DeactiveObject(GameObject _obj)
        {
            objectPool.Enqueue(_obj);
            _obj.SetActive(false);
        }
    }
    #endregion
}
