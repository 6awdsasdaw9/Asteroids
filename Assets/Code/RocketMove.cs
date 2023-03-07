using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class RocketMove : MonoBehaviour
{
    [SerializeField] private InputController inputController;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] float _speed = 5;
    [SerializeField] float _maxSpeed = 15;

    private void OnEnable()
    {
        inputController.OnPressMove += MoveToForward;
        this.UpdateAsObservable().Subscribe(_ => TurnForward());
        this.UpdateAsObservable().Subscribe(_ => ScreenWrap());
        this.UpdateAsObservable().Subscribe(_ => Acceleration());
    }

    private void OnDisable()
    {
        inputController.OnPressMove -= MoveToForward;
    }
    
    private void TurnForward()
    {
        var direction = inputController.mousePosition - (Vector2)transform.position;
        transform.up = direction;
    }

    private void Acceleration()
    {
        if (_rb.velocity.y > _maxSpeed)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _maxSpeed);
        }
        if (_rb.velocity.x > _maxSpeed)
        {
            _rb.velocity = new Vector2( _maxSpeed,_rb.velocity.y);
        }
    }
    private void MoveToForward()
    {
        _rb.AddForce(transform.up.normalized * _speed);
    }

    private void ScreenWrap()
    {
        Vector3 position = Camera.main.WorldToScreenPoint(transform.position);
        if (position.x > Screen.width || position.x < 0)
        {
            position.x = position.x > Screen.width ? 0 : Screen.width;
            transform.position = Camera.main.ScreenToWorldPoint(position);
        }

        if (position.y > Screen.height || position.y < 0)
        {
            position.y = position.y > Screen.height ? 0 : Screen.height;
            transform.position = Camera.main.ScreenToWorldPoint(position);
        }

    }
}