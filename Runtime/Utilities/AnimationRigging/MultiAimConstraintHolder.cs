using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Kiadorn.Rigging
{
    public class MultiAimConstraintHolder : RigConstraintHolder
    {
        [SerializeField]
        private MultiAimConstraint multiAimConstraint;

        [SerializeField]
        private Transform sourceObject;

        /// <summary>
        /// Constraint weight needs to be set in Update, else it is not recognized by the Job system.
        /// </summary>
        private void Update()
        {
            if (multiAimConstraint.weight != targetWeight)
            {
                multiAimConstraint.weight = targetWeight;
            }
        }

        public override void SetTargetPosition(Vector3 target)
        {
            sourceObject.localPosition = target;
        }
    }
}