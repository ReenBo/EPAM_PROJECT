using System;

namespace ET.Interface
{
    public interface ILevelSystem
    {
        event Action<float, float, int, int> onExperiencePlayerChange;

        int CurrentLevel { get; set; }
        float CurrentExperience { get; set; }

        void Init<S>(S structur) where S : struct;
        void Subscribe(IEnemy enemy);
    }
}