using ET.UI.WindowTypes;
using System;
using UnityEngine;

namespace ET.Interface
{
    public interface IPlayer
    {
        event Action<float, int> onArmorViewChange;
        event Action<float, int> onHealthViewChange;
        event Action<WindowType> onPlayerDied;

        Transform PlayerPosition { get; }

        Transform GetPosition();
        void SetPosition(Transform target);
        void Damage(float amount);
        void PlayerIsDying();
    }
}
