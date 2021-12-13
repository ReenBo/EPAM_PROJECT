using ET.Interface;

namespace ET.Core.Stats
{
    [System.Serializable]
    public class CharacterStats : ICharacterStats
    {
        private float _health;
        private float _armor;
        private int _amountCartridges;

        private int _level;
        private float _experience;

        public float[] PositionPlayer = new float[3];

        public CharacterStats(IPlayer player, ILevelSystem progress)
        {
            _health = player.CurrentHealth;
            _armor = player.CurrentArmor;

            _level = progress.CurrentLevel;
            _experience = progress.CurrentExperience;

            PositionPlayer[0] = player.PlayerPosition.position.x;
            PositionPlayer[1] = player.PlayerPosition.position.y;
            PositionPlayer[2] = player.PlayerPosition.position.z;
        }

        public float Health { get => _health; set => _health = value; }
        public float Armor { get => _armor; set => _armor = value; }
        public int Level { get => _level; set => _level = value; }
        public float Experience { get => _experience; set => _experience = value; }
    }
}
