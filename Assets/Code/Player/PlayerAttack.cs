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
        private BulletsFactory _bulletsFactory;

       [Inject]
        private void Construct(InputController inputController, BulletsFactory bulletsFactory)
        {
            _inputController = inputController;
            _bulletsFactory = bulletsFactory;
        }

        private void Awake()
        {
            this.UpdateAsObservable()
                .Where(_ => _inputController.isPressAttack)
                .Subscribe(_ => Attack())
                .AddTo(this);
            
            this.UpdateAsObservable()
                .Where(_ => _inputController.isPressSuperAttack)
                .Subscribe(_ => SuperAttack())
                .AddTo(this);
        }

        private void Attack() => 
            _bulletsFactory.SpawnPlayerBullet(transform.position, transform.up);

        private void SuperAttack() => 
            _bulletsFactory.SpawnPlayerSuperBullet(transform.position, transform.up);
    }
}