using UnityEngine;
using GyroSpace.Interface;
using GyroSpace.Audio;
using GyroSpace.Utility;
using GyroSpace.Input;

namespace GyroSpace.Player
{

    public class PlayerController : MonoBehaviour, iDamageAble
    {
        [SerializeField] private Transform _target, _bulletSpawner;
        [SerializeField] private float _radius, _movementSpeed;
        [SerializeField] private GameObject _bullet;
        [SerializeField] private int _health;
        [SerializeField] private AudioPlayer _audioPlayer;
        [SerializeField] private AudioClip _audioClip;
        public PlayerInput _playerInput;

        private Vector3 _positionOffset;
        private float _angle;

        private void Start()
        {
            _playerInput = new PlayerInput();
            _playerInput.Enable();
        }

        private void Update()
        {
            if (GameState._CurrentState == Gamestates.Game)
            {
                if (IsShooting())
                    Shoot();
            }
        }

        private void LateUpdate()
        {
            if (GameState._CurrentState == Gamestates.Game)
            {
                Move();
                LookingAtTarget();
            }
        }

        private void Move()
        {
            _positionOffset.Set(Mathf.Cos(_angle) * _radius, Mathf.Sin(_angle) * _radius, 0);

            transform.position = _target.position + _positionOffset;

            _angle += Time.deltaTime * MoveInput().x * _movementSpeed;
        }

        private void LookingAtTarget()
        {
            var lookingAtTarget = _target.position - transform.position;
            var lookAngle = Mathf.Atan2(lookingAtTarget.y, lookingAtTarget.x) * Mathf.Rad2Deg - 90;
            var axis = Quaternion.AngleAxis(lookAngle, Vector3.forward);

            transform.rotation = Quaternion.Slerp(transform.rotation, axis, 1);
        }

        private Vector2 MoveInput() => _playerInput.Player.Movement.ReadValue<Vector2>();

        private void Shoot()
        {
            _audioPlayer.PlayAudio(_audioClip);
            var bul = Instantiate(_bullet, _bulletSpawner.position, Quaternion.identity);
            bul.GetComponent<Bullet>().SetPara((_target.position - transform.position).normalized, "Enemy", _audioPlayer);
        }

        public void DamageTaken(int damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                Death();
            }
        }

        public void Death()
        {
            GameState.TryToChange(Gamestates.GameOver);
            Destroy(gameObject);
        }

        private bool IsShooting()
        {
            return _playerInput.Player.Shoot.WasPerformedThisFrame();
        }
    }
}