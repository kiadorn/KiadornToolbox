using UnityEngine;

namespace Kiadorn.ObjectPooling
{
    [CreateAssetMenu(menuName = "Kiadorn/ObjectPoolReference")]
    public class ObjectPoolReference : ScriptableObject
    {
        public IObjectPoolHolder ObjectPoolHolder;
    }
}