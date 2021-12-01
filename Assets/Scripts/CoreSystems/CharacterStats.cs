using System.Collections.Generic;
using UnityEngine;
using ET;
using ET.Player;
using ET.Weapons;

namespace ET.Core.Stats
{
    [System.Serializable]
    public class CharacterStats
    {
        private float _health;
        private float _armor;
        private int _amountCartridges;

        private int _level;
        private float _experience;

        public float[] PositionPlayer = new float[3];

        public CharacterStats(PlayerController player, LevelSystem progress)
        {
            _health = player.CurrentHealth;
            _armor = player.CurrentArmor;
            //_amountCartridges = weapon.AmmoCounter;

            _level = progress.CurrentLevel;
            _experience = progress.CurrentExperience;

            PositionPlayer[0] = player.transform.position.x;
            PositionPlayer[1] = player.transform.position.y;
            PositionPlayer[2] = player.transform.position.z;
        }

        public float Health { get => _health; set => _health = value; }
        public float Armor { get => _armor; set => _armor = value; }
        //public int AmountCartridges { get => _amountCartridges; set => _amountCartridges = value; }
        public int Level { get => _level; set => _level = value; }
        public float Experience { get => _experience; set => _experience = value; }
    }
}
