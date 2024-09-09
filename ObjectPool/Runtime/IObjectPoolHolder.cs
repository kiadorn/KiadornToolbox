using UnityEngine;

namespace Kiadorn.ObjectPooling
{
    public interface IObjectPoolHolder
    {
        public GameObject Prefab { get; }

        public int MaxPoolSize { get; }

        public int DefaultCapacity { get; }

        public bool CollectionChecks { get; }

        public IObjectPoolItem Get();

        public void OnDestroyPoolObject(IObjectPoolItem obj);

        public void OnReturnedToPool(IObjectPoolItem obj);

        public void OnTakeFromPool(IObjectPoolItem obj);

        public IObjectPoolItem CreatePooledItem();
    }
}
