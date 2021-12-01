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
            Debug.Log("_currentLevel" + _currentLevel);
        }

        public void StartGame()
        {
            _preloader.UploadScene(SceneIndex._Level_1);
        }

        public void Restart()
        {
            _preloader.UploadScene(_currentLevel);
        }

        public void ReturnMainMenu()
        {
            SceneManager.LoadSceneAsync(SceneIndex._MainMenu.ToString());
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}
