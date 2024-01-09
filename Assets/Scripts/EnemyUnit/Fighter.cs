using GyroSpace.Audio;
using GyroSpace.Interface;
using GyroSpace.Player;
using GyroSpace.Utility;
using UnityEngine;

namespace GyroSpace.EnemyUnit
{

    public class Fighter : Enemy
    {
        [SerializeField] private Transform _bulletSpawner;

        void FixedUpdate()
        {
            if (GameState._CurrentState == Gamestates.Game)
            {
                _timer.Tick();
                _attackTimer.Tick();

                Move();
                LookingAtTarget();

                if (_timer._CurrentTime <= 0 && _radius < _maxRadius || _radius < 1) RaiseDiameter();

                if (_attackTimer._CurrentTime <= 0) Attack();

                if (!_col.enabled && _radius >= 1) _col.enabled = true;
            }
            else if (GameState._CurrentState == Gamestates.TestScene)
            {
                _attackTimer.Tick();

                if (_attackTimer._CurrentTime <= 0) Attack();
            }
        }

        protected override void Attack()
        {
            GameObject bullet = Instantiate(_rocket, _bulletSpawner.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().SetPara(_bulletSpawner.up, "Player");
            AudioPlayer.PlayAudio(_audioClip);
            _attackTimer.Reset();
        }
    }
}