

using System;

namespace Code.Stats
{
    public interface IStat
    {
        event Action OnStatChanged;
        float Current { get; }
        float Max { get; }
        void  Reduce(float value);
        void  Replenish(float value);
    }
}