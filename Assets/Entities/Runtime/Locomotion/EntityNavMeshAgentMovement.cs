using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Kiadorn.Entities.Locomotion
{
    public class EntityNavMeshAgentMovement : MonoBehaviour, IEntityMovement
    {
        public Vector3 Velocity => agent.velocity;

        public float MaxSpeed => agent.speed;

        [SerializeField]
        protected NavMeshAgent agent;

        protected Dictionary<string, float> movementModifiers = new Dictionary<string, float>();

        public void ProcessMovementDirection(Vector2 direction)
        {
            direction.Normalize();
            agent.ResetPath();
            agent.Move(new Vector3(direction.x, 0, direction.y));
        }

        public void ProcessTargetDestination(Vector3 targetPosition)
        {
            agent.ResetPath();
            agent.SetDestination(targetPosition);
        }

        public void Stop()
        {
            agent.isStopped = true;
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