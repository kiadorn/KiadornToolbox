using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Kiadorn.Rigging
{
    public class TwoBoneIKConstraintHolder : RigConstraintHolder
    {
        [SerializeField]
        private TwoBoneIKConstraint twoBoneIKConstraint;

        [SerializeField]
        private Transform sourceObject;

        /// <summary>
        /// Constraint weight needs to be set in Update, else it is not recognized by the Job system.
        /// </summary>
        private void Update()
        {
            if (twoBoneIKConstraint.weight != targetWeight)
            {
                twoBoneIKConstraint.weight = targetWeight;
            }
        }

        public override void SetTargetPosition(Vector3 target)
        {
            sourceObject.localPosition = target;
        }
    }
}