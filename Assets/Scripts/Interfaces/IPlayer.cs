using ET.UI.WindowTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET.Interface
{
    public interface IPlayer
    {
        event Action<float, int> onArmorViewChange;
        event Action<float, int> onHealthViewChange;
        event Action<WindowType> onPlayerDied;

        void Damage(float amount);
        void PlayerIsDying();
    }
}
