using UnityEngine;

namespace Kiadorn.Entities.Locomotion
{
    public class EntityCharacterControllerTriggerColliderPushback : MonoBehaviour
    {
        [SerializeField]
        private EntityCharacterControllerMovement movement;

        private void OnTriggerEnter(Collider other)
        {
            if (movement != null)
            {
                //Pushback(other);
            }
        }

        private void Pushback(Collider other)
        {
            movement.ProcessMovementDirection(-movement.Velocity);
        }
    }
}