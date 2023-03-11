using Code.Data;
using Code.Player;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Code.Enemy.Aliens
{
    [RequireComponent(typeof(Rigidbody2D),typeof(AlienHp))]
    public class Alien : MonoBehaviour, IEnemy, IDeSpawner
    {
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private AlienHp _hp;
     
        private Transform _player;
        private float _speed;
        
        private DiContainer _container;
        private EnemiesFabric _factory;
        
        [Inject]
        private void Construct(DiContainer container, PlayerMove player, GameConfig config)
        {
            _container = container;
            _player = player.transform;
            _speed = config.aliensSpeed;
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void Move()
        {
            Observable.EveryFixedUpdate().Subscribe(__ =>
            {
                Vector3 direction = _player.position - transform.position;
                direction.Normalize();
                _rb.velocity = direction * _speed;
            }).AddTo(this);
        }

        public void DeSpawn()
        {
            if (_factory == null) 
                _factory = _container.Resolve<EnemiesFabric>();

            _factory.DeSpawnAlien(this);
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

        public class Pool : MonoMemoryPool<Alien>
        {
            protected override void OnCreated(Alien alien)
            {
                alien.Move();
                base.OnCreated(alien);
            }

            protected override void Reinitialize(Alien alien)
            {
                alien._hp.ResetHealth();
                alien.SetPosition(alien.GetRandomPoint());
            }
        }
    }
}