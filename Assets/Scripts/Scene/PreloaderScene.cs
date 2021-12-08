using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using ET.Core.LevelInfo;
using ET.UI;
using ET.UI.WindowTypes;
using ET.Interface;
using ET.Core;
using ET.Enums.EComponents;
using ET.Enums.Scenes;
using UnityEngine.EventSystems;
using ET.Enums.Views;

namespace ET
{
    public class PreloaderScene : MonoBehaviour, IPreloader
    {
        private ILoadingScreen _loadingScreen;
        private IResoursManager _resoursManager;
        private IScenesManager _scenesManager;
        private IMainMenu _mainMenu;

        private AsyncOperation _loading = null;

        protected void Awake()
        {
            _resoursManager = new ResoursesManager();
            _scenesManager = new ScenesManager(this);

            gameObject.AddComponent<EventSystem>();
            gameObject.AddComponent<StandaloneInputModule>();

            _mainMenu = GetMainMenu();
            _mainMenu.Init(this);

            DontDestroyOnLoad(gameObject);
        }

        protected void Start()
        {
            SceneManager.LoadSceneAsync(SceneIndex._MainMenu.ToString(), LoadSceneMode.Additive);

            _mainMenu.Show();

            DontDestroyOnLoad(_mainMenu.MainMenuTrans);
        }

        private IMainMenu GetMainMenu()
        {
            if (_mainMenu is null)
            {
                _mainMenu = _resoursManager.CreateObjectInstance<IMainMenu, EView>(EView.MainMenu);
            }

            return _mainMenu;
        }

        private ILoadingScreen GetLoadingScreen()
        {
            if (_loadingScreen is null)
            {
                _loadingScreen = _resoursManager.CreateObjectInstance<ILoadingScreen, EView>(EView.LoadingScreen);
            }

            return _loadingScreen;
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadSceneAsync(SceneIndex._MainMenu.ToString());
            _mainMenu.Show();
        }

        public void UploadScene(SceneIndex scene)
        {
            _mainMenu.Hide();

            StartCoroutine(AsyncLoading(scene));
        }

        private IEnumerator AsyncLoading(SceneIndex scene)
        {
            var bootScreen = GetLoadingScreen();

            bootScreen.Show();
            
            _loading = SceneManager.LoadSceneAsync(SceneIndex._GameSession.ToString());
            _loading.allowSceneActivation = false;

            while (!_loading.isDone)
            {
                bootScreen.LoadingLine.fillAmount += Mathf.Clamp01(1e-3f);

                if (_loading.progress >= 0.9f)
                {
                    yield return _loading.allowSceneActivation = true;
                }
            }

            _loading = SceneManager.LoadSceneAsync(scene.ToString(), LoadSceneMode.Additive);
            yield return _loading;

            var levelInfo = GameObject.FindGameObjectWithTag(Tags.LEVEL_INFO);
            InfoSceneObjects infoSceneObjects = levelInfo.GetComponent<InfoSceneObjects>();

            yield return null;

            _scenesManager.UpdateAfterLaunch(infoSceneObjects.LevelIndex);

            GameManager.Instance.InitGame(_scenesManager, infoSceneObjects);

            //bootScreen.Hide(); 

            if (_loading.isDone)
            {
                GameManager.Instance.StartSession();
            }
        }
    }
}
