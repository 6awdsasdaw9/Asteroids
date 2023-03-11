using UnityEngine;

namespace Code.Enemy.Aliens
{
    public class Alien : MonoBehaviour, IEnemy, IDeSpawner
    {
        public void SetPosition(Vector3 position)
        {
            throw new System.NotImplementedException();
        }

        public void Move()
        {
            throw new System.NotImplementedException();
        }

        public void DeSpawn()
        {
            throw new System.NotImplementedException();
        }
    }
}