using UnityEngine;

namespace Kiadorn.Entities.Locomotion
{
    public interface IEntityMovement
    {
        public Vector3 Velocity { get; }

        public float MaxSpeed { get; }

        public void ProcessDirectionVector(Vector2 direction);

        public void ProcessTargetDestination(Vector3 targetPosition);
    }
}