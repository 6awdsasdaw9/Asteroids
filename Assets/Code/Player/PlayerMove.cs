using System;
using Code.Services;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Code.Player
{
    [RequireComponent(typeof(Fuel), typeof(Rigidbody2D))]
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private Fuel _fuel;

        [SerializeField, Range(0.5f, 5)] private float _speed = 1;
        [SerializeField, Range(1, 10)] private float _maxSpeed = 2.8f;
        
        private InputController _inputController;
        private IObservable<Unit> _updateObservable;

        [Inject]
        private void Construct(InputController inputController)
        {
            _inputController = inputController;
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
            transform.up = direction.normalized;
        }

        private void Move()
        {
            if (!_fuel.IsEnoughFuel)
                return;

            _fuel.Reduce();
            _rb.AddForce(transform.up * _speed);
            _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, _maxSpeed);
        }


    }
}