using Code.Data;
using Code.Services;
using UniRx;
using UnityEngine;
using Zenject;

namespace Code
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb;
        protected float speed;
        
        protected void SpawnBullet(Vector3 position, Vector3 forward)
        {
            transform.position = position;
            _rb.velocity = forward * speed;
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
                _rb.velocity = direction * speed * Time.deltaTime;
            }).AddTo(this);
        }
    }
}