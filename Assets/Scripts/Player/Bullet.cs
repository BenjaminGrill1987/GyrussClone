using UnityEngine;
using GyroSpace.Audio;
using GyroSpace.Interface;
using GyroSpace.Utility;

namespace GyroSpace.Player
{

    public class Bullet : MonoBehaviour
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _speed;
        [SerializeField] private AudioClip _audioClip;

        private Vector3 _target, _startPosition;
        private Rigidbody2D _rigidbody;
        private string _targetTag;
        private AudioPlayer _audioPlayer;

        void FixedUpdate()
        {
            if (GameState._CurrentState == Gamestates.Game)
            {
                if (Vector3.Distance(_startPosition, transform.position) >= 3.5f) Destroy(gameObject);
            }
            else
            {
                _rigidbody.velocity = Vector3.zero;
            }
        }

        public void SetPara(Vector3 newTarget, string newTag, AudioPlayer newAudioPlayer)
        {
            _target = newTarget;
            _targetTag = newTag;
            _startPosition = transform.position;
            _rigidbody = GetComponent<Rigidbody2D>();
            _audioPlayer = newAudioPlayer;

            Move();
            LookingAtTarget();
        }

        private void Move()
        {
            _rigidbody.velocity = _target * _speed;
        }
        private void LookingAtTarget()
        {
            float lookAngle = Mathf.Atan2(_target.y, _target.x) * Mathf.Rad2Deg - 90;

            Quaternion axis = Quaternion.AngleAxis(lookAngle, Vector3.forward);

            transform.rotation = Quaternion.Slerp(transform.rotation, axis, 1);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(_targetTag))
            {
                collision.gameObject.GetComponent<iDamageAble>().DamageTaken(_damage);
                _audioPlayer.PlayAudio(_audioClip);
                Destroy(gameObject);
            }
        }
    }
}