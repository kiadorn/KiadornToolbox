using UnityEngine;
using UnityEngine.AI;

namespace Kiadorn.Entities.Locomotion
{
    public class EntityNavMeshAgentMovement : MonoBehaviour, IEntityMovement
    {
        public Vector3 Velocity => m_agent.velocity;

        public float MaxSpeed => m_agent.speed;

        [SerializeField]
        protected NavMeshAgent m_agent;

        public void ProcessDirectionVector(Vector2 direction)
        {
            m_agent.Move(new Vector3(direction.x, 0, direction.y));
        }

        public void ProcessTargetDestination(Vector3 targetPosition)
        {
            m_agent.ResetPath();
            m_agent.SetDestination(targetPosition);
        }
    }
}