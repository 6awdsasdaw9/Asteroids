using Code.Data;
using Code.Player;
using UnityEngine;
using Zenject;

namespace Code.Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyMovement : MonoBehaviour, IEnemyMovement
    {
        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private Rigidbody2D _rb;
        private Transform _player;
        private float _speed;

        [Inject]
        public void Construct(PlayerMove player, GameConfig config)
        {
            _player = player.transform;
            _speed = config.enemySpeed;
        }

        public void SetPosition()
        {
            float fieldWidth = Screen.width;
            float fieldHeight = Screen.height;
            var side = Random.Range(0, 4);

            var x = (side == 1) ? fieldWidth : ((side == 3) ? 0 : Random.Range(0, fieldWidth));
            var y = (side == 0) ? fieldHeight : ((side == 2) ? 0 : Random.Range(0, fieldHeight));

            Vector3 randomPoint = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 0));
            randomPoint.z = 0;
            transform.position = randomPoint;
        }

        public void Move()
        {
            Vector2 direction = _player.position - transform.position;
            transform.up = direction.normalized;
            _rb.AddForce(transform.up * _speed, ForceMode2D.Impulse);
        }

        public class Factory : PlaceholderFactory<EnemyMovement>
        {
        }
    }
}