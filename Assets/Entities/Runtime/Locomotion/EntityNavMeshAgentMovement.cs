using System.Collections.Generic;
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

        protected Dictionary<string, float> movementModifiers = new Dictionary<string, float>();


        public void ProcessDirectionVector(Vector2 direction)
        {
            direction.Normalize();
            m_agent.ResetPath();
            m_agent.Move(new Vector3(direction.x, 0, direction.y));
        }

        public void ProcessTargetDestination(Vector3 targetPosition)
        {
            m_agent.ResetPath();
            m_agent.SetDestination(targetPosition);
        }

        public void Stop()
        {
            m_agent.ResetPath();
        }

        public void AddMovementModifier(string modifierKey, float modifierValue)
        {
            movementModifiers.Add(modifierKey, modifierValue);
        }

        public void RemoveMovementModifier(string modifierKey)
        {
            movementModifiers.Remove(modifierKey);
        }
    }
}