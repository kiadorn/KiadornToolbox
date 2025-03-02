using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kiadorn.Rigging
{
    public class RigConstraintController : MonoBehaviour
    {
        [SerializeField]
        private RigConstraintPair[] constraints;

        private Dictionary<RigConstraintType, RigConstraintHolder> constraintLookup = new Dictionary<RigConstraintType, RigConstraintHolder>();

        private void Start()
        {
            for (int i = 0; i < constraints.Length; i++)
            {
                constraintLookup.Add(constraints[i].type, constraints[i].holder);
            }
        }

        public void SetWeight(RigConstraintType constraintType, float weight)
        {
            if (constraintLookup.TryGetValue(constraintType, out RigConstraintHolder holder))
            {
                holder.SetTargetWeight(weight);
            }
        }

        public RigConstraintHolder GetRigConstraint(RigConstraintType constraintType)
        {
            return constraintLookup[constraintType];
        }
    }

    [Serializable]
    public class RigConstraintPair
    {
        public RigConstraintType type;
        public RigConstraintHolder holder;
    }

    public enum RigConstraintType
    {
        None,
        Chest,
        LeftArm,
        RightArm,
        Head,
        Spine,
    }
}