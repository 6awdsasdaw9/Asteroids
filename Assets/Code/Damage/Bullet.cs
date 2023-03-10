using Code.Data;
using Code.Services;
using UniRx;
using UnityEngine;
using Zenject;

namespace Code
{
    public class Bullet : MonoBehaviour, IDetectable, IDespawer
    {
        [SerializeField] private BulletType _bulletType;
        [SerializeField] private Rigidbody2D _rb;

        private float _speed;
        private Pool _pool;
        
        [Inject]
        private void Construct(DiContainer container, GameConfig config)
        {
            switch (_bulletType)
            {
                case BulletType.playerBullet:
                    default:
                    _speed = config.playerBulletSpeed;
                    _pool = container.ResolveId<Pool>(Constants.PlayerBullet);
                    break;
                case BulletType.playerSuperBullet:
                    _speed = config.playerSuperBulletSpeed;
                    _pool = container.ResolveId<Pool>(Constants.PlayerSuperBullet);
                    break;
                case BulletType.AliensBullet:
                    _speed = config.aliensBulletSpeed;
                    _pool = container.ResolveId<Pool>(Constants.AliensBullet);
                    break;
            }
        }

        private void SpawnBullet(Vector3 position, Vector3 forward)
        {
            transform.position = position;
            _rb.velocity = forward * _speed;
        }

        public void MoveTo(Vector3 to)
        {
            // Направляем пулю к игроку каждый кадр
            Observable.EveryUpdate().Subscribe(__ =>
            {
                // направляем пулю к игроку
                Vector3 direction = to - transform.position;

                // нормализуем направление, чтобы скорость не зависела от расстояния до игрока
                direction.Normalize();

                // двигаем пулю в направлении игрока с определенной скоростью
                _rb.velocity = direction * _speed * Time.deltaTime;
            }).AddTo(this);
        }

        public void OnDetect() =>
            Despawn();

        public void Despawn() =>
            _pool.Despawn(this);

        public class Pool : MonoMemoryPool<Vector3, Vector3, Bullet>
        {
            protected override void Reinitialize(Vector3 position, Vector3 forward, Bullet bullet)
            {
                bullet.SpawnBullet(position, forward);
            }
        }

    }
      
}