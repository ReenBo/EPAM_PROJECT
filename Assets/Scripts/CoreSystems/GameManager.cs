using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ET.Enemy;
using ET.Core.Stats;
using ET.Core.SaveSystem;
using ET.Interface;
using ET.Core;
using ET.Structures;
using ET.Enums.EComponents;
using ET.Enums.Views;

namespace ET
{
    public class GameManager : MonoBehaviour, IApplicationManager
    {
        private static GameManager _instance = null;

        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("GameManager is NULL");
                }

                return _instance;
            }
        }

        private IPlayer player;
        private IMainCamera mainCamera;
        private IEnemyManager _enemyManager;
        private IResoursManager resoursManager;
        private ILevelSystem levelSystem;
        private ICharacterStats characterStats;
        private IUIRoot uIRoot;
        private IHUD hUD;
        private IPopups popups;

        #region OLD
        private CharacterStats _stats; // Need create

        public CharacterStats Stats { get => _stats; set => _stats = value; }
        #endregion

        protected void Awake()
        {
            _instance = this;

            mainCamera = GetMainCamera();
            player = GetPlayer();
            uIRoot = GetUIRoot();
            levelSystem = GetLevelSystem();
            characterStats = GetCharacterStats();
            _enemyManager = GetEnemyManager();
        }

        protected void Start()
        {
        }

        protected void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }

            mainCamera = null;
            _enemyManager = null;
            resoursManager = null;
            levelSystem = null;
            characterStats = null;
            uIRoot = null;
            player = null;
        }

        public IResoursManager GetResourseManager() // maybe static
        {
            if (resoursManager is null)
            {
                resoursManager = new ResoursesManager();
            }

            return resoursManager;
        }

        public IPlayer GetPlayer() // maybe static
        {
            if (player is null)
            {
                resoursManager = GetResourseManager();

                player = resoursManager.CreateObjectInstance<IPlayer, EComponents>(EComponents.Player);
            }

            return player;
        }

        public IMainCamera GetMainCamera() // maybe static
        {
            if (mainCamera is null)
            {
                resoursManager = GetResourseManager();

                mainCamera = resoursManager.CreateObjectInstance<IMainCamera, EComponents>(EComponents.MainCamera);
            }

            return mainCamera;
        }

        public IEnemyManager GetEnemyManager() // maybe static
        {
            if (_enemyManager is null)
            {
                resoursManager = GetResourseManager();

                _enemyManager = resoursManager.CreateObjectInstance<IEnemyManager, EComponents>(EComponents.EnemyManager);
            }

            return _enemyManager;
        }

        public ILevelSystem GetLevelSystem() // maybe static
        {
            if (levelSystem is null)
            {
                levelSystem = new LevelSystem
                    (
                    SLevelSystemData.currentLevel, 
                    SLevelSystemData.currentExperience, 
                    SLevelSystemData.maxExperience,
                    SLevelSystemData.amountExperienceRaiseLevel
                    );
            }

            return levelSystem;
        }

        public ICharacterStats GetCharacterStats() // maybe static
        {
            if (characterStats is null)
            {
                resoursManager = GetResourseManager();

                characterStats = new CharacterStats(player, levelSystem);
            }

            return characterStats;
        }

        public IUIRoot GetUIRoot() // maybe static
        {
            if (uIRoot is null)
            {
                resoursManager = GetResourseManager();

                uIRoot = resoursManager.CreateObjectInstance<IUIRoot, EView>(EView.UIRoot);
            }

            return uIRoot;
        }

        public IHUD GetHUD() // maybe static
        {
            if (hUD is null)
            {
                resoursManager = GetResourseManager();

                hUD = resoursManager.CreateObjectInstance<IHUD, EView>(EView.HUD);
            }

            return hUD;
        }

        public IPopups GetPopups() // maybe static
        {
            if (popups is null)
            {
                resoursManager = GetResourseManager();

                popups = resoursManager.CreateObjectInstance<IPopups, EView>(EView.Popups);
            }

            return popups;
        }

        public void StartSession()
        {
            Debug.Log("Susses StartSession()");
        }

        public void InitGame(IScenesManager scenesManager, ISceneInformation info)
        {
            player.SetPosition(info.PlayerSpawnTarget);
            var playerPosition = player.GetPosition();

            mainCamera.GetPlayerPosition(playerPosition);

            uIRoot.Init(player, levelSystem, scenesManager);

            _enemyManager.Init(playerPosition, info.EnemySpawnTargets);
        }

        public void SaveStats()
        {
            SaveSystem.SaveGame(player, levelSystem);
        }

        public void UploadSave()
        {
            CharacterStats stats = SaveSystem.LoadGame();

            player.CurrentHealth = stats.Health;
            player.CurrentArmor = stats.Armor;

            levelSystem.CurrentLevel = stats.Level;
            levelSystem.CurrentExperience = stats.Experience;

            Vector3 position;
            position.x = stats.PositionPlayer[0];
            position.y = stats.PositionPlayer[1];
            position.z = stats.PositionPlayer[2];

            Transform transform = null;
            transform.position = position;

            player.SetPosition(transform);
        }
    }
}
