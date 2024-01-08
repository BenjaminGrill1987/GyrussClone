using System.Collections.Generic;
using UnityEngine;

namespace GyroSpace.Audio
{

    [RequireComponent(typeof(AudioSource))]
    public class MediaPlayer : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> _audioClip;

        private AudioSource _audioSource;

        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.clip = _audioClip[Random.Range(0, _audioClip.Count)];
                _audioSource.Play();
            }
        }
    }
}