using System;
using Code.Data;
using Code.Services;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Code.Player
{
    [RequireComponent(typeof(PlayerFuel), typeof(Rigidbody2D))]
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private PlayerFuel playerFuel;

        private float _speed;
        private float _maxSpeed;
        private float  turnSpeed;
        
        private InputController _inputController;
        private IObservable<Unit> _updateObservable;
      

        [Inject]
        private void Construct(InputController inputController,GameConfig config)
        {
            _inputController = inputController;
            _speed = config.playerSpeed;
            _maxSpeed = config.playerMaxSpeed;
            turnSpeed = config.playerTurnSpeed;
        }

        private void Awake()
        {
            _updateObservable = this.UpdateAsObservable().Share();

            _updateObservable
                .Subscribe(_ => TurnForward())
                .AddTo(this);

            _updateObservable
                .Where(_ => _inputController.isPressMove)
                .Subscribe(_ => Move())
                .AddTo(this);
        }

        private void TurnForward()
        {
            Vector2 direction = _inputController.mousePosition - (Vector2)transform.position;
            transform.up = Vector3.Lerp(transform.up,direction.normalized,turnSpeed * Time.deltaTime);
        }

        private void Move()
        {
            if (!playerFuel.IsEnoughFuel)
                return;

            playerFuel.Reduce();
            _rb.AddForce(transform.up * _speed);
            _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, _maxSpeed);
        }
    }
}