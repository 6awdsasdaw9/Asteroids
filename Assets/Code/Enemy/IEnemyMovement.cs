using Code.Data;
using Code.Player;

namespace Code.Enemy
{
    public interface IEnemyMovement
    {
        void Construct(PlayerMove player, GameConfig config);
        void SetPosition();
        void Move();
    }
}