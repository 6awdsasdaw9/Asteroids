using Code.Data;
using Code.Player;
using UnityEngine;

namespace Code.Enemy
{
    public interface IEnemy
    {
        void SetPosition(Vector3 position);
        void Move();


    }
}