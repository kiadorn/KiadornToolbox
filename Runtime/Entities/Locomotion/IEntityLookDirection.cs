using UnityEngine;

namespace Kiadorn.Entities.Locomotion
{
    public interface IEntityLookDirection
    {
        public Vector3 LookDirection { get; set; }
        public Vector3 LookDirectionPosition { get; set; }

        public bool IsLookDirectionManual { get; set; }

        public void ProcessLookAtPosition(Vector3 targetPosition);
    }
}