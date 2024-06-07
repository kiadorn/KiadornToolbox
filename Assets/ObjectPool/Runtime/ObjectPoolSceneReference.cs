using Kiadorn.CustomAttributes;
using UnityEngine;

namespace Kiadorn.ObjectPooling
{
    public class ObjectPoolSceneReference : MonoBehaviour
    {
        [SerializeField]
        private ObjectPoolReference objectPoolReference;

        [SerializeField, RequireInterface(typeof(IObjectPoolHolder))]
        private Object objectPool;
        private IObjectPoolHolder objectPoolHolder => objectPool as IObjectPoolHolder;
       
        private void Awake()
        {
            objectPoolReference.ObjectPoolHolder = objectPoolHolder;
        }
    }
}