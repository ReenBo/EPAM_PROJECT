using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ET.Core.Stats;
using ET.Interface;

namespace ET.Core
{
    public class SaveSystem : ISaveSystem
    {
        private IPlayer _player;
        private ILevelSystem _levelSystem;

        public SaveSystem(IPlayer player, ILevelSystem levelSystem, IScenesManager scenesManager)
        {
            _player = player;
            _levelSystem = levelSystem;

            scenesManager.OnGameProgressIsSaved += SaveStats;
            scenesManager.OnGameProgressIsBeingLoaded += UploadSave;
        }

        public void SaveStats()
        {
            SaveGameProgress(_player, _levelSystem);
        }

        public void UploadSave()
        {
            CharacterStats stats = LoadGameProgress();

            _player.CurrentHealth = stats.Health;
            _player.CurrentArmor = stats.Armor;

            _levelSystem.CurrentLevel = stats.Level;
            _levelSystem.CurrentExperience = stats.Experience;

            Vector3 position;
            position.x = stats.PositionPlayer[0];
            position.y = stats.PositionPlayer[1];
            position.z = stats.PositionPlayer[2];

            Transform transform = null;
            transform.position = position;

            _player.SetPosition(transform);
        }

        private void SaveGameProgress(IPlayer player, ILevelSystem levelSystem)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            string path = Application.persistentDataPath + "GameStats.sav";
            Debug.Log(path);

            FileStream stream = new FileStream(path, FileMode.Create);
            Debug.Log(stream);

            CharacterStats stats = new CharacterStats(player, levelSystem);

            binaryFormatter.Serialize(stream, stats);
            stream.Close();
        }

        private CharacterStats LoadGameProgress()
        {
            string path = Application.persistentDataPath + "GameStats.sav";

            if (File.Exists(path))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                CharacterStats stats = binaryFormatter.Deserialize(stream) as CharacterStats;
                stream.Close();

                return stats;
            }
            else
            {
                Debug.LogError($"Error loading from the game file {path}");
                return null;
            }
        }
    }
}
