using UnityEngine;
using FMODUnity;
using System.Collections.Generic;

namespace Kiadorn.Audio
{
    [CreateAssetMenu(fileName = "SoundEventMapping", menuName = "Audio/SoundEventMapping")]
    public class FMODSoundEventMapping : ScriptableObject
    {
        [System.Serializable]
        public class SoundEvent
        {
            public string soundName;
            public EventReference eventReference;
        }

        public List<SoundEvent> soundEvents = new List<SoundEvent>();

        private Dictionary<string, EventReference> soundEventDictionary;

        private void OnEnable()
        {
            InitializeDictionary();
        }

        private void InitializeDictionary()
        {
            soundEventDictionary = new Dictionary<string, EventReference>();
            foreach (var soundEvent in soundEvents)
            {
                if (!soundEventDictionary.ContainsKey(soundEvent.soundName))
                {
                    soundEventDictionary[soundEvent.soundName] = soundEvent.eventReference;
                }
            }
        }

        public bool TryGetEventReference(string soundName, out EventReference eventReference)
        {
            return soundEventDictionary.TryGetValue(soundName, out eventReference);
        }
    }
}