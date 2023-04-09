using UnityEngine;
using UnityEngine.Pool;

namespace Kiadorn.ObjectPooling
{
    public class ObjectPool : MonoBehaviour
    {
        public IObjectPool<IObjectPoolItem> Pool
        {
            get
            {
                if (m_Pool == null)
                {
                    m_Pool = new ObjectPool<IObjectPoolItem>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, 10, maxPoolSize);
                }
                return m_Pool;
            }
        }

        protected IObjectPool<IObjectPoolItem> m_Pool;

        [SerializeField]
        protected int maxPoolSize = 10;

        [SerializeField]
        protected bool collectionChecks = true;

        [SerializeField]
        protected GameObject prefab;

        protected void OnDestroyPoolObject(IObjectPoolItem obj)
        {
            obj.DestroyPoolObject();
        }

        protected void OnReturnedToPool(IObjectPoolItem obj)
        {
            obj.ReturnToPool();
        }

        protected void OnTakeFromPool(IObjectPoolItem obj)
        {
            obj.TakeFromPool();
        }

        protected IObjectPoolItem CreatePooledItem()
        {
            GameObject instantiatedObject = Instantiate(prefab, transform);
            IObjectPoolItem poolItem = instantiatedObject.GetComponent<IObjectPoolItem>();
            poolItem.SetPoolOwner(Pool);

            return poolItem;
        }
    }
}