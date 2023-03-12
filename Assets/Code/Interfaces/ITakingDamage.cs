using System;
using Code.Data;

namespace Code.Interfaces
{
    public interface ITakingDamage
    {
        void TakeDamage(int damage,DamageOwnerType damageOwner);
    }

    public interface IPlayerHealth : ITakingDamage
    {
        event Action OnStatChanged;
        int Current { get; }
        int Max { get; }
    }
}