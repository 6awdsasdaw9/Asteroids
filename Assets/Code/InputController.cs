using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Code
{
    public class InputController : MonoBehaviour
    {
        public Vector2 mousePosition =>Camera.main.ScreenToWorldPoint(Input.mousePosition);
        public Action OnPressMove;

        private void Start()
        {
            this.UpdateAsObservable()
                .Where(_ => Input.GetKey(KeyCode.Space))
                .Subscribe(_ => PressMove());
        }

        private void PressMove() => 
            OnPressMove?.Invoke();
    }
}
