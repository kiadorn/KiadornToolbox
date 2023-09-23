using Kiadorn.CustomAttributes;
using Kiadorn.Utilities;
using UnityEngine;

namespace Kiadorn.Entities.Locomotion
{
    public class EntityMovementAnimation : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        [SerializeField, Min(0)]
        private float animationAcceleration = 0.5f;

        [SerializeField, RequireInterface(typeof(IEntityMovement))]
        private Object movement;
        private IEntityMovement movementInterface => movement as IEntityMovement;

        [SerializeField, RequireInterface(typeof(IEntityLookDirection))]
        private Object lookDirection;
        private IEntityLookDirection lookDirectionInterface => lookDirection as IEntityLookDirection;

        private float velocityZ;
        private float currentVelocityZ;
        private float velocityX;
        private float currentVelocityX;
        private float velocity;

        private void Update()
        {
            RotateModelTowardsLookDirection();

            SetXandZVelocity();
        }

        private void SetXandZVelocity()
        {
            float dotCompareMovementAndLookDirection = Vector3.Dot(movementInterface.Velocity.normalized, lookDirectionInterface.LookDirectionVector.normalized);
            float animationAccelerationModifier = 1 / animationAcceleration;

            SetZVelocity(dotCompareMovementAndLookDirection, animationAccelerationModifier);
            SetXVelocity(dotCompareMovementAndLookDirection, animationAccelerationModifier);

            SetVelocity();
        }

        private void SetVelocity()
        {
            velocity = Mathf.Clamp01(Mathf.Abs(velocityX) + Mathf.Abs(velocityZ));
            animator.SetFloat(Constants.VelocityHash, velocity);
        }

        private void SetXVelocity(float dotCompareMovementAndLookDirection, float animationAccelerationModifier)
        {
            bool isMoving = movementInterface.Velocity.magnitude > 0.01f;

            if (isMoving)
            {
                float angle = Vector3.SignedAngle(movementInterface.Velocity, lookDirectionInterface.LookDirectionVector.normalized, Vector3.up);
                bool leftOrRightDirection = angle > 0;
                float targetVelocityX = (1 - Mathf.Abs(dotCompareMovementAndLookDirection)) * (leftOrRightDirection ? -1 : 1);
                velocityX = Mathf.SmoothDamp(velocityX, targetVelocityX, ref currentVelocityX, animationAccelerationModifier);
            }
            else
            {
                velocityX = Mathf.SmoothDamp(velocityX, 0, ref currentVelocityX, animationAccelerationModifier);
            }
            animator.SetFloat(Constants.VelocityXHash, velocityX);
        }

        private void SetZVelocity(float dotCompareMovementAndLookDirection, float animationAccelerationModifier)
        {
            velocityZ = Mathf.SmoothDamp(velocityZ, dotCompareMovementAndLookDirection, ref currentVelocityZ, animationAccelerationModifier);
            animator.SetFloat(Constants.VelocityZHash, velocityZ);
        }

        private void RotateModelTowardsLookDirection()
        {
            transform.LookAt(lookDirectionInterface.LookDirectionVector + transform.position);
        }
    }
}