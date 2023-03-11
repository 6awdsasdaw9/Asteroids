using Code.Data;
using Code.Player;
using Code.Stats;
using UnityEngine;
using Zenject;

namespace Code.Enemy.BigAsteroids
{
    [RequireComponent(typeof(Rigidbody2D), typeof(BigAsteroidHp))]
    public class BigAsteroid : MonoBehaviour, IEnemy, IDeSpawner
    {
        [SerializeField] private Rigidbody2D _rb;
        private Transform _player;
        private float _speed;
        private Pool _pool;
        public BigAsteroidHp hp { get; private set; }

        [Inject]
        public void Construct(Pool pool, PlayerMove player, GameConfig config)
        {
            _pool = pool;
            _player = player.transform;
            _speed = config.bigAsteroidSpeed;
            hp = GetComponent<BigAsteroidHp>();
        }
        
        public void DeSpawn() => 
            _pool.Despawn(this);

        public void SetPosition(Vector3 position) => 
            transform.position = position;

        public void Move()
        {
            Vector2 direction = _player.position - transform.position;
            transform.up = direction.normalized;
            _rb.AddForce(transform.up * _speed, ForceMode2D.Impulse);
        }

        private Vector3 GetRandomPoint()
        {
            float fieldWidth = Screen.width;
            float fieldHeight = Screen.height;
            var side = Random.Range(0, 4);

            var x = (side == 1) ? fieldWidth : ((side == 3) ? 0 : Random.Range(0, fieldWidth));
            var y = (side == 0) ? fieldHeight : ((side == 2) ? 0 : Random.Range(0, fieldHeight));

            Vector3 position = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 0));
            position.z = 0;
            return position;
        }

        public class Pool : MonoMemoryPool<BigAsteroid>
        {
            protected override void Reinitialize(BigAsteroid asteroid)
            {
                asteroid.hp.ResetHealth();
                asteroid.SetPosition(asteroid.GetRandomPoint());
                asteroid.Move();
            }
        }
    }
}