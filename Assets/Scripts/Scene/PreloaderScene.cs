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

        [SerializeField] private Camera _camera;

        private AsyncOperation _loading;

        protected void Awake()
        {
            _resoursManager = GetResoursManager();
            _scenesManager = GetScenesManager();

            gameObject.AddComponent<EventSystem>();
            gameObject.AddComponent<StandaloneInputModule>();

            Instantiate(_camera);
            _mainMenu = GetMainMenu();
            _mainMenu.Init(_scenesManager);

            DontDestroyOnLoad(gameObject);
        }

        protected void Start()
        {
            _mainMenu.Show();

            _scenesManager.OnGameStarts += UploadScene;
            _scenesManager.OnGameIsBeingRestarted += UploadScene;
            _scenesManager.OnReturnsToMenu += LoadMainMenu;
        }

        protected void OnDestroy()
        {
            _scenesManager.OnGameStarts -= UploadScene;
            _scenesManager.OnGameIsBeingRestarted -= UploadScene;
            _scenesManager.OnReturnsToMenu -= LoadMainMenu;
        }

        private IResoursManager GetResoursManager()
        {
            if (_resoursManager is null)
            {
                _resoursManager = new ResoursesManager();
            }

            return _resoursManager;
        }

        private IScenesManager GetScenesManager()
        {
            if (_scenesManager is null)
            {
                _scenesManager = new ScenesManager(this);
            }

            return _scenesManager;
        }

        private IMainMenu GetMainMenu()
        {
            if (_mainMenu is null)
            {
                _resoursManager = GetResoursManager();

                _mainMenu = _resoursManager.CreateObjectInstance<IMainMenu, EView>(EView.MainMenu);
            }

            return _mainMenu;
        }

        private ILoadingScreen GetLoadingScreen()
        {
            if (_loadingScreen is null)
            {
                _resoursManager = GetResoursManager();

                _loadingScreen = _resoursManager.CreateObjectInstance<ILoadingScreen, EView>(EView.LoadingScreen);
            }

            return _loadingScreen;
        }

        public void LoadMainMenu()
        {
            _loading = SceneManager.LoadSceneAsync(SceneIndex._PreLevel.ToString());
            Destroy(gameObject);
        }

        public void UploadScene(SceneIndex scene)
        {
            StartCoroutine(AsyncLoading(scene));
        }

        private IEnumerator AsyncLoading(SceneIndex scene)
        {
            //var bootScreen = GetLoadingScreen();
            //bootScreen.Show();

            _loading = SceneManager.LoadSceneAsync(SceneIndex._GameSession.ToString());
            yield return _loading;

            #region Loading
            //_loading.allowSceneActivation = false;

            //while (!_loading.isDone)
            //{
            //    bootScreen.LoadingLine.fillAmount += Mathf.Clamp01(1e-3f);

            //    if (_loading.progress >= 0.9f)
            //    {
            //        yield return _loading.allowSceneActivation = true;
            //    }
            //}
            #endregion

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
