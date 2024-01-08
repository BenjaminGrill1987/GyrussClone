using UnityEngine;

namespace GyroSpace.Audio
{

    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayer : MonoBehaviour
    {
        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayAudio(AudioClip newClip)
        {
            _audioSource.clip = newClip;
            _audioSource.Play();
        }
    }
}