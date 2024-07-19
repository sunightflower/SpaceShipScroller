using UnityEngine;
using System.Linq;
using System.Collections.Generic;


namespace CustomPool
{
    public class CustomPool<T> where T : MonoBehaviour
    {
        private readonly T _prefab;
        private readonly List<T> _objects;

        public CustomPool(T prefab, int prewarnObjects)
        {
            _prefab = prefab;
            _objects = new List<T>();

            for (int i = 0; i < prewarnObjects; i++)
            {
                var obj = GameObject.Instantiate(_prefab);
                obj.gameObject.SetActive(false);
                _objects.Add(obj);
            }
        }

        public T Get()
        {
            var obg = _objects.FirstOrDefault(x => !x.isActiveAndEnabled) ?? Create();
            obg.gameObject.SetActive(true);
            return obg;
        }

        public void Release(T obj)
        {
            obj.gameObject.SetActive(false);
        }

        private T Create()
        {
            var obj = GameObject.Instantiate(_prefab);
            _objects.Add(obj);
            return obj;
        }
    }
}

