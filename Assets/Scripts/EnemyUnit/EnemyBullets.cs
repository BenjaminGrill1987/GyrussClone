using UnityEngine;
using GyroSpace.Interface;

namespace GyroSpace.EnemyUnit
{

    public class EnemyBullets : MonoBehaviour
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _speed;

        private Vector3 _target;
        private Rigidbody2D _rigidbody;


        void FixedUpdate()
        {
            Move();
            LookingAtTarget();

            if (Vector3.Distance(_target, transform.position) <= 0.1f) Destroy(gameObject);
        }
        public void SetPara(Vector3 newTarget)
        {
            _target = newTarget;
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Move()
        {
            Vector3 direction = (_target - transform.position).normalized;

            _rigidbody.velocity = -direction * _speed;
        }
        private void LookingAtTarget()
        {
            Vector3 lookingAtTarget = _target - transform.position;

            float lookAngle = Mathf.Atan2(-lookingAtTarget.y, -lookingAtTarget.x) * Mathf.Rad2Deg - 90;

            Quaternion axis = Quaternion.AngleAxis(lookAngle, Vector3.forward);

            transform.rotation = Quaternion.Slerp(transform.rotation, axis, 1);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<iDamageAble>().DamageTaken(_damage);
            }
            if (!collision.gameObject.CompareTag("Bullet"))
            {
                Destroy(gameObject);
            }
        }
    }
}