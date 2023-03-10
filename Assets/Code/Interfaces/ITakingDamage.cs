using System;

namespace Code.Stats
{
    public interface ITakingDamage
    {
        void  TakeDamage();
    }

    public interface IPlayerHealth : ITakingDamage
    {
        event Action OnStatChanged;
        byte Current { get; }
        byte Max { get; }
    }
}