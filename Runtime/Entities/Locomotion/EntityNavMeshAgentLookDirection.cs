using UnityEngine;
using UnityEngine.AI;

namespace Kiadorn.Entities.Locomotion
{
    public class EntityNavMeshAgentLookDirection : MonoBehaviour, IEntityLookDirection
    {
        public Vector3 LookDirectionVector { get; set; }
        public bool IsLookDirectionManual { get; set; }

        [SerializeField]
        private NavMeshAgent agent;

        private Vector3 targetDirection;

        private void Update()
        {
            LookDirectionVector = agent.transform.forward;
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

            LookDirectionVector = targetDirection;
        }
    }
}