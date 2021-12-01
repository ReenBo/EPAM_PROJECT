using System;

namespace ET.Interface
{
    public interface IEnemy
    {
        event Action<int> OnExperienceEarned;

        void EnemyIsDying();
    }
}
