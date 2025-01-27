using UnityEngine;

namespace Kiadorn.Audio
{
    public interface IAudioManager
    {
        void PlaySound(string soundName, Vector3 position);

        void StopSound(string soundName);
    }
}