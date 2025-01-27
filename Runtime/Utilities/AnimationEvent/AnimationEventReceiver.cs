using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Kiadorn.Utilities
{
    public class AnimationEventReceiver : MonoBehaviour
    {
        [SerializeField]
        private List<AnimationEvent> animationEvents;

        private void OnDestroy()
        {
            animationEvents.Clear();
        }

        public void OnAnimationEventTriggered(AnimationEventReference eventReference)
        {
            AnimationEvent animationEvent = animationEvents.Find(x => x.eventReference == eventReference);

            if (animationEvent != null && animationEvent.OnAnimationEvent != null)
            {
                animationEvent.OnAnimationEvent.Invoke();
            }
        }

        public void AddEventListener(AnimationEventReference animationEvent, UnityAction unityAction)
        {
            for (int i = 0; i < animationEvents.Count; i++)
            {
                if (animationEvents[i].eventReference == animationEvent)
                {
                    animationEvents[i].OnAnimationEvent.AddListener(unityAction);
                    return;
                }
            }

            AnimationEvent newAnimationEvent = new AnimationEvent
            {
                eventReference = animationEvent,
                OnAnimationEvent = new UnityEvent()
            };
            newAnimationEvent.OnAnimationEvent.AddListener(unityAction);
            animationEvents.Add(newAnimationEvent);
        }

        public void RemoveEventListener(AnimationEventReference animationEvent, UnityAction unityAction)
        {
            for (int i = 0; i < animationEvents.Count; i++)
            {
                if (animationEvents[i].eventReference == animationEvent)
                {
                    animationEvents[i].OnAnimationEvent.RemoveListener(unityAction);
                    return;
                }
            }
        }
    }
}