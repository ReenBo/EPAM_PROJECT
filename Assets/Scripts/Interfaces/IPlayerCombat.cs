using ET.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ET.Interface
{
    public interface IPlayerCombat : ICharacter
    {
        event Action<float, string, int> onWeaponViewChange;
        event Action<int, int> onPlayerStatsViewChange;
        event Action OnShake;
    }
}

