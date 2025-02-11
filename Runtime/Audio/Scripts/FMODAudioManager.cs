using FMOD.Studio;
using FMODUnity;
using System.Collections.Generic;
using UnityEngine;

namespace Kiadorn.Audio
{
    public class FMODAudioManager : MonoBehaviour, IAudioManager
    {
        private readonly Dictionary<string, EventInstance> soundEvents = new Dictionary<string, EventInstance>();

        [SerializeField]
        private FMODSoundEventMapping soundEventMapping;

        public void PlaySound(string soundName, Vector3 position)
        {
            if (string.IsNullOrEmpty(soundName))
            {
                return;
            }

            if (!soundEventMapping.TryGetEventReference(soundName, out EventReference eventReference))
            {
                Debug.LogWarning($"Sound with name {soundName} not found in sound event mapping.");
                return;
            }
            EventInstance soundEvent = RuntimeManager.CreateInstance(eventReference);
            soundEvent.set3DAttributes(RuntimeUtils.To3DAttributes(position));
            soundEvent.start();
            soundEvent.release();

            soundEvents[soundName] = soundEvent;
        }

        public void StopSound(string soundName)
        {
            if (soundEvents.TryGetValue(soundName, out EventInstance soundEvent))
            {
                soundEvent.getPlaybackState(out PLAYBACK_STATE playbackState);

                if (playbackState == PLAYBACK_STATE.PLAYING)
                {
                    soundEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                }

                soundEvent.release();
                soundEvents.Remove(soundName);
            }
            else
            {
                Debug.LogWarning($"Sound with name {soundName} is not currently playing.");
            }
        }
    }
}