using UnityEngine;

namespace Code.Data
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/Data/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [Header("Bullets pool size")]
        public int playerBulletPoolSize = 5;
        public int playerSuperBulletPoolSize = 3;
        public int aliensPoolSize = 3;
        
        [Header("Enemies pool size")]
        public int bigAsteroidsPoolSize = 10;
        public int smallAsteroidsPoolSize = 20;
    }
}