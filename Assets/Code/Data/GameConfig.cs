using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Code.Data
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/Data/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [Header("Player")] [Range(0.5f, 5)] public float playerSpeed = 1;
        [Range(1, 10)] public float playerMaxSpeed = 2.8f;
        [Range(1, 5)] public int playerMaxHP = 3;


        [Header("Fuel")] public float maxFuel = 10;
        public float fuelForMove = 0.2f;
        public float fuelFromEnemy;

        [Header("Bullets")] [Range(1, 10)] public float playerBulletSpeed = 9;
        [Range(1, 10)] public float playerSuperBulletSpeed = 9;
        [Range(1, 10)] public float aliensBulletSpeed = 9;


        [Header("Big Asteroid")] [Range(2, 15)] public float asteroidsSpawnCooldown = 5;
        [Range(0.5f, 5)] public float bigAsteroidSpeed = 2;
        [Range(1, 3)] public int bigAsteroidMaxHP = 1;
        [Range(1, 3)] public int createSmallAsteroid = 3;


        [Header("Small Asteroid")] [Range(0.5f, 5)]
        public float smallAsteroidSpeed = 1.5f;

        [Range(1, 3)] public int smallAsteroidMaxHP = 1;

        [Header("Attack")] 
        public List<DamageConfig> DamageConfig;
    }

    [Serializable]
    public class DamageConfig
    {
        public DamageOwnerType DamageOwner;
        public int Damage;
    }

    public enum DamageOwnerType
    {
        None,
        PlayerBullet,
        PlayerSuperBullet,
        Aliens,
        AliensBullet,
        Asteroid
    }



}