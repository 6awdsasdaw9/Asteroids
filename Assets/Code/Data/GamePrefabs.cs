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
        public PlayerMove player;
        public Bullet playerBullet;
        public UIDisplay hud;
        public GameObject hpIcon;

        public BigAsteroid bigAsteroid;
        public SmallAsteroid smallAsteroid;
    }
}