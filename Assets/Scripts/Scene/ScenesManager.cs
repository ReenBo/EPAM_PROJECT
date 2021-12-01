using UnityEngine;
using UnityEngine.SceneManagement;
using ET.Interface;
using ET.Enums.Scenes;

namespace ET.Core
{
    public class ScenesManager : IScenesManager
    {
        private IPreloader _preloader = null;

        private GameObject _preLoaderGameObject = null;

        private SceneIndex _currentLevel = 0;

        public ScenesManager(IPreloader preloader)
        {
            _preloader = preloader;
        }

        //protected void Start()
        //{
        //    _preLoaderGameObject = GameObject.FindGameObjectWithTag(Tags.PRELOADER);
        //    _preloaderScene = _preLoaderGameObject.GetComponent<PreloaderScene>();
        //}

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

        public void EndGame()
        {
            Application.Quit();
        }
    }
}
