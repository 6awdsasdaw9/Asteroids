using UnityEngine;

namespace Code.Data
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/Data/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        public float maxFuel = 10;
        public float fuelForMove = 0.2f;
        public float fuelFromEnemy;
    }
}