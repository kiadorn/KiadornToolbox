using UnityEngine;

namespace Kiadorn.Utilities
{
    [CreateAssetMenu(fileName = "New Animation Event Reference", menuName = "Kiadorn/Animation Event Reference")]
    public class AnimationEventReference : ScriptableObject
    {
        public string EventName { get => eventName; }

        [SerializeField]
        private string eventName;

        private void OnValidate()
        {
            if (eventName != name)
            {
                eventName = name;
            }
        }
    }
}