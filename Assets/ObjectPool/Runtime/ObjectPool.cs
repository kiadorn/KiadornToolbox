using UnityEngine;
using UnityEngine.Pool;

namespace Kiadorn.ObjectPooling
{
    public class ObjectPool : MonoBehaviour, IObjectPoolHolder
    {
        public IObjectPool<IObjectPoolItem> Pool
        {
            get
            {
                if (pool == null)
                {
                    pool = new ObjectPool<IObjectPoolItem>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, 10, maxPoolSize);
                }
                return pool;
            }
        }

        protected IObjectPool<IObjectPoolItem> pool;

        public GameObject Prefab { get => prefab; }
        [SerializeField]
        protected GameObject prefab;

        public int MaxPoolSize => maxPoolSize;

        protected int maxPoolSize = 10;
        public int DefaultCapacity => defaultCapacity;
        [SerializeField]
        protected int defaultCapacity = 10;

        [SerializeField]
        public bool CollectionChecks => collectionChecks;

        [SerializeField]
        protected bool collectionChecks = true;

        public IObjectPoolItem Get()
        {
            return Pool.Get();
        }

        public void OnDestroyPoolObject(IObjectPoolItem obj)
        {
            obj.DestroyPoolObject();
        }

        public void OnReturnedToPool(IObjectPoolItem obj)
        {
            obj.ReturnToPool();
        }

        public void OnTakeFromPool(IObjectPoolItem obj)
        {
            obj.TakeFromPool();
        }

        public IObjectPoolItem CreatePooledItem()
        {
            GameObject instantiatedObject = Instantiate(prefab, transform);
            IObjectPoolItem poolItem = instantiatedObject.GetComponent<IObjectPoolItem>();
            poolItem.SetPoolOwner(Pool);

            return poolItem;
        }
    }
}