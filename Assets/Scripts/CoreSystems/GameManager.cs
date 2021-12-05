using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ET.Enemy;
using ET.Player;
using ET.Core.Stats;
using ET.Core.SaveSystem;
using ET.Weapons;
using System;
using ET.Player.Skills;
using ET.Interface;
using ET.Core;
using ET.Structures;
using ET.UI;
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

        public IPlayer player = null;
        public IPlayerCombat playerCombat = null;
        public IMainCamera mainCamera = null;
        public IEnemy enemy = null;
        public IResoursManager resoursManager = null;
        public ILevelSystem levelSystem = null;
        public ICharacterStats characterStats = null;
        public IUIRoot uIRoot = null;
        public IHUD hUD = null;
        public IPopups popups = null;

        #region OLD
        [Header("References to the Components")]
        [SerializeField] private EnemyManager _enemyManager;

        private PlayerController _playerController;
        private LevelSystem _levelSystem;
        private CharacterStats _stats;

        public PlayerController PlayerController { get => _playerController; private set => _playerController = value; }
        public CharacterStats Stats { get => _stats; set => _stats = value; }
        public EnemyManager EnemyManager { get => _enemyManager; }
        #endregion

        protected void Awake()
        {
            _instance = this;

            mainCamera = GetMainCamera();
            player = GetPlayer();
            uIRoot = GetUIRoot();
            levelSystem = GetLevelSystem();
            characterStats = GetCharacterStats();
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
            enemy = null;
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

        public IEnumerator InitGame(ISceneInformation info)
        {
            player.SetPosition(info.PlayerSpawnTarget);

            mainCamera.GetPlayerPosition(player.GetPosition());

            uIRoot.Init(player, levelSystem);

            //_enemyManager = Instantiate(_enemyManagerPrefab).GetComponent<EnemyManager>();
            //EnemyManager.GetPlayerPosition(_playerPosition);

            yield return null;
        }

        public void SaveStats()
        {
            SaveSystem.SaveGame(_playerController, _levelSystem);
        }

        public void UploadSave()
        {
            CharacterStats stats = SaveSystem.LoadGame();

            _playerController.CurrentHealth = stats.Health;
            _playerController.CurrentArmor = stats.Armor;
            //_weaponsController.AmmoCounter = stats.AmountCartridges;

            _levelSystem.CurrentLevel = stats.Level;
            _levelSystem.CurrentExperience = stats.Experience;

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
