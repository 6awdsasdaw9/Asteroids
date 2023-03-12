using Code.Interfaces;
using UnityEngine;

namespace Code.Services
{
    public class ScreenWrapper : MonoBehaviour, IDetectable
    {
        private void Wrap()
        {
            Vector3 position = Camera.main.WorldToScreenPoint(transform.position);

            if (position.x > Screen.width)
                position.x %= Screen.width;

            else if (position.x < 0)
                position.x += Screen.width;

            if (position.y > Screen.height)
                position.y %= Screen.height;

            else if (position.y < 0)
                position.y += Screen.height;

            transform.position = Camera.main.ScreenToWorldPoint(position);
        }

        public void OnDetect() => 
            Wrap();
    }
}