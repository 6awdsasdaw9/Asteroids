using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Code.Player
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private InputController inputController;
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField,Range(0.5f,5)] private float _speed = 1;
        [SerializeField,Range(1,10)] private float _maxSpeed = 2.8f;

        private IObservable<Unit> _updateObservable;
        private void OnEnable()
        {
            inputController.OnPressMove += MoveToForward;
        
            _updateObservable = this.UpdateAsObservable().Share();
        
            _updateObservable
                .Subscribe(_ => TurnForward())
                .AddTo(this);
            _updateObservable
                .Subscribe(_ => ScreenWrap())
                .AddTo(this);
        }

        private void OnDisable()
        {
            inputController.OnPressMove -= MoveToForward;
        }
    
        private void TurnForward()
        {
            Vector2 direction = inputController.mousePosition - (Vector2)transform.position;
            transform.up = direction.normalized;
        }

        private void Acceleration()
        {
            _rb.velocity = new Vector2(Mathf.Clamp(_rb.velocity.x, -_maxSpeed, _maxSpeed),
                Mathf.Clamp(_rb.velocity.y, -_maxSpeed, _maxSpeed));
        }
    
        private void MoveToForward()
        {
            _rb.AddForce(transform.up * _speed);
            _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, _maxSpeed);
        }
        private void ScreenWrap()
        {
            Vector3 position = Camera.main.WorldToScreenPoint(transform.position);

            if (position.x > Screen.width)
            {
                position.x %= Screen.width;
            }
            else if (position.x < 0)
            {
                position.x += Screen.width;
            }

            if (position.y > Screen.height)
            {
                position.y %= Screen.height;
            }
            else if (position.y < 0)
            {
                position.y += Screen.height;
            }

            transform.position = Camera.main.ScreenToWorldPoint(position);
        }
    }
}