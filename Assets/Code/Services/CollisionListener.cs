using ModestTree;
using UnityEngine;

namespace Code.Services
{
    public class CollisionListener : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out IDetectable obj)) 
                obj.OnDetect();
        }
    }
}