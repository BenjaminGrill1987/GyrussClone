using GyroSpace.Interface;
using GyroSpace.Level;
using GyroSpace.UI;
using GyroSpace.Utility;
using UnityEngine;

namespace GyroSpace.EnemyUnit
{

    public abstract class Enemy : MonoBehaviour, iDamageAble
    {
        [SerializeField] int _health, _timeToMoveOn;
        [SerializeField] protected float _movementSpeed, _maxRadius, _attackTime;
        [SerializeField] protected GameObject _rocket;
        [SerializeField] protected AudioClip _audioClip;

        protected Vector3 _target;
        protected Vector3 _positionOffset;
        protected float _angle, _radius, _tempRadius;
        protected Timer _timer, _attackTimer;
        protected CapsuleCollider2D _col;
        protected int _direction;
        protected bool _directionIsChanged = false;

        protected virtual void Start()
        {
            if (GameState._CurrentState == Gamestates.Game)
            {
                _direction = 1;
                RoundHandler.AddShip(gameObject);
                _timer = new Timer(_timeToMoveOn);
                _attackTimer = new Timer(_attackTime);
                _col = GetComponent<CapsuleCollider2D>();
            }
            else if (GameState._CurrentState == Gamestates.TestScene)
            {
                _attackTimer = new Timer(_attackTime);
            }
        }

        public void SetPara(Vector3 newTarget, float newAngle, float newRadius)
        {
            _target = newTarget;
            _angle = newAngle;
            _radius = newRadius;
            _tempRadius = newRadius + 1;
        }

        protected void Move()
        {
            _positionOffset.Set(Mathf.Cos(_angle) * _radius, Mathf.Sin(_angle) * _radius, 0);

            transform.position = _target + _positionOffset;

            _angle += Time.deltaTime * _movementSpeed * _direction;
        }

        protected void RaiseDiameter()
        {
            if (!_directionIsChanged)
            {
                ChangeDirection();
                _directionIsChanged = true;
            }

            _radius += Time.fixedDeltaTime;

            if (_radius > _maxRadius) _radius = _maxRadius;

            if (_radius >= _tempRadius)
            {
                _directionIsChanged = false;
                _tempRadius += 1;
                _timer.Reset();
            }
        }

        protected void LookingAtTarget()
        {
            Vector3 lookingAtTarget = (_target - transform.position).normalized;

            float lookAngle = Mathf.Atan2(-lookingAtTarget.y, -lookingAtTarget.x) * Mathf.Rad2Deg - 90;

            Quaternion axis = Quaternion.AngleAxis(lookAngle, Vector3.forward);

            transform.rotation = Quaternion.Slerp(transform.rotation, axis, 1);
        }

        public void DamageTaken(int damage)
        {
            _health -= damage;
            if (_health <= 0) Death();
        }

        public void Death()
        {
            RoundHandler.SubShip(gameObject);
            ScoreHandler.AddScore(Mathf.RoundToInt(4 - Vector3.Distance(_target, transform.position)));
            Destroy(gameObject);
        }

        protected abstract void Attack();

        private void ChangeDirection() => _direction *= -1;
    }
}