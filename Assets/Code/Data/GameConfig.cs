using UnityEngine;

namespace Code.Data
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/Data/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [Header("Player")]
        [Range(0.5f, 5)] public float playerSpeed = 1;
        [Range(1, 10)] public float playerMaxSpeed = 2.8f;
        [Range(1, 5)] public byte playerMaxHP = 3;

        
        [Header("Fuel")]
        public float maxFuel = 10;
        public float fuelForMove = 0.2f;
        public float fuelFromEnemy;
        
        [Header("Bullets")]
        [Range(1, 10)] public float playerBulletSpeed = 9;
        [Range(1, 10)] public float playerSuperBulletSpeed = 9;
        [Range(1, 10)] public float aliensBulletSpeed = 9;
        
        
        [Header("Big Asteroid")]
        [Range(2, 5)] public float asteroidsSpawnCooldown = 5;
        [Range(0.5f, 5)] public float bigAsteroidSpeed = 2;
        [Range(1, 3)]public byte bigAsteroidMaxHP = 1;
        [Range(1, 3)]public byte createSmallAsteroid = 3;
        
        
        [Header("Small Asteroid")]
        [Range(0.5f, 5)] public float smallAsteroidSpeed =1.5f;
        [Range(1, 3)]public byte smallAsteroidMaxHP = 1;

        [Header("Attack")]
        public byte minDamage = 1;
        public byte midDamage = 2;
        public byte maxDamage = 3;
    }
}