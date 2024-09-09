using UnityEngine;
using UnityEngine.Events;

namespace Kiadorn.ScriptableEvents
{
    public class ScriptableEventListener : MonoBehaviour, IScriptableEventListener
    {
        [SerializeField]
        protected ScriptableEvent @event;

        [SerializeField]
        protected UnityEvent response;

        public void OnEnable()
        {
            if (@event != null)
            {
                @event.AddListener(this);
            }
        }

        public void OnDisable()
        {
            if (@event != null)
            {
                @event.RemoveListener(this);
            }
        }

        public void OnEventRaised()
        {
            response?.Invoke();
        }
    }
}