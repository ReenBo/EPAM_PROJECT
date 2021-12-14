using UnityEngine;
using UnityEngine.SceneManagement;
using ET.Interface;
using ET.Enums.Scenes;
using System;

namespace ET.Core
{
    public class ScenesManager : IScenesManager
    {
        private IPreloader _preloader;
        private ISaveSystem _saveSystem;
        private SceneIndex _currentLevel = 0;

        public event Action<SceneIndex> OnGameStarts;
        public event Action OnGameProgressIsSaved;
        public event Action OnGameProgressIsBeingLoaded;
        public event Action OnSettingsGame;
        public event Action<SceneIndex> OnGameIsBeingRestarted;
        public event Action OnReturnsToMenu;

        public ScenesManager(IPreloader preloader)
        {
            _preloader = preloader;
        }

        public void Init(ISaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
            Debug.Log(_saveSystem);
        }

        public void UpdateAfterLaunch(SceneIndex index)
        {
            _currentLevel = index;
        }

        public void StartGame()
        {
            OnGameStarts.Invoke(SceneIndex._Level_1);
        }

        public void SaveGame()
        {
            OnGameProgressIsSaved.Invoke();
        }

        public void LoadGame()
        {
            OnGameProgressIsBeingLoaded.Invoke();
        }

        public void SettingsGame()
        {
            OnSettingsGame.Invoke();
        }

        public void Restart()
        {
            OnGameIsBeingRestarted.Invoke(_currentLevel);
        }

        public void ReturnMainMenu()
        {
            OnReturnsToMenu.Invoke();
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        public void NextLevel()
        {
            OnGameStarts.Invoke(SceneIndex._Level_2);
        }
    }
}
