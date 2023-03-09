using Code.Services;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Code.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        private InputController _inputController;
        private Bullet.Pool _bulletPool;

        [Inject]
        private void Construct(InputController inputController, Bullet.Pool bulletPool)
        {
            _inputController = inputController;
            _bulletPool = bulletPool;
        }

        private void Awake()
        {
            this.UpdateAsObservable()
                .Where(_ => _inputController.isPressAttack)
                .Subscribe(_ => Fire())
                .AddTo(this);
        }
        
        private void Fire() => 
            _bulletPool.Spawn(transform.position, transform.up);
    }
}