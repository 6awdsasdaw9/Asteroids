using ModestTree;
using UnityEngine;

namespace Code.Services
{
    public class CollisionListener : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {

            Debug.Log(col.name);
            if (col.TryGetComponent(out IDetectable obj)) 
                obj.OnTriggerEnter();
        }
    }
}