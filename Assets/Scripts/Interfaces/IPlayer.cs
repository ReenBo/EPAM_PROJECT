using ET.Player;
using ET.Player.Skills;
using ET.UI.WindowTypes;
using System;
using UnityEngine;

namespace ET.Interface
{
    public interface IPlayer : ICharacter
    {
        event Action<float, int> onArmorViewChange;
        event Action<float, int> onHealthViewChange;
        event Action<WindowType> onPlayerDied;

        Transform PlayerPosition { get; }
        PlayerCombatController PlayerCombat { get; }
        PlayerSkillsController PlayerSkills { get; }
        RecoverySkill RecoverySkill { get; }

        Transform GetPosition();
        void SetPosition(Transform target);
        void Damage(float amount);
        void PlayerIsDying();
    }
}
