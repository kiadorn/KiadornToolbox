using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Kiadorn.Entities.Locomotion
{
    public class EntityNavMeshAgentMovement : MonoBehaviour, IEntityMovement
    {
        public Vector3 Velocity => agent.velocity;

        public float MaxSpeed => agent.speed;

        public float AngleSpeed = 1;

        [SerializeField]
        protected NavMeshAgent agent;

        protected Dictionary<string, float> movementModifiers = new Dictionary<string, float>();

        public void ProcessMovementDirection(Vector2 direction)
        {
            direction.Normalize();
            agent.isStopped = false;
            agent.ResetPath();
            agent.Move(new Vector3(direction.x, 0, direction.y));
        }

        public void ProcessTargetDestination(Vector3 targetPosition)
        {
            agent.isStopped = false;
            agent.ResetPath();
            agent.SetDestination(targetPosition);
        }

        public void ProcessRotateTowards(Vector3 targetPosition)
        {
            Vector3 direction = (targetPosition - agent.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, lookRotation, Time.deltaTime * AngleSpeed);
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