using UnityEngine;

namespace Kiadorn.Entities.Locomotion
{
    public class EntityNavMeshAgentLookDirection : MonoBehaviour, IEntityLookDirection
    {
        public Vector3 LookDirectionVector => transform.forward;

        [SerializeField]
        private Transform lookTransform;
    }
}
