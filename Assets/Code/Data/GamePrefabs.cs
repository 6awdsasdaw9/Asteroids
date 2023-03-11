using Code.Enemy.Aliens;
using Code.Enemy.BigAsteroids;
using Code.Enemy.SmallAsteroids;
using Code.Player;
using Code.UI;
using UnityEngine;

namespace Code.Data
{
    [CreateAssetMenu(fileName = "GamePrefabs", menuName = "ScriptableObjects/Data/GamePrefabs")]
    public class GamePrefabs : ScriptableObject
    {
        [Header("UI")]
        public UIDisplay hud;
        public GameObject hpIcon;
        
        [Space, Header("Player")]
        public PlayerMove player;
        public PlayerBullet playerBullet;
        public PlayerSuperBullet playerSuperBullet;

        [Space, Header("Enemy")]
        public BigAsteroid bigAsteroid;
        public SmallAsteroid smallAsteroid;
        public Alien alien;
    }
}