using Code.Enemy;
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
        private BulletsFabric _bulletsFactory;

       [Inject]
        private void Construct(InputController inputController, BulletsFabric bulletsFactory)
        {
            _inputController = inputController;
            _bulletsFactory = bulletsFactory;
        }

        private void Awake()
        {
            this.UpdateAsObservable()
                .Where(_ => _inputController.isPressAttack)
                .Subscribe(_ => Fire())
                .AddTo(this);
        }
        
        private void Fire() => 
            _bulletsFactory.SpawnPlayerBullet(transform.position, transform.up);
    }
}