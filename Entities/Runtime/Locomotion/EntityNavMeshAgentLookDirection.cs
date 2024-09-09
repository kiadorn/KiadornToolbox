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

        [SerializeField]
        private float rotationSpeed = 20;

        private Vector3 targetDirection;

        public void ProcessLookAtPosition(Vector3 targetPosition)
        {
            targetDirection = (targetPosition - transform.position).normalized;
            IsLookDirectionManual = true;
        }

        private void Update()
        {
            float finalRotationSpeed = rotationSpeed;
            if (!IsLookDirectionManual)
            {
                targetDirection = transform.forward;
                finalRotationSpeed *= 5f;
            }

            LookDirectionVector = Vector3.RotateTowards(LookDirectionVector, targetDirection, finalRotationSpeed * Time.deltaTime, finalRotationSpeed * Time.deltaTime);
        }
    }
}