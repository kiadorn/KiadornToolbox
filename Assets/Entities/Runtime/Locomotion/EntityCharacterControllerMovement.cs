using System.Collections.Generic;
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

        protected Vector3 characterInput;
        protected Vector3 targetVelocity;

        protected float currentSpeed;

        protected bool inControl = true;

        protected Dictionary<string, float> movementModifiers = new Dictionary<string, float>();

        protected void Awake()
        {
            inControl = true;
        }

        protected virtual void FixedUpdate()
        {
            character.Move(targetVelocity);
        }

        public virtual void ProcessDirectionVector(Vector2 movementVector)
        {
            if (!inControl)
            {
                return;
            }

            characterInput = new Vector3(movementVector.x, 0, movementVector.y);
            bool isMoving = characterInput.magnitude > 0.05f;

            if (isMoving)
            {
                Accelerate();
            }
            else
            {
                Deaccelerate();
            }
        }

        private void Accelerate()
        {
            currentSpeed += Time.deltaTime * accelerateRate;
            //currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
            float modifiedCurrentSpeed = currentSpeed * TotalModifiers();
            targetVelocity += characterInput * modifiedCurrentSpeed;
            targetVelocity = Vector3.ClampMagnitude(characterInput * modifiedCurrentSpeed, maxSpeed);
        }

        private float TotalModifiers()
        {
            float totalModifiers = 1;
            if (movementModifiers.Count > 0)
            {
                foreach (KeyValuePair<string, float> modifier in movementModifiers)
                {
                    totalModifiers += modifier.Value;
                }
            }

            return totalModifiers;
        }

        private void Deaccelerate()
        {
            targetVelocity *= deaccelerateRate;
            currentSpeed = 0;
        }

        public virtual void ProcessTargetDestination(Vector3 targetPosition)
        {
            //TO-DO
        }

        public virtual void Stop()
        {
            targetVelocity = Vector3.zero;
            currentSpeed = 0;
        }

        public void AddMovementModifier(string modifierKey, float modifierValue)
        {
            movementModifiers.TryAdd(modifierKey, modifierValue);
        }

        public void RemoveMovementModifier(string modifierKey)
        {
             movementModifiers.Remove(modifierKey);
        }
    }
}