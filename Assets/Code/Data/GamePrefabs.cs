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
        public UIDisplay hud;
        public GameObject hpIcon;
        
        public PlayerMove player;
        public PlayerBullet playerBullet;
        public PlayerSuperBullet playerSuperBullet;

        public BigAsteroid bigAsteroid;
        public SmallAsteroid smallAsteroid;
        public Object aliensBullet;
    }
}