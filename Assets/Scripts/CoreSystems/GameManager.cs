using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ET.Enemy;
using ET.Player;
using ET.Core.Stats;
using ET.Core.SaveSystem;
using ET.Weapons;
using System;
using ET.Player.Combat;
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
        public IMainCamera mainCamera = null;
        public IEnemy enemy = null;
        public IResoursManager resoursManager = null;
        public ILevelSystem levelSystem = null;
        public IUIRoot uIRoot = null;
        public IHUD hUD = null;
        public IPopups popups = null;

        #region OLD
        [Header("References to the Components")]
        [SerializeField] private EnemyManager _enemyManager;

        private PlayerController _playerController;
        private PlayerCombatController _playerCombatController;
        private WeaponsController _weaponsController;
        private RecoverySkill _recoverySkill;

        private LevelSystem _levelSystem;
        private CharacterStats _stats;

        public PlayerController PlayerController { get => _playerController; private set => _playerController = value; }
        public PlayerCombatController PlayerCombatController { get => _playerCombatController; }
        public CharacterStats Stats { get => _stats; set => _stats = value; }
        public EnemyManager EnemyManager { get => _enemyManager; }

        public WeaponsController WeaponsController { get => _weaponsController; }
        public RecoverySkill RecoverySkill { get => _recoverySkill; set => _recoverySkill = value; }
        #endregion

        protected void Awake()
        {
            _instance = this;

            mainCamera = GetMainCamera();
            player = GetPlayer();
            uIRoot = GetUIRoot();
            levelSystem = GetLevelSystem();
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
            player = null;
            enemy = null;
            resoursManager = null;
            levelSystem = null;
            uIRoot = null;
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

            //_playerCombatController = _playerController.transform.GetComponent<PlayerCombatController>();
            //_weaponsController = _playerController.transform.GetComponentInChildren<WeaponsController>();
            //_recoverySkill = _playerController.transform.GetComponentInChildren<RecoverySkill>();

            //_camera.GetPlayerPosition(_playerPosition);

            //_enemyManager = Instantiate(_enemyManagerPrefab).GetComponent<EnemyManager>();
            //EnemyManager.GetPlayerPosition(_playerPosition);

            //_levelSystem = new LevelSystem();

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
