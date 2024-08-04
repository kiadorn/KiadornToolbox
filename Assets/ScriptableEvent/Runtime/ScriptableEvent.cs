using System.Collections.Generic;
using UnityEngine;

namespace Kiadorn.ScriptableEvents
{
    [CreateAssetMenu(fileName = "ScriptableEvent", menuName = "Kiadorn/Scriptable Event")]
    public class ScriptableEvent : ScriptableObject
    {
        protected List<IScriptableEventListener> listeners = new List<IScriptableEventListener>();

        public void Raise()
        {
            for (int i = 0; i < listeners.Count; i++)
            {
                listeners[i].OnEventRaised();
            }
        }

        public void AddListener(IScriptableEventListener listener)
        {
            listeners.Add(listener);
        }

        public void RemoveListener(IScriptableEventListener listener)
        {
            listeners.Remove(listener);
        }
    }
}