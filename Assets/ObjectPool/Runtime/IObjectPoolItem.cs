using UnityEngine.Pool;

namespace Kiadorn.ObjectPooling
{
    public interface IObjectPoolItem
    {
        void SetPoolOwner(IObjectPool<IObjectPoolItem> owner);
        void TakeFromPool();
        void DestroyPoolObject();
        void ReturnToPool();
    }
}
