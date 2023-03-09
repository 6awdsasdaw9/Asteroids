using UnityEngine;

namespace Code.Services
{
    public class InputController
    {
        public Vector2 mousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);
        public bool isPressMove => Input.GetKey(KeyCode.Space);

        public bool isPressAttack => Input.GetMouseButtonDown(0);
    }
}