using Code.Data;
using Code.Player;
using UnityEngine;
using Zenject;


namespace Code.Enemy.SmallAsteroid
{
    public class SmallAsteroid : MonoBehaviour,IEnemy
    {
        [SerializeField] private Rigidbody2D _rb;
        private float _speed;
        private Pool _pool;

        [Inject]
        public void Construct(Pool pool, GameConfig config)
        {
            _pool = pool;
            _speed = config.smallAsteroidSpeed;
        }

        public void Despawn() => 
            _pool.Despawn(this);

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void Move()
        {
            Vector3 randomVector = new Vector3(Random.Range(-1, 1),Random.Range(-1, 1),0).normalized;
            _rb.AddForce(randomVector * _speed, ForceMode2D.Impulse);
        }

        public class Pool : MonoMemoryPool<Vector3,SmallAsteroid>
        {
            protected override void Reinitialize(Vector3 position,SmallAsteroid asteroid)
            {
                asteroid.SetPosition(position);
                asteroid.Move();
            }
        }
    }
}