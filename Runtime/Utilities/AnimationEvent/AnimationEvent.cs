using System;
using UnityEngine.Events;

namespace Kiadorn.Utilities
{
    [Serializable]
    public class AnimationEvent
    {
        public AnimationEventReference eventReference;
        public UnityEvent OnAnimationEvent;
    }
}