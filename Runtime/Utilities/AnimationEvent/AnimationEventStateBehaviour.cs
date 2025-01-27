using System;
using UnityEngine;

namespace Kiadorn.Utilities
{
    public class AnimationEventStateBehaviour : StateMachineBehaviour
    {
        public AnimationEventReference eventReference;

        [Range(0, 1)]
        public float triggerTime;

        private bool hasTriggered;

        private AnimationEventReceiver receiver;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (receiver == null)
            {
                receiver = animator.GetComponent<AnimationEventReceiver>();
            }
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            float currentTime = stateInfo.normalizedTime;

            if (currentTime >= triggerTime && !hasTriggered)
            {
                NotifyReceiver();
                hasTriggered = true;
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            hasTriggered = false;
        }

        private void NotifyReceiver()
        {
            if (receiver != null)
            {
                receiver.OnAnimationEventTriggered(eventReference);
            }
        }
    }
}