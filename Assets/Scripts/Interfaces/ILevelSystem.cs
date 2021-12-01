using System;

namespace ET.Interface
{
    public interface ILevelSystem
    {
        public event Action<float, float, int, int> onExperiencePlayerChange;

        void Init<S>(S structur) where S : struct;
        void Subscribe(IEnemy enemy);
        void CalculateExperiencePlayer(int experience);
    }
}