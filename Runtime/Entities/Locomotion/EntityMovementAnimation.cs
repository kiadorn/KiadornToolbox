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

        [SerializeField]
        private float rotateThreshold;

        [SerializeField]
        private AvatarMask bodyMask;

        private float velocityZ;
        private float velocityX;
        private float velocity;

        private Vector3 lastLookDirection;

        private bool isMoving = false;
        private bool wasMoving = true;

        private void Update()
        {
            isMoving = movementInterface.Velocity.magnitude > 0.05f;
            RotateModelTowardsLookDirection();
            //SetShuffleLeftRight();
            SetXandZVelocity();
            //SetAvatarBodyMask();
            wasMoving = isMoving;
        }

        private void SetAvatarBodyMask()
        {
            if (bodyMask == null || wasMoving == isMoving)
                return;

            bodyMask.SetHumanoidBodyPartActive(AvatarMaskBodyPart.LeftLeg, !isMoving);
            bodyMask.SetHumanoidBodyPartActive(AvatarMaskBodyPart.RightLeg, !isMoving);
            bodyMask.SetHumanoidBodyPartActive(AvatarMaskBodyPart.Root, !isMoving);
            bodyMask.SetHumanoidBodyPartActive(AvatarMaskBodyPart.LeftFootIK, !isMoving);
            bodyMask.SetHumanoidBodyPartActive(AvatarMaskBodyPart.RightFootIK, !isMoving);
        }

        private void SetXandZVelocity()
        {
            float dotCompareMovementAndLookDirection = Vector3.Dot(movementInterface.Velocity.normalized, lookDirectionInterface.LookDirectionVector.normalized);

            SetZVelocity(dotCompareMovementAndLookDirection);
            SetXVelocity(dotCompareMovementAndLookDirection);

            SetVelocity();
        }

        private void SetVelocity()
        {
            float targetVelocity = isMoving ? movementInterface.Velocity.magnitude / movementInterface.MaxSpeed : 0;
            velocity = Mathf.Lerp(velocity, targetVelocity, animationAcceleration * Time.deltaTime);
            animator.SetFloat(Constants.VelocityHash, velocity);
        }

        private void SetXVelocity(float dotCompareMovementAndLookDirection)
        {
            float targetVelocityX = 0;

            if (isMoving)
            {
                float angle = Vector3.SignedAngle(movementInterface.Velocity, lookDirectionInterface.LookDirectionVector, Vector3.up);
                bool leftOrRightDirection = angle > 0;
                targetVelocityX = (1 - Mathf.Abs(dotCompareMovementAndLookDirection)) * (leftOrRightDirection ? -1 : 1);
            }
            velocityX = Mathf.Lerp(velocityX, targetVelocityX, animationAcceleration * Time.deltaTime);
            animator.SetFloat(Constants.VelocityXHash, velocityX);
        }

        private void SetZVelocity(float dotCompareMovementAndLookDirection)
        {
            float targetVelocityZ = 0;
            if (isMoving)
            {
                targetVelocityZ = dotCompareMovementAndLookDirection;
            }
            velocityZ = Mathf.Lerp(velocityZ, targetVelocityZ, animationAcceleration * Time.deltaTime);
            animator.SetFloat(Constants.VelocityZHash, velocityZ);
        }

        private void RotateModelTowardsLookDirection()
        {
            transform.LookAt(lookDirectionInterface.LookDirectionVector + transform.position);
        }

        private void SetShuffleLeftRight()
        {
            int isRotatingRight = 0;
            if (!isMoving)
            {
                float newAngle = Vector3.SignedAngle(lastLookDirection, lookDirectionInterface.LookDirectionVector, Vector3.up);

                if (newAngle > rotateThreshold)
                {
                    isRotatingRight = 1;
                }
                else if (newAngle < -rotateThreshold)
                {
                    isRotatingRight = -1;
                }
            }
            animator.SetInteger(Constants.RotateHash, isRotatingRight);
            lastLookDirection = lookDirectionInterface.LookDirectionVector;
        }
    }
}