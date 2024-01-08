using GyroSpace.Interface;
using GyroSpace.Utility;

namespace GyroSpace.EnemyUnit
{

    public class BaseEnemy : Enemy
    {
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
        }
    }
}