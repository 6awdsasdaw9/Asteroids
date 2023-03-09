

using System;

namespace Code.Stats
{
    public interface IHealth
    {
        event Action OnStatChanged;
        byte Current { get; }
        byte Max { get; }
        void  TakeDamage();

    }
}