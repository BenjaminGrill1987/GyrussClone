using GyroSpace.Utility;
using UnityEngine;

namespace GyroSpace.Audio
{

    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayer : Singleton<AudioPlayer>
    {
        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public static void PlayAudio(AudioClip newClip)
        {
            _Instance._audioSource.clip = newClip;
            _Instance._audioSource.Play();
        }
    }
}