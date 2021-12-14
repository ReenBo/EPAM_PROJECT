using ET.Player;
using ET.Player.Skills;
using ET.UI.WindowTypes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET.Interface
{
    public interface IPlayer : ICharacter
    {
        event Action<float, int> onArmorViewChange;
        event Action<float, int> onHealthViewChange;
        event Action<WindowType> onOpenWindow;
        event Action<WindowType> onCloseWindow;

        Transform PlayerPosition { get; }
        InputSystem InputSystem { get; }
        PlayerCombatController PlayerCombat { get; }
        PlayerSkillsController PlayerSkills { get; }

        List<GameObject> KeyCards { get; }

        float CurrentHealth { get; set; }
        float CurrentArmor { get; set; }

        Transform GetPosition();
        void SetPosition(Transform target);
        void Damage(float amount);
        void PlayerIsDying();
    }
}
