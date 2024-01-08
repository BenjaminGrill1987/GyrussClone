using GyroSpace.Audio;
using GyroSpace.EnemyUnit;
using GyroSpace.Utility;
using UnityEngine;

namespace GyroSpace.Level
{

    public class Spawner : MonoBehaviour
    {
        [Header("Spawning enemies")]
        [SerializeField] int _min, _max;
        [SerializeField] GameObject _enemy;
        [Header("Time for new Enemy Group")]
        [SerializeField] float _time;
        [SerializeField] AudioPlayer _audioPlayer;

        private Timer _timer;

        void Start()
        {
            _timer = new Timer(_time);
        }

        void FixedUpdate()
        {
            if (!RoundHandler.IsRoundFinished())
            {
                _timer.Tick();
                if (_timer._CurrentTime <= 0)
                {
                    SpawnEnemy();
                    _timer.Reset();
                }
            }
        }

        private void SpawnEnemy()
        {
            RoundHandler.CountRoundUp();
            var numberOfEnemies = Random.Range(_min, _max + 1);
            var angle = 360f / numberOfEnemies;
            var tempAngle = 0f;

            for (int index = 0; index < numberOfEnemies; index++)
            {
                var newEnemy = Instantiate(_enemy, transform.root.position, Quaternion.identity);
                newEnemy.GetComponent<Enemy>().SetPara(transform.root.position, tempAngle, 0);
                newEnemy.GetComponent<Enemy>().GetAudioPlayer(_audioPlayer);
                tempAngle += angle;
            }
        }
    }
}