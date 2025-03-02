using UnityEngine;

namespace Kiadorn.Rigging
{
    public class AnimationConstraintBehaviour : StateMachineBehaviour
    {
        public bool shouldSmooth = true;
        public bool forceSetStart = false;
        public bool forceSetEnd = true;
        public float start = 0.2f;
        public float end = 0.2f;

        [Header("Rigging")]
        public Vector3 targetLocalPosition;

        public float targetWeight;

        public RigConstraintType constraintType;
        private RigConstraintHolder rigConstraint;

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Initialize(animator);
            if (forceSetEnd)
            {
                rigConstraint.SetTargetWeight(0);
            }
        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Initialize(animator);
            if (forceSetStart)
            {
                rigConstraint.SetTargetWeight(targetWeight);
            }
            rigConstraint.SetTargetPosition(targetLocalPosition);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!shouldSmooth)
                return;

            float constraintWeight = 0;
            float normalizedTime = stateInfo.normalizedTime;
            float endThreshold = 1 - end;

            if (normalizedTime < start)
            {
                constraintWeight = normalizedTime / start;
            }
            else if (normalizedTime > endThreshold)
            {
                constraintWeight = 1 - ((normalizedTime - endThreshold) / end);
            }
            else
            {
                constraintWeight = 1;
            }

            rigConstraint.SetTargetWeight(constraintWeight);

            if (normalizedTime < endThreshold)
            {
                return;
            }
        }

        private void Initialize(Animator animator)
        {
            if (rigConstraint == null)
            {
                rigConstraint = animator.GetComponentInChildren<RigConstraintController>().GetRigConstraint(constraintType);
            }
        }
    }
}