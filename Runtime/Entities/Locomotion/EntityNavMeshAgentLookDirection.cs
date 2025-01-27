using UnityEngine;
using UnityEngine.AI;

namespace Kiadorn.Entities.Locomotion
{
    public class EntityNavMeshAgentLookDirection : MonoBehaviour, IEntityLookDirection
    {
        public Vector3 LookDirection { get; set; }
        public Vector3 LookDirectionPosition { get; set; }
        public bool IsLookDirectionManual { get; set; }

        [SerializeField]
        private NavMeshAgent agent;

        private Vector3 targetDirection;

        private void Update()
        {
            LookDirection = agent.transform.forward;
            LookDirectionPosition = agent.pathEndPosition;
            RotateTowardsTarget();
        }

        public void ProcessLookAtPosition(Vector3 targetPosition)
        {
            targetDirection = (targetPosition - transform.position).normalized;
            IsLookDirectionManual = true;
        }

        private void RotateTowardsTarget()
        {
            if (!IsLookDirectionManual)
            {
                targetDirection = agent.velocity.normalized != Vector3.zero ? agent.velocity.normalized : agent.transform.forward.normalized;
            }

            LookDirection = targetDirection;
        }
    }
}