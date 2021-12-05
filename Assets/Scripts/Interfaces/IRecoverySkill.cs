using System;

namespace ET.Interface
{
    public interface IRecoverySkill : ICommand
    {
        event Action<float, int> onDisplaySkill;
        event Action<float, float> onHealthViewChange;
    }
}
