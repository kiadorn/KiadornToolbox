using UnityEngine;

namespace Kiadorn.Entities.Locomotion
{
    public class EntityCharacterControllerMovement : MonoBehaviour, IEntityMovement
    {
        public Vector3 Velocity => character.velocity;

        public float MaxSpeed => maxSpeed;

        [SerializeField]
        protected float maxSpeed = 10f;

        [SerializeField]
        protected float accelerateRate = 10f;

        [SerializeField]
        protected float deaccelerateRate = 10f;

        [SerializeField]
        protected CharacterController character;

        protected Vector3 characterInputVector;
        protected Vector3 targetVelocity;

        protected float currentSpeed;

        protected bool inControl;

        protected virtual void FixedUpdate()
        {
            character.Move(targetVelocity);
        }

        public virtual void ProcessDirectionVector(Vector2 movementVector)
        {
            characterInputVector = new Vector3(movementVector.x, 0, movementVector.y);

            bool isMoving = characterInputVector.magnitude > 0.05f;
            if (isMoving)
            {
                currentSpeed += Time.deltaTime * accelerateRate;
                currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
                targetVelocity += characterInputVector * currentSpeed;
                targetVelocity = Vector3.ClampMagnitude(characterInputVector * currentSpeed, maxSpeed);
            }
            else
            {
                targetVelocity *= deaccelerateRate;
                currentSpeed = 0;
            }
        }

        public virtual void ProcessTargetDestination(Vector3 targetPosition)
        {
            //TO-DO
        }
    }
}