using UnityEngine;

namespace Code.Data
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/Data/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        public int playerBulletPoolSize = 20;
        public int enemyBulletPoolSize = 10;
        
    }
}