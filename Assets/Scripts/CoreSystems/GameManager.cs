using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ET.Enemy;
using ET.Core.Stats;
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

        private IPlayer _player;
        private IMainCamera _mainCamera;
        private IEnemyManager _enemyManager;
        private IResoursManager _resoursManager;
        private ILevelSystem _levelSystem;

        private IUIRoot _uIRoot;
        private IHUD _hUD;
        private IPopups _popups;

        private ISaveSystem _saveSystem;

        protected void Awake()
        {
            _instance = this;

            _mainCamera = GetMainCamera();
            _player = GetPlayer();
            _uIRoot = GetUIRoot();
            _levelSystem = GetLevelSystem();
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

            _mainCamera = null;
            _enemyManager = null;
            _resoursManager = null;
            _levelSystem = null;
            _uIRoot = null;
            _player = null;
        }

        public IResoursManager GetResourseManager() // maybe static
        {
            if (_resoursManager is null)
            {
                _resoursManager = new ResoursesManager();
            }

            return _resoursManager;
        }

        public IPlayer GetPlayer() // maybe static
        {
            if (_player is null)
            {
                _resoursManager = GetResourseManager();

                _player = _resoursManager.CreateObjectInstance<IPlayer, EComponents>(EComponents.Player);
            }

            return _player;
        }

        public IMainCamera GetMainCamera() // maybe static
        {
            if (_mainCamera is null)
            {
                _resoursManager = GetResourseManager();

                _mainCamera = _resoursManager.CreateObjectInstance<IMainCamera, EComponents>(EComponents.MainCamera);
            }

            return _mainCamera;
        }

        public IEnemyManager GetEnemyManager() // maybe static
        {
            if (_enemyManager is null)
            {
                _resoursManager = GetResourseManager();

                _enemyManager = _resoursManager.CreateObjectInstance<IEnemyManager, EComponents>(EComponents.EnemyManager);
            }

            return _enemyManager;
        }

        public ILevelSystem GetLevelSystem() // maybe static
        {
            if (_levelSystem is null)
            {
                _levelSystem = new LevelSystem
                    (
                    SLevelSystemData.currentLevel, 
                    SLevelSystemData.currentExperience, 
                    SLevelSystemData.maxExperience,
                    SLevelSystemData.amountExperienceRaiseLevel
                    );
            }

            return _levelSystem;
        }

        public IUIRoot GetUIRoot() // maybe static
        {
            if (_uIRoot is null)
            {
                _resoursManager = GetResourseManager();

                _uIRoot = _resoursManager.CreateObjectInstance<IUIRoot, EView>(EView.UIRoot);
            }

            return _uIRoot;
        }

        public IHUD GetHUD() // maybe static
        {
            if (_hUD is null)
            {
                _resoursManager = GetResourseManager();

                _hUD = _resoursManager.CreateObjectInstance<IHUD, EView>(EView.HUD);
            }

            return _hUD;
        }

        public IPopups GetPopups() // maybe static
        {
            if (_popups is null)
            {
                _resoursManager = GetResourseManager();

                _popups = _resoursManager.CreateObjectInstance<IPopups, EView>(EView.Popups);
            }

            return _popups;
        }

        public void StartSession()
        {
            Debug.Log("Susses StartSession()");
        }

        public void InitGame(IScenesManager scenesManager, ISceneInformation info)
        {
            _player.SetPosition(info.PlayerSpawnTarget);
            var playerPosition = _player.GetPosition();

            _mainCamera.GetPlayerPosition(_player, playerPosition);

            _uIRoot.Init(_player, _levelSystem, scenesManager);

            _enemyManager.Init(playerPosition, info.EnemySpawnTargets, info.BossSpawnTarget);

            _saveSystem = new SaveSystem(_player, _levelSystem, scenesManager);

            info.EndLevel.Init(scenesManager);
        }
    }
}
