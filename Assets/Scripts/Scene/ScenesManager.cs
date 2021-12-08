using UnityEngine;
using UnityEngine.SceneManagement;
using ET.Interface;
using ET.Enums.Scenes;
using System;

namespace ET.Core
{
    public class ScenesManager : IScenesManager
    {
        private IPreloader _preloader = null;
        private SceneIndex _currentLevel = 0;

        public ScenesManager(IPreloader preloader)
        {
            _preloader = preloader;
        }

        public void UpdateAfterLaunch(SceneIndex index)
        {
            _currentLevel = index;
        }

        public void StartGame()
        {
            _preloader.UploadScene(SceneIndex._Level_1);
        }

        public void SaveGame()
        {

        }

        public void LoadGame()
        {

        }

        public void Restart()
        {
            _preloader.UploadScene(_currentLevel);
        }

        public void ReturnMainMenu()
        {
            _preloader.LoadMainMenu();
            //SceneManager.LoadSceneAsync(SceneIndex._MainMenu.ToString());
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}
